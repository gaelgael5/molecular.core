using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SchemaApi.Models.Structures;
using SchemaApi.Models.Generators;
using System.Threading.Tasks;

namespace SchemaApi.Tests
{
    [TestClass]
    public class IntegrationUnitTest
    {

        [TestMethod]
        public void Test1()
        {


            var dirPath = Path.Combine( new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"SchemaApi\SchemaApi");

            //Directory.CreateDirectory(dirPath);

            //GenerateAppsettings(dirPath);
            //GenerateTargetKeys(dirPath);

            Program.Main(new string[] { dirPath });

            while (!Program.Instance.task.IsCompleted)
            {
                Task.Yield();
            }

        }

        private static void GenerateAppsettings(string dirPath)
        {
            string appsetting = @"
{

  ""Logging"": {
    ""IncludeScopes"": false,
    ""LogLevel"": {
                ""Default"": ""Warning""
    }
        },

  ""Schemas"": {
    ""RootFolder"": ""Root""
  },

  ""Scenarii"": {

    ""Path"": ""Scenarii""

  }

}";
            File.AppendAllText(Path.Combine(dirPath, "appsettings.json"), appsetting);
        }


        private static void GenerateTargetKeys(string dirPath)
        {
            string appsetting = @"[ {""Key"":  ""::front"", ""Value"":  ""C:\\_d\\src\\molecular.core\\front\\src\\app""} ]";
            File.AppendAllText(Path.Combine(dirPath, "TargetKeys.json"), appsetting);
        }

        private static void GenerateScenarii(string dirPath)
        {
            string appsetting = @"[ {""Key"":  ""::front"", ""Value"":  ""C:\\_d\\src\\molecular.core\\front\\src\\app""} ]";
            File.AppendAllText(Path.Combine(dirPath, "appsettings.json"), appsetting);
        }

    }

}
