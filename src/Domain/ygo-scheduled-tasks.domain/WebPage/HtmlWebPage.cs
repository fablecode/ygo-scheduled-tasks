﻿using System;
using HtmlAgilityPack;
using ygo_scheduled_tasks.core.WebPage;

namespace ygo_scheduled_tasks.domain.WebPage
{
    public class HtmlWebPage : IHtmlWebPage
    {
        public HtmlDocument Load(string webPageUrl)
        {
            return Load(new Uri(webPageUrl));
        }

        public HtmlDocument Load(Uri webPageUrl)
        {
            var htmlWeb = new HtmlWeb
            {
                AutoDetectEncoding = true,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246"
            };

            htmlWeb.PreRequest += request =>
            {
                request.CookieContainer = new System.Net.CookieContainer();
                return true;
            };

            return htmlWeb.Load(webPageUrl);
        }
    }
}