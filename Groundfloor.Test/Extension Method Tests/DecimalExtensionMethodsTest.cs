using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Groundfloor.Test
{
    
    
    /// <summary>
    ///This is a test class for DecimalExtensionMethodsTest and is intended
    ///to contain all DecimalExtensionMethodsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DecimalExtensionMethodsTest
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
        ///A test for ToFormat
        ///</summary>
        [TestMethod()]
        public void ToFormatTest()
        {
            Nullable<Decimal> num = new Nullable<Decimal>(); // TODO: Initialize to an appropriate value
            string format = string.Empty; // TODO: Initialize to an appropriate value
            string formatIfPositive = string.Empty; // TODO: Initialize to an appropriate value
            string formatIfZero = string.Empty; // TODO: Initialize to an appropriate value
            string formatIfNegative = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = DecimalExtensionMethods.ToFormat(num, format, formatIfPositive, formatIfZero, formatIfNegative);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
