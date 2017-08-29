using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SchemaApi.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestAddAndUpdateApplication()
        {

            string applicationName = "appTest";
            var dirPath = Path.GetTempPath() + "temp" + Guid.NewGuid().ToString().Replace("-", "");
            SchemaApi.Models.Structures.ApplicationStructures.Initialize(dirPath);

            var i = SchemaApi.Models.Structures.ApplicationStructures.Instance;
            i.AddApplication(applicationName);

            var application = i.GetApplication(applicationName);

            var l = i.Lock(application.Name, "moi");

            var s = new Models.Structures.StructureObject()
            {
                Name = "StructureTest",
                Kind = Models.TypeKind.Structure,
                Properties = new System.Collections.Generic.List<Models.Structures.StructureProperty>()
                {
                    new Models.Structures.StructureProperty()
                    {
                        Name = "Name",
                        Category = "Misc",
                        Description = "item's Name",
                        DisplayName = "Name",
                        Required = true,
                        Type = new Models.Structures.StructureObjectReference() { Name = SchemaApi.Models.Constants.Types.String },
                    }
                }
            };

            application.Objects.Add(s);

            i.SaveApplication(application, l, "moi");

            i.Unlock(applicationName, "moi", false);

        }

        [TestMethod]
        public void TestAddEnvironment()
        {

            string applicationName = "appTest";
            var dirPath = Path.GetTempPath() + "temp" + Guid.NewGuid().ToString().Replace("-", "");
            SchemaApi.Models.Environments.EnvironmentsConfig.Initialize(dirPath);

            var i = SchemaApi.Models.Environments.EnvironmentsConfig.Instance;
            i.AddEnvironment(applicationName);




        }


    }
}
