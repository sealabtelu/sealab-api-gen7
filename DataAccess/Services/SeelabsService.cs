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
    public class SeelabsService
    {
        private readonly HttpRequestHelper _client;
        private readonly HttpRequest _httpRequest;
        private string _token => _httpRequest.GetSeelabsToken();
        public SeelabsService(IHttpContextAccessor httpRequest)
        {
            _httpRequest = httpRequest.HttpContext.Request;
            _client = new HttpRequestHelper("https://see.labs.telkomuniversity.ac.id/praktikum/index.php");
        }
        public async Task<dynamic> Score(ScoreListGroupRequest data)
        {
            _client.AddHeader("Cookie", "ci_session=" + _token);

            var request = new
            {
                aksi = (int?)(data.Group == null ? null : 1),
                search = true,
                hari_id = data.Day + 7,
                shift_id = data.Shift,
                kelompok_id = data.Group,
                praktikum_id = 168,
                terms = "on"
            }.ToDict();
            bool isInput = false;
            if (data is ScoreInputRequest input)
            {
                isInput = true;
                request.AddKey("modulid", input.Module.ToString());
                request.AddKey("tanggal", input.Date.ToString("yyyy-MM-dd"));
                request.AddKey("submit", "submit");
                // request.Add(new KeyValuePair<string, string>("modulid", data.module.ToString()));
                // request.Add(new KeyValuePair<string, string>("tanggal", data.date.ToString("yyyy-MM-dd")));
                // request.Add(new KeyValuePair<string, string>("submit", "submit"));

                var score = new Dictionary<string, dynamic>{
                    {"uid[]", input.GetUid()},
                    {"status[]", input.GetStatus()},
                    {"TP[]", input.GetTP()},
                    {"TA[]", input.GetTA()},
                    {"D1[]", input.GetD()},
                    {"D2[]", input.GetD()},
                    {"D3[]", input.GetD()},
                    {"D4[]", input.GetD()},
                    {"I1[]", input.GetI1()},
                    {"I2[]", input.GetI2()}
                };
                foreach (var item in score)
                    foreach (string arr in item.Value)
                        request.AddKey(item.Key, arr);
            }

            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/inputnilaipraktikum", request);

            var table = response.ParseHtml().QuerySelector("table");

            if (data.Group == null)
            {
                int count = 0, id_group = 0, counter = 0;
                string span;
                var columns = table?.QuerySelectorAll("td");
                var result = new List<object>(); //new ExpandoObject() as IDictionary<string, object>;
                List<string> names = new();
                foreach (var item in columns)
                {
                    if ((span = item.GetAttribute("rowspan")) != null)
                    {
                        count = int.Parse(span);
                        counter++;
                        if (counter == 4)
                        {
                            counter = 0;
                            id_group = int.Parse(item.QuerySelector("input[name='kelompok_id']").GetAttribute("value"));
                        }
                    }
                    else
                    {
                        names.Add(item.TextContent.Substring(2));
                        if (names.Count == count)
                        {
                            result.Add(new { id_group, names });
                            names = new List<string>();
                        }
                    }
                }
                return result;
            }
            else if (data.Group != null && !isInput)
                return table?.QuerySelectorAll("tr").Skip(2).Select(td => new
                {
                    uid = td.QuerySelector("input").GetAttribute("value"),
                    name = td.Children[1].TextContent,
                });
            else
                return response.ParseHtml().QuerySelector("#myAlert b").TextContent;
        }

        public async Task<dynamic> Schedule()
        {
            _client.AddHeader("Cookie", "ci_session=" + _token);
            HttpResponseMessage response = await _client.HtmlGet("/pageasisten/datajadwal");

            return response.ParseHtml().QuerySelector("table")?
                .QuerySelectorAll("tr").Skip(1).Select(td => new
                {
                    nim = td.Children[1].TextContent,
                    name = td.Children[2].TextContent,
                    day = td.Children[3].TextContent,
                    shift = td.Children[4].TextContent
                });
        }

        public dynamic Login(string nim, string password, string role)
        {
            var data = new
            {
                user_nim = nim,
                user_pass = password,
                login_ass = role == "Assistant" ? 2 : 1,
                submit = ""
            };
            HttpResponseMessage response = _client.HtmlPost("/home/loginprak", data);
            var name = response.ParseHtml().QuerySelector(".navbar-link")?.TextContent;
            Cookie cookie = name != null ? _client.GetCookie("ci_session") : null;
            return new
            {
                valid = name != null,
                name,
                token = cookie?.Value,
                expires = cookie?.Expires,
            }.ToExpando();
        }
    }
}
