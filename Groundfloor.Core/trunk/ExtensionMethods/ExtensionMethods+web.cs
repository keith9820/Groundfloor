using System.Collections.Generic;

namespace System.Web.Mvc.Html
{
    public static class ExtensionMethods
    {
        public static MvcHtmlString UsStatesList(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            var StateDictionary = new List<SelectListItem> {
                                        #region load the states
                                        new SelectListItem{Text="ALABAMA", Value="AL"},
                                        new SelectListItem{Text="ALASKA", Value="AK"},
                                        new SelectListItem{Text="AMERICAN SAMOA", Value="AS"},
                                        new SelectListItem{Text="ARIZONA ", Value="AZ"},
                                        new SelectListItem{Text="ARKANSAS", Value="AR"},
                                        new SelectListItem{Text="CALIFORNIA ", Value="CA"},
                                        new SelectListItem{Text="COLORADO ", Value="CO"},
                                        new SelectListItem{Text="CONNECTICUT", Value="CT"},
                                        new SelectListItem{Text="DELAWARE", Value="DE"},
                                        new SelectListItem{Text="DISTRICT OF COLUMBIA", Value="DC"},
                                        new SelectListItem{Text="FEDERATED STATES OF MICRONESIA", Value="FM"},
                                        new SelectListItem{Text="FLORIDA", Value="FL"},
                                        new SelectListItem{Text="GEORGIA", Value="GA"},
                                        new SelectListItem{Text="GUAM ", Value="GU"},
                                        new SelectListItem{Text="HAWAII", Value="HI"},
                                        new SelectListItem{Text="IDAHO", Value="ID"},
                                        new SelectListItem{Text="ILLINOIS", Value="IL"},
                                        new SelectListItem{Text="INDIANA", Value="IN"},
                                        new SelectListItem{Text="IOWA", Value="IA"},
                                        new SelectListItem{Text="KANSAS", Value="KS"},
                                        new SelectListItem{Text="KENTUCKY", Value="KY"},
                                        new SelectListItem{Text="LOUISIANA", Value="LA"},
                                        new SelectListItem{Text="MAINE", Value="ME"},
                                        new SelectListItem{Text="MARSHALL ISLANDS", Value="MH"},
                                        new SelectListItem{Text="MARYLAND", Value="MD"},
                                        new SelectListItem{Text="MASSACHUSETTS", Value="MA"},
                                        new SelectListItem{Text="MICHIGAN", Value="MI"},
                                        new SelectListItem{Text="MINNESOTA", Value="MN"},
                                        new SelectListItem{Text="MISSISSIPPI", Value="MS"},
                                        new SelectListItem{Text="MISSOURI", Value="MO"},
                                        new SelectListItem{Text="MONTANA", Value="MT"},
                                        new SelectListItem{Text="NEBRASKA", Value="NE"},
                                        new SelectListItem{Text="NEVADA", Value="NV"},
                                        new SelectListItem{Text="NEW HAMPSHIRE", Value="NH"},
                                        new SelectListItem{Text="NEW JERSEY", Value="NJ"},
                                        new SelectListItem{Text="NEW MEXICO", Value="NM"},
                                        new SelectListItem{Text="NEW YORK", Value="NY"},
                                        new SelectListItem{Text="NORTH CAROLINA", Value="NC"},
                                        new SelectListItem{Text="NORTH DAKOTA", Value="ND"},
                                        new SelectListItem{Text="NORTHERN MARIANA ISLANDS", Value="MP"},
                                        new SelectListItem{Text="OHIO", Value="OH"},
                                        new SelectListItem{Text="OKLAHOMA", Value="OK"},
                                        new SelectListItem{Text="OREGON", Value="OR"},
                                        new SelectListItem{Text="PALAU", Value="PW"},
                                        new SelectListItem{Text="PENNSYLVANIA", Value="PA"},
                                        new SelectListItem{Text="PUERTO RICO", Value="PR"},
                                        new SelectListItem{Text="RHODE ISLAND", Value="RI"},
                                        new SelectListItem{Text="SOUTH CAROLINA", Value="SC"},
                                        new SelectListItem{Text="SOUTH DAKOTA", Value="SD"},
                                        new SelectListItem{Text="TENNESSEE", Value="TN"},
                                        new SelectListItem{Text="TEXAS", Value="TX"},
                                        new SelectListItem{Text="UTAH", Value="UT"},
                                        new SelectListItem{Text="VERMONT", Value="VT"},
                                        new SelectListItem{Text="VIRGIN ISLANDS", Value="VI"},
                                        new SelectListItem{Text="VIRGINIA", Value="VA"},
                                        new SelectListItem{Text="WASHINGTON", Value="WA"},
                                        new SelectListItem{Text="WEST VIRGINIA", Value="WV"},
                                        new SelectListItem{Text="WISCONSIN", Value="WI"},
                                        new SelectListItem{Text="WYOMING", Value="WY"}
                                        #endregion
                                     };
            return htmlHelper.DropDownList(name, StateDictionary, htmlAttributes);
        }
    }
}


namespace System.Web.Mvc
{
    public static class UrlHelperExtension
    {
        public static string Absolute(this UrlHelper url, string relativeOrAbsolute)
        {
            var uri = new Uri(relativeOrAbsolute, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri)
            {
                return relativeOrAbsolute;
            }
            // At this point, we know the url is relative.
            return VirtualPathUtility.ToAbsolute(relativeOrAbsolute);
        }
    }
}