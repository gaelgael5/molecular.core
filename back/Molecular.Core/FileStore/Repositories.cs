using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using Molecular.Helpers;
using Molecular.Exceptions;

/// <summary>
/// 
/// </summary>
namespace Molecular.FileStore
{

    public class Repositories<TItem> where TItem : DataModelBase, new()
    {

        #region ctor        

        public static void Initialize(bool inApplication, string folderName = null, Func<string, TItem> ctor = null)
        {

            if (string.IsNullOrEmpty(folderName))
                folderName = typeof(TItem).Name;

            Repositories<TItem>._instance = new Repositories<TItem>(folderName, ctor, inApplication);

        }

        private Repositories(string folderName, Func<string, TItem> ctor, bool inApplication)
        {

            this.InApplication = inApplication;

            if (ctor != null)
                this.ctor = ctor;

            else
                this.ctor = (name) =>
                {
                    return new TItem()
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

        }

        #endregion ctor

        /// <summary>
        /// Adds a new application.
        /// </summary>
        /// <param name="name">Name of the application.</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">applicationName</exception>
        public bool Create(TItem item, string username, string application = null)
        {

            CheckApplication(application);

            if (item == null)
                throw new NullReferenceException("item");

            if (string.IsNullOrEmpty(item.Name))
                throw new NullReferenceException("item.name");

            if (item.Name.StartsWith("__"))
                throw new InvalidOperationException($"the name can't start with charset '__'");

            if (this.folder.GetDirectories().Any(c => string.Compare(c.Name, item.Name, true) == 0))
                return false;

            string path = string.Empty;
            string pathIndexLock = GetFilename("__index", application, "lock");

            using (var _l = Serializer.Lock(pathIndexLock, true))
            {

                IndexModelList items;
                path = GetFilename(item.Name, application, null);
                Serializer.SaveFile(item, path);

                string pathIndex = GetFilename("__index", application, null);
                if (File.Exists(pathIndex))
                    items = Serializer.LoadFile<IndexModelList>(pathIndex);
                else
                    items = new IndexModelList() {Version = -1 };

                IndexModel index = new IndexModel()
                {
                    Name = item.Name,
                };
                items.Items.Add(index);

                items.Updated = DateTime.UtcNow;
                items.UpdatedBy = username;
                items.Version += 1;

                items.SaveFile(pathIndex);

                Historize(path, item.Version);

            }

            Repository.Instance.Append(path, typeof(TItem), application, EventFileEnm.Create);

            return true;

        }

        public TItem Create(string name)
        {
            return ctor(name);
        }

        public TItem Read(string name, string application = null)
        {

            CheckApplication(application);
            string _name = name.ToLower();

            box box;

            if (!this._datas.TryGetValue(_name, out box))
                lock (this.__lock)
                    if (!this._datas.TryGetValue(_name, out box))
                        box = LoadItem<TItem>(_name, application);

                    else if (box.IsObsolet)
                        box.Refresh();


            if (box.IsObsolet)
                lock (this.__lock)
                    if (box.IsObsolet)
                        box.Refresh();

            return (box as box<TItem>).Item;

        }

        public IndexModelList Index(string application)
        {

            string _name = "__index";
            CheckApplication(application);

            box box;

            if (!this._datas.TryGetValue(_name, out box))
                lock (this.__lock)
                    if (!this._datas.TryGetValue(_name, out box))
                        box = LoadItem<IndexModelList>(_name, application);

                    else if (box.IsObsolet)
                        box.Refresh();


            if (box.IsObsolet)
                lock (this.__lock)
                    if (box.IsObsolet)
                        box.Refresh();

            box<IndexModelList> _box = box as box<IndexModelList>;

            return _box.Item;

        }

        public void Delete(string name, string lockId, string userName, string application = null)
        {

            CheckApplication(application);

            if (string.IsNullOrEmpty(lockId) || Guid.Empty.ToString() == lockId)
                throw new LockException("missing lock id");

            CheckLock(name, lockId, userName, application);

            string path = GetFilename(name, application, null);
            File.Delete(path);

            Repository.Instance.Append(path, typeof(TItem), application, EventFileEnm.Delete);

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
                Type = typeof(TItem).Name,
            };

            _lock.SaveFileWithLock(fileLock);

            Repository.Instance.Append(GetFilename(name, application, null), typeof(TItem), application, EventFileEnm.Lock);

            return _lock.LockId;

        }

        public void Unlock(string name, string lockId, string userName, bool isAdmin, string application = null)
        {

            CheckApplication(application);

            CheckLock(name, lockId, userName, application);

            string fileLock = GetFilename(name, application, "lock");

            File.Delete(fileLock);

            Repository.Instance.Append(GetFilename(name, application, null), typeof(TItem), application, EventFileEnm.UnLock);

        }

        public void Update(TItem item, string lockId, string userName, string application = null)
        {

            CheckApplication(application);

            var storedItem = Read(item.Name, application);
            if (storedItem.Updated > item.Updated)
                throw new InvalidOperationException($"item {typeof(TItem).Name} have been changed from last the loading");

            CheckLock(item.Name, lockId, userName, application);

            item.Updated = DateTime.UtcNow;
            item.UpdatedBy = userName;
            item.Version++;

            string path = GetFilename(item.Name, application, null);
            Serializer.SaveFile<TItem>(item, path);

            Historize(path, item.Version);

            Repository.Instance.Append(path, typeof(TItem), application, EventFileEnm.Update);

        }

        public static Repositories<TItem> Instance
        {
            get
            {
                if (Repositories<TItem>._instance == null)
                    lock (_lock)
                        if (Repositories<TItem>._instance == null)
                            Repositories<TItem>.Initialize(false);
                return Repositories<TItem>._instance;
            }
        }

        public bool InApplication { get; private set; }


        #region private 

        private box<T> LoadItem<T>(string name, string application)
        {
            box<T> box = new box<T>(GetFilename(name, application, null), application);
            this._datas.Add(name, box);
            return box;
        }

        private void CheckApplication(string application)
        {
            if (InApplication && string.IsNullOrEmpty(application))
                throw new InvalidOperationException($"type {typeof(TItem).Name} must be in application");

            else if (!InApplication && !string.IsNullOrEmpty(application))
                throw new InvalidOperationException($"type {typeof(TItem).Name} is not in application");
        }

        public string GetFilename(string name, string application, string suffix)
        {

            string path = string.Empty;

            DirectoryInfo dir = this.folder;
            if (this.InApplication)
                path = Path.Combine(dir.FullName, application, typeof(TItem).Name.Replace("`", "").Replace("+", "."));
            else
                path = Path.Combine(dir.FullName, typeof(TItem).Name.Replace("`", "").Replace("+", "."));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, FormatFilename(name, suffix));

            return path;

        }

        private static string FormatFilename(string name, string suffix = null)
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

        private abstract class box
        {

            public box(string filename, string application)
            {
                this.file = new FileInfo(filename);
                this.application = application;
                Refresh();
            }

            public bool IsObsolet
            {
                get
                {
                    this.file.Refresh();
                    return this.file.LastWriteTimeUtc > this.ReadedAt;
                }
            }

            public abstract void Refresh();

            protected DateTime ReadedAt;
            protected readonly FileInfo file;
            protected readonly string application;

        }

        private class box<T> : box
        {

            public box(string filename, string application) 
                : base (filename, application)
            {
            }

            public override void Refresh()
            {
                this.Item = Serializer.LoadFile<T>(this.file.FullName);
                Repository.Instance.Append(file.FullName, typeof(TItem), this.application, EventFileEnm.Read);
                this.ReadedAt = DateTime.UtcNow;
            }

            public T Item { get; private set; }           

        }

        private static object _lock = new object();
        private object __lock = new object();
        private object __lockIndex = new object();
        private readonly DirectoryInfo folder;
        private static Repositories<TItem> _instance;
        private Dictionary<string, box> _datas;
        private readonly Func<string, TItem> ctor;

        #endregion private 

    }

}