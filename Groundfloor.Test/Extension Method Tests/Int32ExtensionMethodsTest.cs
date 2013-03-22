using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Groundfloor.Test
{
    
    
    /// <summary>
    ///This is a test class for Int32ExtensionMethodsTest and is intended
    ///to contain all Int32ExtensionMethodsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Int32ExtensionMethodsTest
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
        ///A test for EqualsEnum
        ///</summary>
        [TestMethod()]
        public void EqualsEnumTest()
        {
            int num = 0; // TODO: Initialize to an appropriate value
            Enum @enum = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Int32ExtensionMethods.EqualsEnum(num, @enum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NullIf
        ///</summary>
        [TestMethod()]
        public void NullIfTest()
        {
            int num = 0; // TODO: Initialize to an appropriate value
            int compareTo = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = Int32ExtensionMethods.NullIf(num, compareTo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToFormat
        ///</summary>
        [TestMethod()]
        public void ToFormatTest()
        {
            int num = 0; // TODO: Initialize to an appropriate value
            string format = string.Empty; // TODO: Initialize to an appropriate value
            string formatIfPositive = string.Empty; // TODO: Initialize to an appropriate value
            string formatIfZero = string.Empty; // TODO: Initialize to an appropriate value
            string formatIfNegative = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = Int32ExtensionMethods.ToFormat(num, format, formatIfPositive, formatIfZero, formatIfNegative);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToFormat
        ///</summary>
        [TestMethod()]
        public void ToFormatTest1()
        {
            Nullable<int> num = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string format = string.Empty; // TODO: Initialize to an appropriate value
            string formatIfPositive = string.Empty; // TODO: Initialize to an appropriate value
            string formatIfZero = string.Empty; // TODO: Initialize to an appropriate value
            string formatIfNegative = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = Int32ExtensionMethods.ToFormat(num, format, formatIfPositive, formatIfZero, formatIfNegative);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
