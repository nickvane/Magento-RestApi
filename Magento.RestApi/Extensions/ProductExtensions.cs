using System.Collections.Generic;
using System.Linq;
using Magento.RestApi.Models;

namespace Magento.RestApi.Extensions
{
    public static class ProductExtensions
    {
        public static void AddAttributes(this Product product, dynamic dynamicProduct)
        {
            foreach (var item in dynamicProduct)
            {
                var property = product.GetType().GetProperty(item.Name);
                if (property == null)
                {
                    if (product.Attributes.ContainsKey(item.Name)) product.Attributes.Remove(item.Name);
                    string value = null;
                    if (item.Value != null) value = item.Value.ToString();
                    product.Attributes.Add(item.Name, value);
                }
            }
        }

        //public static Product Changes(this Product changedProduct, Product originalProduct)
        //{
        //    var hasChanges = false;
        //    var product = new Product();

        //    hasChanges = Updater.UpdateChanges(originalProduct, changedProduct, product);

        //    if (changedProduct.stock_data != null)
        //    {
        //        var stockData = new StockData();
        //        product.stock_data = originalProduct.stock_data == null 
        //            ? changedProduct.stock_data 
        //            : Updater.UpdateChanges(originalProduct.stock_data, changedProduct.stock_data, stockData) ? stockData : null;
        //    }
        //    if (changedProduct.group_price != null)
        //    {
        //        foreach (var groupPrice in changedProduct.group_price)
        //        {
        //            if (originalProduct.group_price != null)
        //            {
        //                var price = originalProduct.group_price.SingleOrDefault(p => p.cust_group == groupPrice.cust_group
        //                                                                             && p.price == groupPrice.price
        //                                                                             && p.website_id == groupPrice.website_id);
        //                if (price == null)
        //                {
        //                    if(product.group_price == null) product.group_price = new List<GroupPrice>();
        //                    product.group_price.Add(groupPrice);
        //                    hasChanges = true;
        //                }
        //            }
        //        }
        //    }
        //    if (changedProduct.tier_price != null)
        //    {
        //        foreach (var tierPrice in changedProduct.tier_price)
        //        {
        //            if (originalProduct.tier_price != null)
        //            {
        //                var price = originalProduct.tier_price.SingleOrDefault(p => p.cust_group == tierPrice.cust_group
        //                                                                             && p.price == tierPrice.price
        //                                                                             && p.website_id == tierPrice.website_id
        //                                                                             && p.price_qty == tierPrice.price_qty);
        //                if (price == null)
        //                {
        //                    if (product.tier_price == null) product.tier_price = new List<TierPrice>();
        //                    product.tier_price.Add(tierPrice);
        //                    hasChanges = true;
        //                }
        //            }
        //        }
        //    }
        //    foreach (var attribute in changedProduct.Attributes)
        //    {
        //        if (!originalProduct.Attributes.ContainsKey(attribute.Key))
        //        {
        //            product.Attributes.Add(attribute.Key, attribute.Value);
        //            hasChanges = true;
        //        }
        //        else
        //        {
        //            if (attribute.Value != originalProduct.Attributes[attribute.Key])
        //            {
        //                product.Attributes.Add(attribute.Key, attribute.Value);
        //                hasChanges = true;
        //            }
        //        }
        //    }
        //    // required properties
        //    product.entity_id = changedProduct.entity_id;
        //    product.sku = changedProduct.sku;
        //    product.visibility = changedProduct.visibility;
        //    product.status = changedProduct.status;
        //    product.name = changedProduct.name;
        //    product.price = changedProduct.price;
        //    product.weight = changedProduct.weight;
        //    product.tax_class_id = changedProduct.tax_class_id;
        //    product.description = changedProduct.description;

        //    return !hasChanges ? null : product;
        //}

    }
}
