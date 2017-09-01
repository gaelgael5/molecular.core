using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SchemaApi.Models;
using SchemaApi.Models.Structures;
using System.Collections.Generic;
using System.Diagnostics;

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

            Debug.WriteLine(dirPath);

            Repository.Initialize(dirPath);

            Repositories<Structure>.Initialize(true, null, (name) =>
            {
                return new Structure()
                {
                    Name = name,
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
            });

            var i = Repositories<Structure>.Instance;
            i.Add(i.Create(applicationName), applicationName);

            var application = i.Get(applicationName, applicationName);

            var l = i.Lock(application.Name, "moi", applicationName);

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

            i.Update(application, l, "moi", applicationName);

            i.Delete(applicationName, l, "moi", applicationName);

        }

        [TestMethod]
        public void TestAddEnvironment()
        {

            string applicationName = "appTest";
            var dirPath = Path.GetTempPath() + "temp" + Guid.NewGuid().ToString().Replace("-", "");

            Debug.WriteLine(dirPath);

            Repository.Initialize(dirPath);

            Repositories<Structure>.Initialize(true, @"Applications", (name) =>
            {
                return new Structure()
                {
                    Name = name,
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
            });

            //SchemaApi.Models.Structures.ApplicationStructures.Initialize(dirPath);

            //var i = Repositories<ApplicationStructure>.Instance;
            //var i = Repositories<ApplicationStructure>.Instance;

            //SchemaApi.Models.Environments.EnvironmentsConfig.Initialize(dirPath);

            //var i = SchemaApi.Models.Environments.EnvironmentsConfig.Instance;
            //i.AddEnvironment(applicationName);




        }


    }
}
