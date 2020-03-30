using System;
using System.Collections.Generic;
using System.Text;

namespace Readr.Models
{
    public class VisionAnalysisTagData
    {
        public string name;
        public float confidence;
    }

    public class VisionAnalysisResultData
    {
        public VisionAnalysisTagData[] tags;
        public string requestId;
        public object metadata;
    }
}
