using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Groundfloor.Google;

namespace Groundfloor.Test
{
    [TestClass]
    public class GoogleDocTest
    {
        [TestMethod]
        public void ReadGoogleDocument()
        {
            SpreadsheetProxy proxy = new SpreadsheetProxy(new Credentials { username = "robot9820@gmail.com", password = "Asimov123" });
            Dictionary<string,string>[] worksheet = proxy.GetWorkbookData("phlavor-mixtures");

            Assert.AreEqual(worksheet.Length, 3);
            Assert.AreEqual(worksheet[0].Keys.Count, 5);
        }
        [TestMethod]
        public void ClearGoogleDocument()
        {
            SpreadsheetProxy proxy = new SpreadsheetProxy(new Credentials { username = "robot9820@gmail.com", password = "Asimov123" });
            proxy.InitWorkbook("phlavor-mixtures", "phlavor-mixtures.xlsx");
        }
    }
}
