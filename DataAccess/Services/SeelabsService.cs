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
        private string _token => _httpRequest.ReadToken("seelabs_token");
        public SeelabsService(IHttpContextAccessor httpRequest)
        {
            _httpRequest = httpRequest.HttpContext.Request;
            _client = new HttpRequestHelper("https://see.labs.telkomuniversity.ac.id/praktikum/index.php");
        }
        public async Task<dynamic> Score(ScoreListGroupRequest data)
        {
            SetToken();
            var request = new
            {
                aksi = (int?)(data.Group == null ? null : 1),
                search = true,
                hari_id = data.Day + 7,
                shift_id = data.Shift,
                kelompok_id = data.Group,
                praktikum_id = 168,
                terms = "on"
            }.ToDictionary();

            bool isInput = false;
            if (data is ScoreInputRequest input)
            {
                isInput = true;
                request.AddKey("modulid", input.Module.ToString());
                request.AddKey("tanggal", input.Date.ToString("yyyy-MM-dd"));
                request.AddKey("submit", "submit");
                input.GetScores(request);
            }

            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/inputnilaipraktikum", request);
            var responseHtml = response.ParseHtml();
            var table = responseHtml.QuerySelector("table");

            if (data.Group == null)
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
                            names = new();
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
            else{
                var result = responseHtml.QuerySelector("#myAlert b")?.TextContent;
                if (result == "Gagal")
                    throw new ArgumentException("Failed input score!");
                return result;
            }
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
        public async Task<dynamic> Login(string nim, string password, string role)
        {
            var data = new
            {
                user_nim = nim,
                user_pass = password,
                login_ass = role == "Assistant" ? 2 : 1,
                submit = ""
            };
            HttpResponseMessage response = await _client.HtmlPost("/home/loginprak", data);
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
        private void SetToken()
        {
            _client.AddHeader("Cookie", "ci_session=" + _token);
        }
    }
}
