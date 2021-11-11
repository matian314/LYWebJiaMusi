using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYWeb.Helpers
{
    public class DataStatus
    {
        public string AEIStatus { get; set; }
        public string DimensionStatus { get; set; }
        public string ScrapeStatus { get; set; }
        public string InspectionStatus { get; set; }
        public DataStatus()
        {
            AEIStatus = "T";
            DimensionStatus = "T";
            ScrapeStatus = "T";
            InspectionStatus = "T";
        }
    }
}