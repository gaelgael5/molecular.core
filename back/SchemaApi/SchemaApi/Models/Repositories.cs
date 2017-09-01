using SchemaApi.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System;
using System.Linq;
using SchemaApi.Models.Stores;
using System.Reflection;
using SchemaApi.Exceptions;
using SchemaApi.Models.Structures;

namespace SchemaApi.Models
{

    public class Repository
    {

        private Repository(string folder)
        {
            this.Folder = new DirectoryInfo(folder);
            if (!this.Folder.Exists)
                this.Folder.Create();

        }

        public static void Initialize(string folder)
        {
            Repository._instance = new Repository(folder);
        }

        public DirectoryInfo Folder { get; private set; }

        private static Repository _instance;

        public static Repository Instance { get { return Repository._instance; } }


        public EventHandler<FileModelEventArgs> ModelEvents;

        internal void Append(string filename, Type type, string appplication, EventFileEnm eventType)
        {
            if (ModelEvents != null)
                ModelEvents(null, new FileModelEventArgs(filename, type, appplication, eventType));
        }

    }

    public enum EventFileEnm
    {
        Create,
        Update,
        Delete,
        Lock,
        UnLock,
        Read,
    }

    public class FileModelEventArgs : EventArgs
    {

        public FileModelEventArgs(string filename, Type type, string application, EventFileEnm eventType)
        {
            this.Filename = filename;
            this.Type = type;
            this.Application = application;
            this.EventType = eventType;
        }

        public string Filename { get; private set; }

        public Type Type { get; private set; }

        public string Application { get; private set; }

        public EventFileEnm EventType { get; private set; }
    }

    public class Repositories<T> where T : DataModelBase, new()
    {

        #region ctor        

        public static void Initialize(bool inApplication, string folderName = null, Func<string, T> ctor = null)
        {

            if (string.IsNullOrEmpty(folderName))
                folderName = typeof(T).Name;

            Repositories<T>._instance = new Repositories<T>(folderName, ctor, inApplication);

        }

        private Repositories(string folderName, Func<string, T> ctor, bool inApplication)
        {

            this.InApplication = inApplication;

            if (ctor != null)
                this.ctor = ctor;

            else
                this.ctor = (name) =>
                {
                    return new T()
                    {
                        Name = name,
                        Created = DateTime.UtcNow,
                        Updated = DateTime.UtcNow,
                        // UpdatedBy = userName,
                    };
                };

            this._datas = new Dictionary<string, box>();

            string path = inApplication
                ? Path.Combine(Repository.Instance.Folder.FullName, "Applications")
                : Repository.Instance.Folder.FullName
                ;

            this.folder = new DirectoryInfo(path);
            if (!this.folder.Exists)
                this.folder.Create();

            //this.watcher = new FileSystemWatcher(this.folder.FullName);
            //watcher.Changed += Watcher_Changed;
            //watcher.Created += Watcher_Created;
            //watcher.Deleted += Watcher_Deleted;
            //watcher.Error += Watcher_Error;
            ////watcher.IncludeSubdirectories = true;
            //watcher.Renamed += Watcher_Renamed;
            //watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;            
            //watcher.EnableRaisingEvents = true;

        }


        //private void Watcher_Renamed(object sender, RenamedEventArgs e)
        //{

        //}

        //private void Watcher_Error(object sender, ErrorEventArgs e)
        //{

        //}

        //private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        //{

        //}

        //private void Watcher_Created(object sender, FileSystemEventArgs e)
        //{

        //}

        //private void Watcher_Changed(object sender, FileSystemEventArgs e)
        //{

        //}

        #endregion ctor

        public T Create(string name)
        {
            return ctor(name);
        }

        public T Read(string name, string application = null)
        {

            CheckApplication(application);
            string _name = name.ToLower();

            box box;

            if (!this._datas.TryGetValue(_name, out box))
                lock (this.__lock)
                    if (!this._datas.TryGetValue(_name, out box))
                        box = LoadItem(name, application);

                    else if (box.IsObsolet)
                        box.Refresh();


            if (box.IsObsolet)
                lock (this.__lock)
                    if (box.IsObsolet)
                            box.Refresh();

            return box.Item;

        }

        private box LoadItem(string name, string application)
        {
            box box = new box(GetFilename(name, application), application);
            this._datas.Add(name, box);
            return box;
        }

        public List<string> GetList(string application)
        {

            CheckApplication(application);

            var files = folder.GetFiles(".json")
                .OfType<FileInfo>()
                .Select(c => c.Name.Substring(0, c.Name.Length - 5));

            List<string> result = files.ToList();

            return result;

        }

        public void Delete(string name, string lockId, string userName, string application = null)
        {

            CheckApplication(application);

            if (string.IsNullOrEmpty(lockId) || Guid.Empty.ToString() == lockId)
                throw new LockException("missing lock id");

            CheckLock(name, lockId, userName, application);

            string path = GetFilename(name, application);
            File.Delete(path);

            Repository.Instance.Append(path, typeof(T), application, EventFileEnm.Delete);

            path = GetFilename(name, application, "lock");
            File.Delete(path);

        }

        public string Lock(string name, string userName, string application = null)
        {

            CheckApplication(application);

            var fileLock = GetFilename(name, application, "lock");

            if (File.Exists(fileLock))
                throw new LockException("allready locked");

            LockModel _lock = new LockModel()
            {
                LockId = Guid.NewGuid().ToString(),
                LockedBy = userName,
                Created = DateTime.UtcNow,
                Name = name,
                Type = typeof(T).Name,
            };

            Serializer.SaveFile(_lock, fileLock);

            Repository.Instance.Append(GetFilename(name, application), typeof(T), application, EventFileEnm.Lock);

            var lock2 = Serializer.LoadFile<LockModel>(fileLock);

            if (lock2.LockId != _lock.LockId)
                throw new LockException($"{application}\\{name} is allready locked by {lock2.LockedBy}");

            return _lock.LockId;

        }

        public void Unlock(string name, string lockId, string userName, bool isAdmin, string application = null)
        {

            CheckApplication(application);

            CheckLock(name, lockId, userName, application);

            string fileLock = GetFilename(name, application, "lock");

            File.Delete(fileLock);

            Repository.Instance.Append(GetFilename(name, application), typeof(T), application, EventFileEnm.UnLock);

        }

        /// <summary>
        /// Adds a new application.
        /// </summary>
        /// <param name="name">Name of the application.</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">applicationName</exception>
        public bool Add(T item, string application = null)
        {

            CheckApplication(application);

            if (item == null)
                throw new NullReferenceException("item");

            if (string.IsNullOrEmpty(item.Name))
                throw new NullReferenceException("item.name");

            if (this.folder.GetDirectories().Any(c => string.Compare(c.Name, item.Name, true) == 0))
                return false;

            string path = GetFilename(item.Name, application);

            Serializer.SaveFile(item, path);

            Historize(path, item.Version);

            Repository.Instance.Append(path, typeof(T), application, EventFileEnm.Create);

            return true;

        }

        public void Update(T item, string lockId, string userName, string application = null)
        {

            CheckApplication(application);

            var storedItem = Read(item.Name, application);
            if (storedItem.Updated > item.Updated)
                throw new InvalidOperationException($"item {typeof(T).Name} have been changed from last the loading");

            CheckLock(item.Name, lockId, userName, application);

            item.Updated = DateTime.UtcNow;
            item.UpdatedBy = userName;
            item.Version++;

            string path = GetFilename(item.Name, application);
            Serializer.SaveFile<T>(item, path);

            Historize(path, item.Version);

            Repository.Instance.Append(path, typeof(T), application, EventFileEnm.Update);

        }


        private void CheckApplication(string application)
        {
            if (InApplication && string.IsNullOrEmpty(application))
                throw new InvalidOperationException($"type {typeof(T).Name} must be in application");

            else if (!InApplication && !string.IsNullOrEmpty(application))
                throw new InvalidOperationException($"type {typeof(T).Name} is not in application");
        }

        private string GetFilename(string name, string application, string suffix = null)
        {

            string path = string.Empty;

            DirectoryInfo dir = this.folder;
            if (this.InApplication)
                path = Path.Combine(dir.FullName, application, typeof(T).Name.Replace("`", "").Replace("+", "."));
            else
                path = Path.Combine(dir.FullName, typeof(T).Name.Replace("`", "").Replace("+", "."));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, FormatFilename(name, suffix));

            return path;

        }

        private string FormatFilename(string name, string suffix = null)
        {

            if (string.IsNullOrEmpty(suffix))
                suffix = string.Empty;
            else
                suffix = $".{suffix}";

            string result = $"{name}{suffix}.json";

            return result;
        }

        private void Historize(string path, int version, string application = null)
        {

            FileInfo f = new FileInfo(path);

            var dir = new DirectoryInfo(Path.Combine(f.Directory.FullName, "_History"));

            if (!dir.Exists)
                dir.Create();

            string filename = $"{Path.GetFileNameWithoutExtension(f.Name)}_{version}{f.Extension}";

            var target = Path.Combine(dir.FullName, filename);

            f.CopyTo(target);

        }

        public static Repositories<T> Instance
        {
            get
            {
                if (Repositories<T>._instance == null)
                    lock (_lock)
                        if (Repositories<T>._instance == null)
                            Repositories<T>.Initialize(false);
                return Repositories<T>._instance;
            }
        }

        public bool InApplication { get; private set; }

        private void CheckLock(string name, string lockId, string userName, string application)
        {

            CheckApplication(application);

            if (string.IsNullOrEmpty(lockId) || Guid.Empty.ToString() == lockId)
                throw new LockException("missing lock id");

            var fileLock = GetFilename(name, application, "lock");

            if (!File.Exists(fileLock))
                throw new LockException("missing lock");

            else
            {

                var _lock = Serializer.LoadFile<LockModel>(fileLock);

                if (_lock.LockedBy != userName)
                    throw new FailedAuthorizationException($"failed authorization, because the file has locked by {_lock.LockedBy} at {_lock.Created}");

            }

        }

        private class box
        {

            public box(string filename, string application)
            {
                this.file = new FileInfo(filename);
                this.application = application;
                Refresh();
                this.ReadedAt = DateTime.UtcNow;
            }

            public void Refresh()
            {
                this.Item = Serializer.LoadFile<T>(this.file.FullName);
                Repository.Instance.Append(file.FullName, typeof(T), this.application, EventFileEnm.Read);
            }

            public T Item { get; private set; }

            public bool IsObsolet
            {
                get
                {
                    this.file.Refresh();
                    return this.file.LastWriteTimeUtc > this.ReadedAt;
                }
            }

            private DateTime ReadedAt;
            private FileInfo file;
            private readonly string application;
        }

        private static object _lock = new object();
        private object __lock = new object();
        private readonly DirectoryInfo folder;
        private static Repositories<T> _instance;
        private Dictionary<string, box> _datas;
        private readonly Func<string, T> ctor;
        private readonly FileSystemWatcher watcher;
    }

}