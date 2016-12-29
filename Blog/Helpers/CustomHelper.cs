using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class CustomHelper
    {

        public static string BaseUrl(this UrlHelper url)
        {
            /*
             * Url.BaseUrl()  helper oluştuldu.
             * Çıktısı http://loalhost:5200 gibi olabilir
             * System.Web.HttpContext.Current.Request
             */
            var request = HttpContext.Current.Request;
            var BaseUrl = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);
            return BaseUrl;
        }
        public static string TypeHttp(this UrlHelper url)
        {
            /*
             * Url.BaseUrl()  helper oluştuldu.
             * Çıktısı http://loalhost:5200 gibi olabilir
             * System.Web.HttpContext.Current.Request
             */
            var request = HttpContext.Current.Request;
            var BaseUrl = string.Format("{0}", request.Url.Scheme);
            return BaseUrl;
        }
        public static string GetAction(this UrlHelper url)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }
        public static string GetController(this UrlHelper url)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }
        public static string GetSeoLink(this UrlHelper url)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("seolink"))
                return (string)routeValues["seolink"];
            else if (HttpContext.Current.Request.QueryString.AllKeys.Contains("seolink"))
                return HttpContext.Current.Request.QueryString["seolink"];

            return string.Empty;
        }
    }
}