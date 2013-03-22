using Groundfloor.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;

namespace Groundfloor.Test
{
    
    
    /// <summary>
    ///This is a test class for HashTest and is intended
    ///to contain all HashTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HashTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Hash Constructor
        ///</summary>
        [TestMethod()]
        public void HashConstructorTest()
        {
            HashAlgorithm HashAlgorithm = null; // TODO: Initialize to an appropriate value
            int theSaltLength = 0; // TODO: Initialize to an appropriate value
            Hash target = new Hash(HashAlgorithm, theSaltLength);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ComputeHash
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Groundfloor.dll")]
        public void ComputeHashTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Hash_Accessor target = new Hash_Accessor(param0); // TODO: Initialize to an appropriate value
            string Data = string.Empty; // TODO: Initialize to an appropriate value
            string Salt = string.Empty; // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.ComputeHash(Data, Salt);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ComputeHash
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Groundfloor.dll")]
        public void ComputeHashTest1()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Hash_Accessor target = new Hash_Accessor(param0); // TODO: Initialize to an appropriate value
            byte[] Data = null; // TODO: Initialize to an appropriate value
            byte[] Salt = null; // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.ComputeHash(Data, Salt);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GenerateSalt
        ///</summary>
        [TestMethod()]
        public void GenerateSaltTest()
        {
            HashAlgorithm HashAlgorithm = null; // TODO: Initialize to an appropriate value
            int theSaltLength = 0; // TODO: Initialize to an appropriate value
            Hash target = new Hash(HashAlgorithm, theSaltLength); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.GenerateSalt();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GenerateSaltString
        ///</summary>
        [TestMethod()]
        public void GenerateSaltStringTest()
        {
            HashAlgorithm HashAlgorithm = null; // TODO: Initialize to an appropriate value
            int theSaltLength = 0; // TODO: Initialize to an appropriate value
            Hash target = new Hash(HashAlgorithm, theSaltLength); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GenerateSaltString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetHashAndSalt
        ///</summary>
        [TestMethod()]
        public void GetHashAndSaltTest()
        {
            HashAlgorithm HashAlgorithm = null; // TODO: Initialize to an appropriate value
            int theSaltLength = 0; // TODO: Initialize to an appropriate value
            Hash target = new Hash(HashAlgorithm, theSaltLength); // TODO: Initialize to an appropriate value
            byte[] Data = null; // TODO: Initialize to an appropriate value
            byte[] Hash = null; // TODO: Initialize to an appropriate value
            byte[] HashExpected = null; // TODO: Initialize to an appropriate value
            byte[] Salt = null; // TODO: Initialize to an appropriate value
            byte[] SaltExpected = null; // TODO: Initialize to an appropriate value
            target.GetHashAndSalt(Data, out Hash, out Salt);
            Assert.AreEqual(HashExpected, Hash);
            Assert.AreEqual(SaltExpected, Salt);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetHashAndSaltString
        ///</summary>
        [TestMethod()]
        public void GetHashAndSaltStringTest()
        {
            HashAlgorithm HashAlgorithm = null; // TODO: Initialize to an appropriate value
            int theSaltLength = 0; // TODO: Initialize to an appropriate value
            Hash target = new Hash(HashAlgorithm, theSaltLength); // TODO: Initialize to an appropriate value
            string clearPassword = string.Empty; // TODO: Initialize to an appropriate value
            string Hash = string.Empty; // TODO: Initialize to an appropriate value
            string HashExpected = string.Empty; // TODO: Initialize to an appropriate value
            string Salt = string.Empty; // TODO: Initialize to an appropriate value
            string SaltExpected = string.Empty; // TODO: Initialize to an appropriate value
            target.GetHashAndSaltString(clearPassword, out Hash, out Salt);
            Assert.AreEqual(HashExpected, Hash);
            Assert.AreEqual(SaltExpected, Salt);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetHashString
        ///</summary>
        [TestMethod()]
        public void GetHashStringTest()
        {
            HashAlgorithm HashAlgorithm = null; // TODO: Initialize to an appropriate value
            int theSaltLength = 0; // TODO: Initialize to an appropriate value
            Hash target = new Hash(HashAlgorithm, theSaltLength); // TODO: Initialize to an appropriate value
            string clearPassword = string.Empty; // TODO: Initialize to an appropriate value
            string salt = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetHashString(clearPassword, salt);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToHash
        ///</summary>
        [TestMethod()]
        public void ToHashTest()
        {
            string plainText = string.Empty; // TODO: Initialize to an appropriate value
            string salt = string.Empty; // TODO: Initialize to an appropriate value
            int saltLength = 0; // TODO: Initialize to an appropriate value
            HashAlgorithm algorithm = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = Hash.ToHash(plainText, salt, saltLength, algorithm);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToHexit
        ///</summary>
        [TestMethod()]
        public void ToHexitTest()
        {
            string plainText = string.Empty; // TODO: Initialize to an appropriate value
            string salt = string.Empty; // TODO: Initialize to an appropriate value
            int saltLength = 0; // TODO: Initialize to an appropriate value
            HashAlgorithm algorithm = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = Hash.ToHexit(plainText, salt, saltLength, algorithm);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for VerifyHash
        ///</summary>
        [TestMethod()]
        public void VerifyHashTest()
        {
            HashAlgorithm HashAlgorithm = null; // TODO: Initialize to an appropriate value
            int theSaltLength = 0; // TODO: Initialize to an appropriate value
            Hash target = new Hash(HashAlgorithm, theSaltLength); // TODO: Initialize to an appropriate value
            byte[] Data = null; // TODO: Initialize to an appropriate value
            byte[] Hash = null; // TODO: Initialize to an appropriate value
            byte[] Salt = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.VerifyHash(Data, Hash, Salt);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for VerifyHashString
        ///</summary>
        [TestMethod()]
        public void VerifyHashStringTest()
        {
            HashAlgorithm HashAlgorithm = null; // TODO: Initialize to an appropriate value
            int theSaltLength = 0; // TODO: Initialize to an appropriate value
            Hash target = new Hash(HashAlgorithm, theSaltLength); // TODO: Initialize to an appropriate value
            string Data = string.Empty; // TODO: Initialize to an appropriate value
            string Hash = string.Empty; // TODO: Initialize to an appropriate value
            string Salt = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.VerifyHashString(Data, Hash, Salt);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
