using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClosedLoop.Web.Helpers
{
    public static class UriHelpers
    {
        public static string Combine(params string[] uriParts)
        {
            if (uriParts == null || !uriParts.Any())
            {
                return string.Empty;
            }
            else
            {
                var trims = new[] {'\\', '/'};
                var uri = (uriParts[0] ?? string.Empty).TrimEnd(trims);
                for (var i = 1; i < uriParts.Count(); i++)
                {
                    uri = string.Format("{0}/{1}", uri.TrimEnd(trims), (uriParts[i] ?? string.Empty).TrimStart(trims));
                }
                return uri;
            }
        }
    }
}