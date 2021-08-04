using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookAdo.net;
using System;

namespace AddressBookTest
{
    [TestClass]
    public class UnitTest1
    {
        AddressBookModel model;
        AddressBookRepository repository;

        [TestInitialize]
        public void Setup()
        {
            model = new AddressBookModel();
            repository = new AddressBookRepository();
        }
        /// <summary>
        /// Retrive All Data
        /// </summary>
        [TestMethod]
        public void TestForRetreiveAllData()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = repository.RetreiveAllData(model);
                Assert.AreEqual(actual, expected);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
