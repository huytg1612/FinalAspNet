using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductMN.Repositories
{
    public class SupplierRepo : BaseRepository<Supplier, int>
    {
        public SupplierRepo(DbContext context) : base(context)
        {
        }

        public override Supplier FindById(int id)
        {
            return _set.FirstOrDefault(s => s.SupplierID == id);
        }

        public override IQueryable<Supplier> FindByName(string name)
        {
            return _set.Where(s => s.CompanyName.Equals(name));
        }

        public override Supplier Update(int k, Supplier e)
        {
            throw new NotImplementedException();
        }
    }
}