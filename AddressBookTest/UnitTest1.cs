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
        AddressBookRepositoryEr erRepository;
        AddressBookTransaction transaction;
        [TestInitialize]
        public void Setup()
        {
            model = new AddressBookModel();
            repository = new AddressBookRepository();
            erRepository = new AddressBookRepositoryEr();
            transaction = new AddressBookTransaction();
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

        /// <summary>
        /// Methods to test stored procedure update
        /// </summary>
        [TestMethod]
        public void UpdateDataUsingStoredProcedure()
        {
            try
            {
                string actual, expected;
                //Setting values to model object
                model.firstName = "Diwakar";
                model.emailId = "diwakar@gmail.com";
                
                //Expected
                expected = "Updated 1 rows";
                actual = repository.UpdateEmailIdUsingStoredProcedure(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        // <summary>
        /// Test method to retreive data usin name
        /// </summary>
        [TestMethod]
        public void TestForRetrieveUsingStateNameOrCity()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = repository.RetreiveDataBasedOnStateNameOrCityName(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Retrive All Data based on Er diagram
        /// </summary>
        [TestMethod]
        public void TestForRetreiveAllDataEr()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = erRepository.RetreiveAllDataEr(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Retrive All Data based on Er diagram
        /// </summary>
        [TestMethod]
        public void TestForRetrieveUsingStateNameOrCityEr()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = erRepository.RetreiveAllDataStateNameOrCityEr(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Sort Based on first name
        /// </summary>
        [TestMethod]
        public void TestForSortByFirstName()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = erRepository.SortBasedOnFirstName(model);
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Add a date added field
        /// </summary>
        [TestMethod]
        public void TestForAddDateAddedField()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = transaction.AlterTableAddDateAdded();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Add a date added field
        /// </summary>
        [TestMethod]
        public void TestForUpdateDateAddedField()
        {
            try
            {
                string actual, expected;
                expected = "Success";
                actual = transaction.UpdateDateAddedCoulmn();
                Assert.AreEqual(actual, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
