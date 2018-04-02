﻿using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace ygo_scheduled_tasks.core.WebPage
{
    public interface ICardHtmlDocument
    {
        void Load(string url);
        void Load(Uri url);
        HtmlNode ProfileElement();
        string ProfileImageUrl();
        string ProfileCardDescription();
    }
}