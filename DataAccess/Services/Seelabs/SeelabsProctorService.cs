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
    public class SeelabsProctorService : SeelabsBase
    {
        protected override string _token => _httpRequest.ReadToken("proctor_token");
        public SeelabsProctorService(IHttpContextAccessor httpRequest, IConfiguration configuration) : base(httpRequest, configuration, "pengawas") { }
        public async Task<List<SeelabsProctorResponse>> Schedule()
        {
            SetToken();
            HttpResponseMessage response = await _client.HtmlGet("/page/jadwal");
            var responseHtml = await response.ParseHtml();
            var tr = responseHtml.QuerySelector("table")?.QuerySelectorAll("tr").Skip(1);
            if (tr.ElementAt(0).Children.Length > 1)
            {
                return tr.Select(td => new SeelabsProctorResponse(td)).ToList();
            }
            return null;
        }

        public async Task<SeelabsLoginResponse> Login(SeelabsLoginRequest data)
        {
            HttpResponseMessage response = await _client.HtmlPost("/home/login", data);
            var responseHtml = await response.ParseHtml();
            var name = responseHtml.QuerySelector(".navbar-link")?.TextContent;
            Cookie cookie = name != null ? _client.GetCookie("ci_session") : null;
            return new SeelabsLoginResponse(name, cookie);
        }
    }
}
