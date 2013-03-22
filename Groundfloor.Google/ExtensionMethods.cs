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
    public enum LinkType
    {
        listfeed,
        cellsfeed,
        edit
    }
    public static class ExtensionMethods
    {
        public static string GetFeedLink(this WorksheetEntry entry, LinkType feedType = LinkType.listfeed)
        {
            return entry.Links.GetHrefContent(feedType);
        }
        public static string GetHrefContent(this AtomLinkCollection links, LinkType feedType = LinkType.listfeed)
        {
            foreach (var link in links)
            {
                if (link.Rel.EndsLike(feedType.ToString()))
                    return link.HRef.Content;
            }
            return null;
        }
    }
}
