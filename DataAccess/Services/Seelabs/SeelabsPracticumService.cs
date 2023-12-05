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
    public class SeelabsPracticumService : SeelabsBase
    {
        public SeelabsPracticumService(IHttpContextAccessor httpRequest, IConfiguration configuration) : base(httpRequest, configuration, "praktikum") { }
        public async Task<List<SeelabsInputOverviewResponse>> InputOverview(SeelabsScoreListRequest request)
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/ceknilaimasuk", request);
            var responseHtml = await response.ParseHtml();
            var tr = responseHtml.QuerySelector("table")?.QuerySelectorAll("tr").Skip(1);
            if (tr.ElementAt(0).Children.Length > 1)
            {
                string assistantName = "";
                return tr.Select(td =>
                {
                    assistantName = td.QuerySelectorAll("td[rowspan]").ElementAtOrDefault(1)?.InnerHtml ?? assistantName;
                    return new InputOverviewList(assistantName, td);
                })
                .GroupBy(x => new { x.Mentor, x.Group })
                .Select(group => new SeelabsInputOverviewResponse
                {
                    Mentor = group.Key.Mentor,
                    Group = group.Key.Group,
                    StudentList = group.Select(x => new InputOverviewDetail(x)).ToList()
                }).OrderBy(x => x.Group).ToList();
            }
            return null;
        }
        public async Task<List<SeelabsBAPResponse>> BAP(SeelabsBAPRequest request)
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/bap", request);
            var responseHtml = await response.ParseHtml();
            var tr = responseHtml.QuerySelector("table")?.QuerySelectorAll("tr").Skip(1);
            if (tr.ElementAt(0).Children.Length > 1)
            {
                return tr.Select(td => new SeelabsBAPResponse(td)).ToList();
            }
            return null;
        }
        public async Task<List<SeelabsScoreStudentResponse>> ScoreStudent()
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlPost("/page/nilai_user", new SeelabsScoreStudentRequest());
            var responseHtml = await response.ParseHtml();
            return responseHtml.QuerySelector("table")?.QuerySelectorAll("tr").Skip(1).Select(td => new SeelabsScoreStudentResponse(td)).ToList();
        }
        public async Task<List<SeelabsListGroupResponse>> ScoreList(SeelabsScoreListRequest request)
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/lihat_inputnilai", request);
            var responseHtml = await response.ParseHtml();
            var table = responseHtml.QuerySelector("table");
            var result = TableToJson(table, 6);
            return result.Select(group => new SeelabsListGroupResponse(group)).ToList();
        }
        public async Task<SeelabsScoreResultResponse> ScoreResult(SeelabsScoreResultRequest request)
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/lihat_inputnilai", request);
            var responseHtml = await response.ParseHtml();
            var tr = responseHtml.QuerySelector("table")?.QuerySelectorAll("tr")?.Skip(2);
            return new SeelabsScoreResultResponse(tr);
        }
        public async Task<SeelabsScoreDetailResponse> ScoreDetail(SeelabsScoreDetailRequest request)
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/lihat_inputnilai", request);
            var responseHtml = await response.ParseHtml();
            var tr = responseHtml.QuerySelector("table")?.QuerySelectorAll("tr")?.Skip(2);
            var module = int.Parse((responseHtml.QuerySelector("select[name='modulid']") as IHtmlSelectElement).Value);
            var group = int.Parse((responseHtml.QuerySelector("input[name='kelompok_id']") as IHtmlInputElement).Value);
            return new SeelabsScoreDetailResponse(module, group, tr);
        }
        public async Task<string> ScoreDelete(SeelabsScoreDeleteRequest request)
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/lihat_inputnilai", request);
            var responseHtml = await response.ParseHtml();
            var result = responseHtml.QuerySelector("#myAlert b")?.TextContent;
            if (result == "Gagal")
                throw new ArgumentException("Failed delete score!");
            return result;
        }
        public async Task<string> ScoreUpdate(ScoreUpdateRequest data)
        {
            SetToken();
            SeelabsScoreUpdateRequest update = new(data);
            var request = update.ToDictionary().ToList();
            data.GetScores(request);
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/lihat_inputnilai", request);
            var responseHtml = await response.ParseHtml();
            var result = responseHtml.QuerySelector("#myAlert b")?.TextContent;
            if (result == "Gagal")
                throw new ArgumentException("Failed update score!");
            return result;
        }
        public async Task<List<SeelabsListGroupResponse>> GroupList(SeelabsListGroupRequest request)
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/inputnilaipraktikum", request);
            var responseHtml = await response.ParseHtml();
            var table = responseHtml.QuerySelector("table");
            var result = TableToJson(table, 4);
            return result.Select(group => new SeelabsListGroupResponse(group)).ToList();
        }
        public async Task<List<SeelabsDetailGroupResponse>> GroupDetail(SeelabsDetailGroupRequest request)
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/inputnilaipraktikum", request);
            var responseHtml = await response.ParseHtml();
            var table = responseHtml.QuerySelector("table");
            return table?.QuerySelectorAll("tr").Skip(2).Select(td => new SeelabsDetailGroupResponse(td)).ToList();
        }
        public async Task<string> ScoreInput(ScoreInputRequest data)
        {
            SetToken();
            SeelabsScoreInputRequest input = new(data);
            var request = input.ToDictionary().ToList();
            data.GetScores(request);
            HttpResponseMessage response = await _client.HtmlPost("/pageasisten/inputnilaipraktikum", request);
            var responseHtml = await response.ParseHtml();
            var result = responseHtml.QuerySelector("#myAlert b")?.TextContent;
            if (result == "Gagal")
                throw new ArgumentException("Failed input score!");
            return result;
        }
        public async Task<List<SeelabsScheduleResponse>> Schedule()
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlGet("/pageasisten/datajadwal");
            var responseHtml = await response.ParseHtml();
            return responseHtml.QuerySelector("table")?.QuerySelectorAll("tr").Skip(1).Select(td => new SeelabsScheduleResponse(td)).ToList();
        }
        public async Task<SeelabsLoginResponse> Login(SeelabsLoginRequest data)
        {
            HttpResponseMessage response = await _client.HtmlPost("/home/loginprak", data);
            var responseHtml = await response.ParseHtml();
            var name = responseHtml.QuerySelector(".navbar-link")?.TextContent;
            Cookie cookie = name != null ? _client.GetCookie("ci_session") : null;
            return new SeelabsLoginResponse(name, cookie);
        }
    }
}
