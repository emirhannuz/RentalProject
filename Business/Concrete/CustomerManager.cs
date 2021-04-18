using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            this._customerDal = customerDal;
        }
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult();
        }

        public IResult Delete(Customer customer)
        {
            this._customerDal.Delete(customer);
            return new SuccessResult();
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(this._customerDal.GetAll());
        }

        public IDataResult<List<Customer>> GetByUserId(int id)
        {
            return new SuccessDataResult<List<Customer>>(this._customerDal.GetAll(c => c.Id == id));
        }

        public IResult Update(Customer customer)
        {
            this._customerDal.Update(customer);
            return new SuccessResult();
        }
    }
}
