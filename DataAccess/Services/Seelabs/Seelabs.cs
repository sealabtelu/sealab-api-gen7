using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.DataAccess.Models;
using SealabAPI.Helpers;
using System.Dynamic;
using System.Net;

namespace SealabAPI.DataAccess.Services
{
    public class SeelabsBase
    {
        protected readonly HttpRequestHelper _client;
        protected readonly HttpRequest _httpRequest;
        protected readonly int _idLab;
        protected virtual string _token => _httpRequest.ReadToken("practicum_token");
        public SeelabsBase(IHttpContextAccessor httpRequest, IConfiguration configuration, string endpoint)
        {
            _httpRequest = httpRequest.HttpContext.Request;
            _client = new HttpRequestHelper($"{configuration["SeelabsUrl"]}/{endpoint}/index.php");
            _idLab = int.Parse(configuration["LabId"]);
        }
        protected List<dynamic> TableToJson(IElement table, int rowCount)
        {
            int count = 0, id_group = 0, counter = 0;
            string span;
            var columns = table?.QuerySelectorAll("td");
            List<object> result = new();
            List<string> names = new();
            foreach (var item in columns)
            {
                if ((span = item.GetAttribute("rowspan")) != null)
                {
                    count = int.Parse(span);
                    counter++;
                    if (counter == rowCount)
                    {
                        counter = 0;
                        id_group = int.Parse(item.QuerySelector("input[name='kelompok_id']").GetAttribute("value"));
                    }
                }
                else
                {
                    names.Add(item.TextContent);
                    if (names.Count == count)
                    {
                        result.Add(new { id_group, names });
                        names = new();
                    }
                }
            }
            return result;
        }
        protected void SetToken()
        {
            _client.AddHeader("Cookie", "ci_session=" + _token);
        }
    }
}
