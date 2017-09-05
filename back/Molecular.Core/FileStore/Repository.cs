using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Molecular.FileStore
{

    public class Repository
    {

        private Repository(string folder, string urlMaster, bool isMaster)
        {
            this.Folder = new DirectoryInfo(folder);
            if (!this.Folder.Exists)
                this.Folder.Create();

            this.UrlMaster = urlMaster;
            this.IsMaster = isMaster;

        }


        public string UrlMaster { get; private set; }

        public bool IsMaster { get; private set; }

        public static void Initialize(string folder, string urlMaster, bool isMaster)
        {
            Repository._instance = new Repository(folder, urlMaster, isMaster);
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

        internal string Lock(string filename)
        {
            // A terme doit être fourni pas le server maitre
            return Guid.NewGuid().ToString();

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


}
