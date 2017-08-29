using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models.Environments
{
    public class EnvironmentsConfig
    {
        
        #region ctor

        public EnvironmentsConfig()
        {

        }

        public static void Initialize(string folderName)
        {
            EnvironmentsConfig._instance = new EnvironmentsConfig(folderName);
        }

        public static EnvironmentsConfig Instance { get { return EnvironmentsConfig._instance; } }

        private EnvironmentsConfig(string folderName)
        {

            this.folder = new DirectoryInfo(folderName);
            if (!folder.Exists)
                this.folder.Create();

        }

        #endregion ctor


        public EnvironmentsConfig Current { get; set; }

        //public List<EnvironmentsConfig> Nexts { get; set; }


        private readonly DirectoryInfo folder;
        private static EnvironmentsConfig _instance;

        public void AddEnvironment(string EnvironmentName)
        {

            var e = new EnvironmentConfig()
            {
                Name = EnvironmentName
            };



        }
    }
}
