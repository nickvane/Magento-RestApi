using System.Collections.Generic;

namespace Magento.RestApi.Models
{
    public class Filter
    {
        public Filter()
        {
            Page = 1;
            PageSize = 10;
            SortDirection = SortDirection.asc;
            FilterExpressions = new List<FilterExpression>();
        }

        public int PageSize { get; set; }
        public int Page { get; set; }
        public SortDirection SortDirection { get; set; }
        public string SortField { get; set; }
        public List<FilterExpression> FilterExpressions { get; set; }
    }

    public enum SortDirection
    {
        asc,
        desc
    }
}
