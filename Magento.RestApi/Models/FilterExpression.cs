namespace Magento.RestApi.Models
{
    public class FilterExpression
    {
        public FilterExpression(string fieldName, ExpressionOperator expressionOperator, string fieldValue)
        {
            FieldName = fieldName;
            ExpressionOperator = expressionOperator;
            FieldValue = fieldValue;
        }

        public string FieldName { get; set; }
        public ExpressionOperator ExpressionOperator { get; set; }
        public string FieldValue { get; set; }
    }

    public enum ExpressionOperator
    {
        neq,
        lt,
        gt,
        @in,
        nin 
    }
}
