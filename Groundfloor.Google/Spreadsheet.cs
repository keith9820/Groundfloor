using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Spreadsheets;
using Google.GData.Client;
using System.Collections;
using Google.GData.Documents;

namespace Groundfloor.Google
{
    public struct Credentials
    {
        public string username;
        public string password;

        public Credentials(string uname, string pwd)
        {
            username = uname; password = pwd;
        }
    }

    public class SpreadsheetProxy
    {
        SpreadsheetsService spreadsheetService;
        DocumentsService documentService;


        private List<WorksheetEntry> GetWorkbookSheets(string workbookName)
        {
            var query = new global::Google.GData.Spreadsheets.SpreadsheetQuery();
            SpreadsheetFeed feed = spreadsheetService.Query(query);

            var list = new List<WorksheetEntry>();
            AtomEntryCollection entries = feed.Entries;
            foreach (SpreadsheetEntry entry in entries)
            {
                if (!entry.Title.Text.Resembles(workbookName))
                    continue;

                list.AddRange(GetAllWorksheets(entry));
                break;
            }

            return list;
        }
        private List<WorksheetEntry> GetAllWorksheets(SpreadsheetEntry entry)
        {
            AtomLink listLink = entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, AtomLink.ATOM_TYPE);

            WorksheetQuery query = new WorksheetQuery(listLink.HRef.ToString());
            WorksheetFeed feed = spreadsheetService.Query(query);

            var list = new List<WorksheetEntry>();
            foreach (WorksheetEntry worksheet in feed.Entries)
            {
                list.Add(worksheet);
            }
            return list;
        }

        public Dictionary<string, string>[] GetWorkbookData(string workbookName, int worksheetNumber = 0)
        {
            List<WorksheetEntry> list = GetWorkbookSheets(workbookName);
            ListQuery query = new ListQuery(list[0].GetFeedLink());
            ListFeed feed = spreadsheetService.Query(query);
            AtomEntryCollection entries = feed.Entries;

            Dictionary<string, string>[] results = new Dictionary<string, string>[entries.Count];
            int i = 0;

            foreach(ListEntry entry in entries)
            {
                if (entry != null)
                {
                    Dictionary<string, string> row = new Dictionary<string, string>();
                    foreach(ListEntry.Custom element in entry.Elements)
                    {
                        row.Add(element.LocalName, element.Value);
                    }
                    results[i++] = row;
                }
            }
            return results;

            //CellQuery query = new CellQuery(list[0].CellFeedLink);
            //CellFeed feed = spreadsheetService.Query(query);

            //AtomEntryCollection entries = feed.Entries;

            //int currentRow = 1;
            //int currentCol = 1;

            //List<List<string>> table = new List<List<string>>();
            //List<string> row = new List<string>();

            //for (int i = 0; i < entries.Count; i++)
            //{
            //    CellEntry entry = entries[i] as CellEntry;
            //    if (entry != null)
            //    {
            //        // Add the current row, since we are starting 
            //        // a new row in data from the feed
            //        while (entry.Cell.Row > currentRow)
            //        {
            //            table.Add(row);
            //            row = new List<string>();
            //            currentRow++;
            //            currentCol = 1;
            //        }
            //        // Add blank entries where there is no data for the column
            //        while (entry.Cell.Column > currentCol)
            //        {
            //            row.Add("");
            //            currentCol++;
            //        }

            //        // Add the current data entry
            //        row.Add(entry.Cell.Value);
            //        currentCol++;
            //    }
            //}

            //turn the table into a dictionary.  First row is header row, contains keys.  If header column is empty, ignore that column
            //Dictionary<string, string>[] results = new Dictionary<string, string>[table.Count-1];
            //for (int i = 1; i < table.Count; i++)
            //{
            //    Dictionary<string, string> entry = new Dictionary<string, string>();
            //    for (int c=0; c<table[0].Count; c++)
            //    {
            //        string key = table[0][c];
            //        if (key.isNotEmpty())
            //        {
            //            entry.Add(key, table[i][c]);
            //        }
            //        results[i - 1] = entry;
            //    }
            //}
            //return results;
        }

        public void InitWorkbook(string workbookName, string uploadTemplatePath)
        {
            DocumentsFeed docs = documentService.Query(new DocumentsListQuery());
            DocumentEntry entry = null;
            foreach (DocumentEntry doc in docs.Entries)
            {
                if (!doc.IsSpreadsheet)
                    continue;

                if (doc.Title.Text == workbookName)
                {
                    entry = doc;
                    break;
                }
            }
            if (entry == null)
                documentService.UploadDocument(uploadTemplatePath, null);
            else
            {
                ClearSheet(workbookName);
            }
        }

        private void ClearSheet(string workbookName)
        {
            var page1 = (WorksheetEntry)GetWorkbookSheets(workbookName)[0];
            if (page1 == null)
            {
                throw new ApplicationException("Worksheet not found");
            }


            AtomLink listFeedLink = page1.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null);
            ListFeed listFeed = spreadsheetService.Query(new ListQuery(listFeedLink.HRef.ToString()));
            CellFeed cells = spreadsheetService.Query(new CellQuery(listFeedLink.HRef.ToString()));

            int ctr = 0;
            CellEntry toUpdateA;
            CellEntry toUpdateB;
            AtomFeed batchFeed = new AtomFeed(cells);
            // This is for the header row 
            ctr = (int)cells.ColCount.Count;
            // Skip all of this if there are no cells with values in them other than those in the header row
            if (cells.Entries.Count > ctr)
            {
                // Process through all of the cells that have a value in them starting at the cell below the Header
                while (true)
                {
                    toUpdateA = (CellEntry)cells.Entries[ctr];
                    toUpdateA.Cell.InputValue = "";
                    toUpdateA.BatchData = new GDataBatchEntryData("A", GDataBatchOperationType.update);
                    batchFeed.Entries.Add(toUpdateA);
                    ctr++;
                    if (ctr >= cells.Entries.Count)
                    {
                        break;
                    }
                    toUpdateB = (CellEntry)cells.Entries[ctr];
                    toUpdateB.Cell.InputValue = "";
                    toUpdateB.BatchData = new GDataBatchEntryData("B", GDataBatchOperationType.update);
                    batchFeed.Entries.Add(toUpdateB);
                    ctr = ctr + 1;
                    if (ctr >= cells.Entries.Count)
                    {
                        break;
                    }
                }
                // Erase all cells
                CellFeed batchResultFeed = (CellFeed)spreadsheetService.Batch(batchFeed, new Uri(cells.Batch));
            }
        }

        public SpreadsheetProxy(Credentials credentials)
        {
            spreadsheetService = new SpreadsheetsService("groundfloor-svc1");
            spreadsheetService.setUserCredentials(credentials.username, credentials.password);

            documentService = new DocumentsService("groundfloor-svc2");
            documentService.setUserCredentials(credentials.username, credentials.password);
        }
    }
}
