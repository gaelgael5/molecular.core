using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SchemaApi.Models.Structures;
using SchemaApi.Models.Generators;

namespace SchemaApi.Tests
{
    [TestClass]
    public class UnitTest2
    {

        [TestMethod]
        public void Test1()
        {

            Type type = typeof(ApplicationStructure);


            TypeDiscoverRepository repository = new TypeDiscoverRepository(null);

            repository.Add(type);





        }


    }
}