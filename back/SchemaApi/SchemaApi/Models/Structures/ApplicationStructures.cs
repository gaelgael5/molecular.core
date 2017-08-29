using Microsoft.Extensions.Configuration;
using SchemaApi.Exceptions;
using SchemaApi.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Structures
{
    public class ApplicationStructures
    {

        #region ctor

        private ApplicationStructures(string folderName)
        {
            //this.configuration = configuration;


            this.folder = new DirectoryInfo(folderName);
            if (!folder.Exists)
                this.folder.Create();


        }


        public static void Initialize(string folderName)
        {
            ApplicationStructures._instance = new ApplicationStructures(folderName);
        }

        public string Lock(string applicationName, string userName)
        {

            var dir = Path.Combine(this.folder.FullName, applicationName);
            var fileLock = Path.Combine(dir, "_lock.json");

            if (File.Exists(fileLock))
                throw new LockException("allready locked");

            LockApplicationModel _lock = new LockApplicationModel()
            {
                LockId = Guid.NewGuid().ToString(),
                LockedBy = userName,
                Created = DateTime.UtcNow,
            };

            Serializer.SaveFile(_lock, fileLock);

            return _lock.LockId;

        }

        public void Unlock(string applicationName, string userName, bool isAdmin)
        {

            var dir = Path.Combine(this.folder.FullName, applicationName);
            var fileLock = Path.Combine(dir, "_lock.json");

            if (!File.Exists(fileLock))
                throw new LockException("missing lock");

            else
            {

                var _lock = Serializer.LoadFile<LockApplicationModel>(fileLock);

                if (_lock.LockedBy != userName)
                    if (!isAdmin)
                        throw new FailedAuthorizationException("failed authorization");
            }

            File.Delete(fileLock);

        }

        public static ApplicationStructures Instance
        {
            get
            {
                return ApplicationStructures._instance;
            }
        }

        #endregion ctor

        public string[] GetApplications()
        {
            return this.folder.GetDirectories().Select(c => c.Name).ToArray();
        }

        public void SaveApplication(ApplicationStructure application, string lockId, string userName)
        {

            if (string.IsNullOrEmpty(lockId) || Guid.Empty.ToString() == lockId)
                throw new LockException("missing lock id");

            var appli = GetApplication(application.Name);
            if (appli.Updated > application.Updated)
                throw new InvalidOperationException("Application have been changed from the loading");

            var dir = Path.Combine(this.folder.FullName, application.Name);

            //if (!Directory.Exists(dir))
            //    return null;

            var fileLock = Path.Combine(dir, "_lock.json");

            if (File.Exists(fileLock))
            {

                var _lock = Serializer.LoadFile<LockApplicationModel>(fileLock);

                if (_lock.LockedBy != userName)
                    throw new FailedAuthorizationException("failed authorization");

                else if (_lock.LockId != lockId)
                    throw new FailedAuthorizationException("failed authorization");

            }
            else
                throw new LockException("please lock application before");

            application.Updated = DateTime.UtcNow;
            application.UpdatedBy = userName;
            application.Version++;

            string path = Path.Combine(dir, $"_application.json");
            Serializer.SaveFile(application, path);

            Historize(path, application.Version);

        }
    
        /// <summary>
        /// Adds a new application.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">applicationName</exception>
        public bool AddApplication(string applicationName)
        {

            if (string.IsNullOrEmpty(applicationName))
                throw new NullReferenceException("applicationName");

            if (this.folder.GetDirectories().Any(c => string.Compare(c.Name, applicationName, true) == 0))
                return false;

            var dir = this.folder.CreateSubdirectory(applicationName);

            ApplicationStructure application = new ApplicationStructure()
            {
                Name = applicationName,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Objects = new List<StructureObject>()
                {
                    new StructureObject() { Name = Constants.Types.String, Kind = TypeKind.Value },
                    new StructureObject() { Name = Constants.Types.DateTime, Kind = TypeKind.Value },
                    new StructureObject() { Name = Constants.Types.Integer, Kind = TypeKind.Value },
                    new StructureObject() { Name = Constants.Types.Decimal , Kind = TypeKind.Value},
                    new StructureObject() { Name = Constants.Types.Guid , Kind = TypeKind.Value},
                },

            };

            string path = Path.Combine(dir.FullName, "_application.json");
            Serializer.SaveFile(application, path);

            Historize(path, application.Version);

            return true;

        }

        private void Historize(string path, int version)
        {

            FileInfo f = new FileInfo(path);

            var dir = new DirectoryInfo(Path.Combine(f.Directory.FullName, "History"));

            if (!dir.Exists)
                dir.Create();

            string filename = $"{Path.GetFileNameWithoutExtension(f.Name)}_{version}{f.Extension}";

            var target = Path.Combine(dir.FullName, filename);

            f.CopyTo(target);

        }

        /// <summary>
        /// Adds a new application.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">applicationName</exception>
        public ApplicationStructure GetApplication(string applicationName)
        {

            if (string.IsNullOrEmpty(applicationName))
                throw new NullReferenceException("applicationName");

            var dir = Path.Combine(this.folder.FullName, applicationName);

            if (!Directory.Exists(dir))
                return null;

            var application = Serializer.LoadFile<ApplicationStructure>(Path.Combine(dir, "_application.json"));

            return application;

        }


        //private readonly IConfigurationRoot configuration;
        private readonly DirectoryInfo folder;
        private static ApplicationStructures _instance;
    }

}
