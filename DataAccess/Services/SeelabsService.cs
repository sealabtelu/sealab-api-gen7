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
        public async Task<dynamic> BAP(DateTime date)
        {
            SetToken();
            var request = new
            {
                first_date = date.ToString("yyyy-MM-dd"),
            }.ToDictionary();
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/bap", request);
            var responseHtml = await response.ParseHtml();
            var tr = responseHtml.QuerySelector("table")?.QuerySelectorAll("tr").Skip(1);
            if (tr.ElementAt(0).Children.Length > 1)
            {
                return tr.Select(td => new
                {
                    date = td.Children[1].TextContent,
                    shift = td.Children[2].TextContent,
                    module = td.Children[3].TextContent,
                });
            }
            return null;
        }
        public async Task<dynamic> ScoreResult(ScoreResultRequest data, int? action = null)
        {
            SetToken();
            var request = new
            {
                pilihan = action,
                search = true,
                modul_id = data.Module
            }.ToDictionary();
            bool isUpdate = false;
            if (data.Group != null)
            {
                request.AddKey("modul", data.Module.ToString());
                request.AddKey("kelompok_id", data.Group.ToString());
                if (data is ScoreUpdateRequest update)
                {
                    isUpdate = true;
                    request.AddKey("modulid", update.Module.ToString());
                    request.AddKey("praktikum_id", "114");
                    request.AddKey("editinput", "submit");
                    update.GetScores(request);
                }

            }
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/lihat_inputnilai", request);
            var responseHtml = await response.ParseHtml();
            if (action == 1)
            {
                if (isUpdate)
                {
                    var result = responseHtml.QuerySelector("#myAlert b")?.TextContent;
                    if (result == "Gagal")
                        throw new ArgumentException("Failed update score!");
                    return result;
                }
                else
                {
                    var tr = responseHtml.QuerySelector("table")?.QuerySelectorAll("tr")?.Skip(2);
                    return new
                    {
                        Module = (responseHtml.QuerySelector("select[name='modulid']") as IHtmlSelectElement).Value,
                        Scores = tr.Select(td => new
                        {
                            Name = td.Children[1].TextContent,
                            Uid = td.QuerySelector("input[name='id[]']")?.GetAttribute("value"),
                            Status = td.QuerySelector("option")?.GetAttribute("value") == "1",
                            TP = int.Parse(td.QuerySelector("input[name='TP[]']")?.GetAttribute("value")),
                            TA = int.Parse(td.QuerySelector("input[name='TA[]']")?.GetAttribute("value")),
                            D = int.Parse(td.QuerySelector("input[name='D1[]']")?.GetAttribute("value")),
                            I1 = int.Parse(td.QuerySelector("input[name='I1[]']")?.GetAttribute("value")),
                            I2 = int.Parse(td.QuerySelector("input[name='I2[]']")?.GetAttribute("value"))
                        })
                    };

                }
            }
            else if (action == 2)
            {
                var tr = responseHtml.QuerySelector("table")?.QuerySelectorAll("tr")?.Skip(2);
                return new
                {
                    Module = tr.ElementAt(0).Children[2].TextContent,
                    Shift = tr.ElementAt(0).Children[12].TextContent,
                    Scores = tr.Select(td => new
                    {
                        Name = td.Children[1].TextContent,
                        TP = td.Children[3].TextContent,
                        TA = td.Children[4].TextContent,
                        D1 = td.Children[5].TextContent,
                        D2 = td.Children[6].TextContent,
                        D3 = td.Children[7].TextContent,
                        D4 = td.Children[8].TextContent,
                        I1 = td.Children[9].TextContent,
                        I2 = td.Children[10].TextContent,
                        Date = td.Children[11].TextContent
                    })
                };
            }
            else if (action == 3)
            {
                var result = responseHtml.QuerySelector("#myAlert b")?.TextContent;
                return result;
            }
            else
            {
                var table = responseHtml.QuerySelector("table");
                var result = TableToJson(table, 6);
                return result;
            }
        }
        public async Task<dynamic> ScoreInput(ScoreListGroupRequest data)
        {
            SetToken();
            var request = new
            {
                aksi = (int?)(data.Group == null ? null : 1),
                search = true,
                hari_id = data.Day,
                shift_id = data.Shift,
                kelompok_id = data.Group,
                praktikum_id = 114,
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
            var responseHtml = await response.ParseHtml();
            var table = responseHtml.QuerySelector("table");

            if (data.Group == null)
            {
                var result = TableToJson(table, 4);
                return result.Select(group => new
                {
                    id_group = group.id_group,
                    names = ((List<string>)group.names).Select(name => name.Substring(2)).ToArray()
                });
            }
            else if (data.Group != null && !isInput)
                return table?.QuerySelectorAll("tr").Skip(2).Select(td => new
                {
                    uid = td.QuerySelector("input").GetAttribute("value"),
                    name = td.Children[1].TextContent,
                });
            else
            {
                var result = responseHtml.QuerySelector("#myAlert b")?.TextContent;
                if (result == "Gagal")
                    throw new ArgumentException("Failed input score!");
                return result;
            }
        }
        public async Task<dynamic> Schedule()
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlGet("/pageasisten/datajadwal");
            var responseHtml = await response.ParseHtml();
            return responseHtml.QuerySelector("table")?
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
            var responseHtml = await response.ParseHtml();
            var name = responseHtml.QuerySelector(".navbar-link")?.TextContent;
            Cookie cookie = name != null ? _client.GetCookie("ci_session") : null;
            return new
            {
                valid = name != null,
                name,
                token = cookie?.Value,
                expires = cookie?.Expires,
            }.ToExpando();
        }
        private List<dynamic> TableToJson(IElement table, int rowCount)
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
        private void SetToken()
        {
            _client.AddHeader("Cookie", "ci_session=" + _token);
        }
    }
}
