using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using MongoDB.Bson;

namespace Readr.Models
{
    public class SessionDetail : IBsonModel
    {
        public ObjectId Id { get; set; }
        public ObjectId SessionId { get; set; }
        public OcrResult PrintedTextResult { get; set; }
        public ObjectId ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    //public class ScannedText
    //{
    //    public string boundingBox { get; set; }
    //    public string text { get; set; }
    //}

    //public class ScannedLine
    //{
    //    public string boundingBox { get; set; }
    //    public List<ScannedText> words { get; set; }
    //}

    //public class Region
    //{
    //    public string boundingBox { get; set; }
    //    public List<ScannedLine> lines { get; set; }
    //}

    //public class ScanData
    //{
    //    public string language { get; set; }
    //    public double textAngle { get; set; }
    //    public string orientation { get; set; }
    //    public List<Region> regions { get; set; }
    //}
}
