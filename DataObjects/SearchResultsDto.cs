using System;
using System.Collections.Generic;
using System.Text;

namespace Readr.Models
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
        }
        public String Text => ResultText.Text;
        public string TextBoundingBox => ResultText.BoundingBox;
        public string LineBoundingBox => ResultLine.BoundingBox;
        public string RegionBoundingBox => ResultRegion.BoundingBox;
        public string Language => ResultDetail.Language;
        public double TextAngle => ResultDetail.TextAngle;
        public string Orientation => ResultDetail.Orientation;
        public List<SearchResultText> LineText => ResultLine.Text;
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
