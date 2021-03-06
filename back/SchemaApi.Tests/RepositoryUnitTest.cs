using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SchemaApi.Models;
using SchemaApi.Models.Structures;
using System.Collections.Generic;
using System.Diagnostics;
using Molecular.FileStore;
using Molecular;
using Molecular.Helpers;

namespace SchemaApi.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestAddAndUpdateApplication()
        {

            string applicationName = "apptest";
            var dirPath = Path.GetTempPath() + "temp" + Guid.NewGuid().ToString().Replace("-", "");
            string _file = Path.Combine(dirPath, @"Applications", applicationName, "Structure", applicationName + ".json");

            // C:\Users\g.beard\AppData\Local\Temp\tempf9832303dd2a45f6a2ae1ef71713f83e\Applications\apptest\Structure\apptest.Structure.json

            Debug.WriteLine(dirPath);

            Repository.Initialize(dirPath, "", true);

            string outputFilename = string.Empty;
            EventFileEnm outputTypeEvent = EventFileEnm.Update;
            Repository.Instance.ModelEvents += (o, a) =>
            {
                outputFilename = a.Filename;
                outputTypeEvent = a.EventType;
            };

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

            i.Create(i.Create(applicationName), "moi", applicationName);

            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Create);

            var application = i.Read(applicationName, applicationName);
            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Read);

            var l = i.Lock(application.Name, "moi", applicationName);
            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Lock);

            var s = new Models.Structures.StructureObject()
            {
                Name = "StructureTest",
                Kind = Molecular.TypeKind.Structure,
                Properties = new System.Collections.Generic.List<Models.Structures.StructureProperty>()
                {
                    new Models.Structures.StructureProperty()
                    {
                        Name = "Name",
                        Category = "Misc",
                        Description = "item's Name",
                        DisplayName = "Name",
                        Required = true,
                        Type = new StructureObjectReference() { Name = Constants.Types.String },
                    }
                }
            };

            application.Objects.Add(s);

            i.Update(application, l, "moi", applicationName);
            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Update);

            i.Delete(applicationName, l, "moi", applicationName);
            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Delete);

        }

        [TestMethod]
        public void TestAddAndUpdate()
        {

            string name = "apptest";
            var dirPath = Path.GetTempPath() + "temp" + Guid.NewGuid().ToString().Replace("-", "");
            string _file = Path.Combine(dirPath, "Structure", name + ".json");

            // C:\Users\g.beard\AppData\Local\Temp\tempf9832303dd2a45f6a2ae1ef71713f83e\Structure\apptest.Structure.json

            Debug.WriteLine(dirPath);

            Repository.Initialize(dirPath, "", true);

            string outputFilename = string.Empty;
            EventFileEnm outputTypeEvent = EventFileEnm.Update;
            Repository.Instance.ModelEvents += (o, a) =>
            {
                outputFilename = a.Filename;
                outputTypeEvent = a.EventType;
            };

            Repositories<Structure>.Initialize(false, null, (name1) =>
            {
                return new Structure()
                {
                    Name = name1,
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

            i.Create(i.Create(name), "moi");

            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Create);

            var application = i.Read(name);
            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Read);

            var l = i.Lock(application.Name, "moi");
            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Lock);

            var s = new Models.Structures.StructureObject()
            {
                Name = "StructureTest",
                Kind = TypeKind.Structure,
                Properties = new System.Collections.Generic.List<Models.Structures.StructureProperty>()
                {
                    new Models.Structures.StructureProperty()
                    {
                        Name = "Name",
                        Category = "Misc",
                        Description = "item's Name",
                        DisplayName = "Name",
                        Required = true,
                        Type = new Models.Structures.StructureObjectReference() { Name = Constants.Types.String },
                    }
                }
            };

            application.Objects.Add(s);

            i.Update(application, l, "moi");
            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Update);

            i.Delete(name, l, "moi");
            Assert.AreEqual(_file, outputFilename);
            Assert.AreEqual(outputTypeEvent, EventFileEnm.Delete);

        }

        [TestMethod]
        public void TestLock()
        {

            string name = "apptest";
            var dirPath = Path.GetTempPath() + "temp" + Guid.NewGuid().ToString().Replace("-", "");
            string _file = Path.Combine(dirPath, "Structure", name + ".json");

            Debug.WriteLine(dirPath);

            Repository.Initialize(dirPath, "", true);

            Repositories<Structure>.Initialize(false, null, (name1) =>
            {
                return new Structure()
                {
                    Name = name1,
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

            i.Create(i.Create(name), "moi");

            var application = i.Read(name);

            var l = i.Lock(application.Name, "moi");

            try
            {
                var l2 = i.Lock(application.Name, "moi");
                Assert.Fail("file should be locked");
            }
            catch (Exception)
            {

            }

            i.Delete(name, l, "moi");

        }

        [TestMethod]
        public void TestLock2()
        {

            string name = "apptest";
            var dirPath = Path.GetTempPath() + "temp" + Guid.NewGuid().ToString().Replace("-", "");
            string _file = Path.Combine(dirPath, "Structure", name + ".json");

            FileInfo f = new FileInfo(_file);
            f.Directory.Create();

            using (var  l = Serializer.Lock(_file, true))
            {

                try
                {
                    using (var l2 = Serializer.Lock(_file, true))
                    {

                        Assert.Fail("file should be locked");

                    }
                }
                catch (Exception)
                {

                }

            }

        }


        [TestMethod]
        public void TestAddEnvironment()
        {

            string applicationName = "appTest";
            var dirPath = Path.GetTempPath() + "temp" + Guid.NewGuid().ToString().Replace("-", "");

            Debug.WriteLine(dirPath);

            Repository.Initialize(dirPath, "", true);

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
