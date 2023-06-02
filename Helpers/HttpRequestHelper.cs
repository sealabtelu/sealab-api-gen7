using SealabAPI.DataAccess.Extensions;
using System.Dynamic;
using System.Net;
using System.Text;
using System.Web;

namespace SealabAPI.Helpers
{
    public class HttpRequestHelper
    {
        private readonly HttpClient _client;
        private readonly HttpClientHandler _handler = new();
        public string BaseAddress => _client.BaseAddress.ToString();
        public HttpRequestHelper(string BaseAddress)
        {
            _handler.CookieContainer = new();
            _handler.UseCookies = true;
            _client = new HttpClient(_handler);
            _client.BaseAddress = new Uri(BaseAddress);
            // Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
        }
        public async Task<HttpResponseMessage> HtmlPost(string endpoint, List<KeyValuePair<string, string>> data)
        {
            string url = BaseAddress + endpoint;
            return await _client.PostAsync(url, new FormUrlEncodedContent(data));
        }
        public HttpResponseMessage HtmlPost(string endpoint, object data)
        {
            string url = _client.BaseAddress.ToString() + endpoint;
            return _client.PostAsync(url, data.ToFormData()).Result;
        }
        public async Task<HttpResponseMessage> HtmlGet(string endpoint)
        {
            string url = _client.BaseAddress.ToString() + endpoint;
            return await _client.GetAsync(url);
        }
        //public dynamic ApiGet<T>(string url, T data)
        //{
        //    return ApiGet(QueryString(url, data));
        //}
        //public dynamic ApiGet(string url)
        //{
        //    HttpResponseMessage response = Client.GetAsync(url).Result;
        //    return JsonSerializer.Deserialize<ExpandoObject>(
        //        response.Content.ReadAsStringAsync().Result
        //    );
        //}
        //public dynamic ApiPost<T>(string endpoint, T data)
        //{
        //    string url = Client.BaseAddress.ToString() + endpoint;
        //    HttpResponseMessage response = Client.PostAsync(url, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")).Result;
        //    return JsonSerializer.Deserialize<ExpandoObject>(
        //        response.Content.ReadAsStringAsync().Result
        //    );
        //}
        public void AddHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key, value);
        }
        public Cookie GetCookie(string name)
        {
            return _handler.CookieContainer.GetCookies(new Uri(BaseAddress)).Cast<Cookie>().FirstOrDefault(c => c.Name == name);
        }
        public string QueryString<T>(string url, T data)
        {
            var properties = from property in data.GetType().GetProperties()
                             where property.GetValue(data, null) != null
                             select property.Name + "=" + HttpUtility.UrlEncode(property.GetValue(data, null).ToString());
            url += "?" + string.Join("&", properties.ToArray());
            return url;
        }
    }
}
