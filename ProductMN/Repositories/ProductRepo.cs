using ProductMN.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductMN.Repositories
{
    public class ProductRepo : BaseRepository<Product, int>
    {
        public ProductRepo(DbContext context) : base(context)
        {
        }

        public override Product FindById(int id)
        {
            return _set.FirstOrDefault(p => p.ProductID == id);
        }

        public override IQueryable<Product> FindByName(string name)
        {
            return _set.Where(p => p.ProductName.Contains(name));
        }

        public override Product Update(int id, Product e)
        {
            var product = _set.FirstOrDefault(p => p.ProductID == id);

            product.ProductName = e.ProductName;
            product.SupplierID = e.SupplierID;
            product.CategoryID = e.CategoryID;
            product.UnitPrice = e.UnitPrice;
            product.UnitsInStock = e.UnitsInStock;
            product.UnitsOnOrder = e.UnitsOnOrder;
            product.ReorderLevel = e.ReorderLevel;
            product.Discontinued = e.Discontinued;
            product.QuantityPerUnit = e.QuantityPerUnit;

            return product;
        }

        public ProductQueryModels QueryFilter(string name, decimal? min_price, decimal? max_price, int page, int take,int? supId)
        {
            var list = _set.Where(p => p.ProductName.Contains(name) && (p.UnitPrice >= min_price && p.UnitPrice <= max_price)).ToList();

            if (supId != null)
            {
                list = list.Where(p => p.SupplierID == supId).ToList();
            }

            var query = list.OrderBy(p => p.ProductName)
                .Skip(page * take)
                .Take(take);

            var total_count = list.Count;

            var model = new ProductQueryModels
            {
                list = query.ToList(),
                total_count = total_count
            };

            return model;
        }
    }
}