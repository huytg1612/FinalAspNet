using ProductMN.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductMN.Domains
{
    public class CategoryDomain
    {
        private DbContext _context;
        public CategoryDomain(DbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            var repo = new CategoryRepo(_context);
            return repo.GetAll();
        }
    }
}