namespace Magento.RestApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterExpression
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="expressionOperator"></param>
        /// <param name="fieldValue"></param>
        public FilterExpression(string fieldName, ExpressionOperator expressionOperator, string fieldValue)
        {
            FieldName = fieldName;
            ExpressionOperator = expressionOperator;
            FieldValue = fieldValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ExpressionOperator ExpressionOperator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FieldValue { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ExpressionOperator
    {
        /// <summary>
        /// 
        /// </summary>
        neq,
        /// <summary>
        /// 
        /// </summary>
        lt,
        /// <summary>
        /// 
        /// </summary>
        gt,
        /// <summary>
        /// 
        /// </summary>
        @in,
        /// <summary>
        /// 
        /// </summary>
        nin,
        /// <summary>
        /// 
        /// </summary>
        like
    }
}
