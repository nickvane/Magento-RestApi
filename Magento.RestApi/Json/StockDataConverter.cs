using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    public class StockDataConverter : BaseConverter<StockData>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var stockData = value as StockData;

            writer.WriteStartObject();

            WriteProperty(stockData, p => p.backorders, false, writer, serializer);
            WriteProperty(stockData, p => p.enable_qty_increments, false, writer, serializer);
            WriteProperty(stockData, p => p.is_decimal_divided, false, writer, serializer);
            WriteProperty(stockData, p => p.is_in_stock, false, writer, serializer);
            WriteProperty(stockData, p => p.is_qty_decimal, false, writer, serializer);
            WriteProperty(stockData, p => p.manage_stock, false, writer, serializer);
            WriteProperty(stockData, p => p.max_sale_qty, false, writer, serializer);
            WriteProperty(stockData, p => p.min_qty, false, writer, serializer);
            WriteProperty(stockData, p => p.min_sale_qty, false, writer, serializer);
            WriteProperty(stockData, p => p.notify_stock_qty, false, writer, serializer);
            WriteProperty(stockData, p => p.qty, false, writer, serializer);
            WriteProperty(stockData, p => p.qty_increments, false, writer, serializer);
            WriteProperty(stockData, p => p.use_config_backorders, false, writer, serializer);
            WriteProperty(stockData, p => p.use_config_enable_qty_inc, false, writer, serializer);
            WriteProperty(stockData, p => p.use_config_manage_stock, false, writer, serializer);
            WriteProperty(stockData, p => p.use_config_max_sale_qty, false, writer, serializer);
            WriteProperty(stockData, p => p.use_config_min_qty, false, writer, serializer);
            WriteProperty(stockData, p => p.use_config_min_sale_qty, false, writer, serializer);
            WriteProperty(stockData, p => p.use_config_notify_stock_qty, false, writer, serializer);
            WriteProperty(stockData, p => p.use_config_qty_increments, false, writer, serializer);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var stockData = existingValue as StockData ?? new StockData();

            serializer.Populate(reader, stockData);

            stockData.StartTracking();
            return stockData;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(StockData));
        }
    }
}
