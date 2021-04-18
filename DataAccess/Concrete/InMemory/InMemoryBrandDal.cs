using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory
{
    class InMemoryBrandDal : IBrandDal
    {

        List<Brand> _brands;

        public InMemoryBrandDal()
        {
            this._brands = new List<Brand> {
                new Brand(){Id=1,BrandName="Kia"},
                new Brand(){Id=2,BrandName="Toyota"},
                new Brand(){Id=3,BrandName="Tesla"},
            };
        }
        public void Add(Brand entity)
        {
            this._brands.Add(entity);
        }

        public void Delete(Brand entity)
        {
            throw new NotImplementedException();
        }

        public Brand Get(Expression<Func<Brand, bool>> filter)
        {
            throw new NotImplementedException();
        }


        public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Brand entity)
        {
            throw new NotImplementedException();
        }
    }
}
