using System;
using System.Collections.Generic;
using System.Text;

namespace Readr.Models
{
    public class SessionDetailDto
    {
        public string Id { get; set; }
        public string SessionId { get; set; }

        /// <summary>
        /// This is a Json Serialized object that represents a result from Azure Computer Vision OCR Services.
        /// It is derived from  <see cref="Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models.OcrResult"/>
        /// </summary>
        public string PrintedTextResult { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
