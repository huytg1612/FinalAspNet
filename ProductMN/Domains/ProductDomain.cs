using ProductMN.Models.ViewModels;
using ProductMN.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductMN.Domains
{
    public class ProductDomain
    {
        private DbContext _context;

        public ProductDomain(DbContext context)
        {
            _context = context;
        }

        public Product Create(Product product)
        {
            var repo = new ProductRepo(_context);
            return repo.Create(product);
        }

        public IQueryable<Product> GetAll()
        {
            var repo = new ProductRepo(_context);
            return repo.GetAll();
        }

        public Product FindById(int id)
        {
            var repo = new ProductRepo(_context);
            return repo.FindById(id);
        }

        public Product Update(int id,Product product)
        {
            var repo = new ProductRepo(_context);
            return repo.Update(id,product);
        }

        public IQueryable<Product> FindByName(string name)
        {
            var repo = new ProductRepo(_context);
            return repo.FindByName(name);
        }

        public ProductQueryModels QueryFilter(string name,decimal? min_price,decimal? max_price,int page,int take,int? supId)
        {
            var repo = new ProductRepo(_context);
            return repo.QueryFilter(name, min_price, max_price,page,take,supId);
        }

        public Product Delete(Product product)
        {
            var repo = new ProductRepo(_context);
            return repo.Delete(product);
        }
    }
}