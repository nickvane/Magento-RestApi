using System;
using Magento.RestApi.Models;
using Newtonsoft.Json;

namespace Magento.RestApi.Json
{
    public class StockItemConverter : BaseConverter<StockItem>
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var stockItem = value as StockItem;

            writer.WriteStartObject();

            WriteProperty(stockItem, p => p.qty, true, writer, serializer);
            WriteProperty(stockItem, p => p.item_id, false, writer, serializer);
            WriteProperty(stockItem, p => p.product_id, false, writer, serializer);
            WriteProperty(stockItem, p => p.stock_id, false, writer, serializer);
            WriteProperty(stockItem, p => p.backorders, false, writer, serializer);
            WriteProperty(stockItem, p => p.enable_qty_increments, false, writer, serializer);
            WriteProperty(stockItem, p => p.is_decimal_divided, false, writer, serializer);
            WriteProperty(stockItem, p => p.is_in_stock, false, writer, serializer);
            WriteProperty(stockItem, p => p.is_qty_decimal, false, writer, serializer);
            WriteProperty(stockItem, p => p.manage_stock, false, writer, serializer);
            WriteProperty(stockItem, p => p.max_sale_qty, false, writer, serializer);
            WriteProperty(stockItem, p => p.min_qty, false, writer, serializer);
            WriteProperty(stockItem, p => p.min_sale_qty, false, writer, serializer);
            WriteProperty(stockItem, p => p.notify_stock_qty, false, writer, serializer);
            WriteProperty(stockItem, p => p.qty_increments, false, writer, serializer);
            WriteProperty(stockItem, p => p.use_config_backorders, false, writer, serializer);
            WriteProperty(stockItem, p => p.use_config_enable_qty_inc, false, writer, serializer);
            WriteProperty(stockItem, p => p.use_config_manage_stock, false, writer, serializer);
            WriteProperty(stockItem, p => p.use_config_max_sale_qty, false, writer, serializer);
            WriteProperty(stockItem, p => p.use_config_min_qty, false, writer, serializer);
            WriteProperty(stockItem, p => p.use_config_min_sale_qty, false, writer, serializer);
            WriteProperty(stockItem, p => p.use_config_notify_stock_qty, false, writer, serializer);
            WriteProperty(stockItem, p => p.use_config_qty_increments, false, writer, serializer);
            WriteProperty(stockItem, p => p.stock_status_changed_auto, false, writer, serializer);
            WriteProperty(stockItem, p => p.low_stock_date, false, writer, serializer);
            
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var stockItem = existingValue as StockItem ?? new StockItem();

            serializer.Populate(reader, stockItem);

            stockItem.StartTracking();
            return stockItem;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(StockData));
        }
    }
}
