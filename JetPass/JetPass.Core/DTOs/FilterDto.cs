using System;

namespace JetPass.Core.DTOs
{
    public class FilterDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool SortByPrice { get; set; } = true;
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
    }
}