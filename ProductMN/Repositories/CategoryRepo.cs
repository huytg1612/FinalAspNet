using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductMN.Repositories
{
    public class CategoryRepo : BaseRepository<Category, int>
    {
        public CategoryRepo(DbContext context) : base(context)
        {
        }

        public override Category FindById(int id)
        {
            return _set.FirstOrDefault(c => c.CategoryID == id);
        }

        public override IQueryable<Category> FindByName(string name)
        {
            return _set.Where(s => s.CategoryName.Equals(name));
        }

        public override Category Update(int k, Category e)
        {
            throw new NotImplementedException();
        }
    }
}