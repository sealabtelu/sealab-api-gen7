using AngleSharp.Html.Parser;
using SealabAPI.Helpers;
using System.Dynamic;

namespace SealabAPI.DataAccess.Extensions
{
    public static class ObjectExtension
    {
        public static AngleSharp.Html.Dom.IHtmlDocument ParseHtml(this HttpResponseMessage response)
        {
            return new HtmlParser().ParseDocument(response.Content.ReadAsStringAsync().Result);
        }
        public static List<KeyValuePair<string, string>> ToDict(this object data)
        {
            return data.GetType().GetProperties()
                .Select(x => new KeyValuePair<string, string>(x.Name, x.GetValue(data)?.ToString())).ToList();
        }
        public static List<KeyValuePair<string, string>> AddKey(this List<KeyValuePair<string, string>> data, string key, string value)
        {
            data.Add(new KeyValuePair<string, string>(key, value));
            return data;
        }
        public static FormUrlEncodedContent ToFormData(this object data)
        {
            return new FormUrlEncodedContent(data.ToDict());
        }
        public static dynamic ToExpando(this object data)
        {
            var expando = new ExpandoObject() as IDictionary<string, object>;
            foreach (var property in data.GetType().GetProperties())
                expando.Add(property.Name, property.GetValue(data));
            return expando;
        }

    }
}
