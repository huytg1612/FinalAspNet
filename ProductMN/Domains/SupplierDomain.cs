using ProductMN.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductMN.Domains
{
    public class SupplierDomain
    {
        private DbContext _context;

        public SupplierDomain(DbContext context)
        {
            _context = context;
        }

        public IQueryable<Supplier> GetAll()
        {
            var repo = new SupplierRepo(_context);
            return repo.GetAll();
        }
    }
}