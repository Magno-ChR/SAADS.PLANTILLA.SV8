using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SAADS.PLANTILLA.SV8.Helpers
{
    public static class NavigationManagerExtensions
    {
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string? QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }

        public static void DeleteQueryString(this NavigationManager navigationManager, string key)
        {
            var query = navigationManager.QueryString();
            query.Remove(key);

            var parameters = new Dictionary<string, string>();
            foreach (var item in query.AllKeys)
            {
                if (!string.IsNullOrEmpty(item) && query != null)
                {
                    var valores = query[item]!;

                    if (valores != null)
                    {
                        parameters.Add(item, valores);
                    }

                }

            }

            // build url


            if (parameters.Count > 0)
            {
                var url = navigationManager.Uri.Split('?')[0];
                url += "?";
                foreach (var item in parameters)
                {
                    url += $"{item.Key}={item.Value}&";
                }
                url = url.Substring(0, url.Length - 1);

                navigationManager.NavigateTo(url);
            }
            else
            {
                var url = navigationManager.Uri.Split('?')[0];
                navigationManager.NavigateTo(url);
            }



        }

        public static string GetUrlDeleteQueryString(this NavigationManager navigationManager, string key, bool htmlEncode = false)
        {
            var query = navigationManager.QueryString();
            query.Remove(key);

            var parameters = new Dictionary<string, string>();
            foreach (var item in query.AllKeys)
            {
                if (!string.IsNullOrEmpty(item) && query != null)
                {
                    var valores = query[item]!;

                    if (valores != null)
                    {
                        parameters.Add(item, valores);
                    }

                }

            }

            // build url
            var url = navigationManager.Uri.Split('?')[0];
            if (parameters.Count > 0)
            {

                url += "?";
                foreach (var item in parameters)
                {
                    url += $"{item.Key}={item.Value}&";
                }
                url = url.Substring(0, url.Length - 1);
                return htmlEncode ? HttpUtility.HtmlEncode(url) : url;
            }




            return htmlEncode ? HttpUtility.HtmlEncode(url) : url;
        }

        // return tru if the key exists in the query string
        public static bool HasQueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key] != null;
        }

        //return string encodehtml page parameter string

        public static string GetEncodeHtml(this NavigationManager navigationManager, string url)
        {
            return HttpUtility.HtmlEncode(url);
        }

    }
}
