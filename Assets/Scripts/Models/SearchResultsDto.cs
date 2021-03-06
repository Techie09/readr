﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readr.Assets.Scripts.Models
{
    public class SearchResultsDto
    {
        public SearchResultsDto()
        {
            ResultDetails = new List<SearchResultDetails>();
        }

        //should store results to view search history
        //public ObjectId Id
        public List<SearchResultDetails> ResultDetails { get; set; }
    }

    public class SearchResultDetails
    {
        public SearchResultDetails(SearchResultText resultText,
            SearchResultLine resultLine,
            SearchResultRegion resultRegion,
            SearchResultDetail resultDetail,
            int lineNumber,
            int position)
        {
            ResultText = resultText;
            ResultLine = resultLine;
            ResultRegion = resultRegion;
            ResultDetail = resultDetail;
            LineNumber = lineNumber;
            Position = position;
        }
        public int LineNumber { get; set; }
        public int Position { get; set; }
        public SearchResultText ResultText { get; set; }
        public SearchResultLine ResultLine { get; set; }
        public SearchResultRegion ResultRegion { get; set; }
        public SearchResultDetail ResultDetail { get; set; }
    }

    public class SearchResultDetail
    {
        public string Language { get; set; }
        public double TextAngle { get; set; }
        public string Orientation { get; set; }
        //public ObjectId UserSessionOrigin { get; set; }
        public List<SearchResultRegion> Regions { get; set; }
    }

    public class SearchResultRegion
    {
        public string BoundingBox { get; set; }
        public List<SearchResultLine> Lines { get; set; }
    }

    public class SearchResultLine
    {
        public string BoundingBox { get; set; }
        public List<SearchResultText> Text { get; set; }
    }

    public class SearchResultText
    {
        public string BoundingBox { get; set; }
        public string Text { get; set; }
    }
}
