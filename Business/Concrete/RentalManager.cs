using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        public IResult Add(Rental rental)
        {
            /*bu kısım fonksiyonlara ayrılmalı*/
            //var cars = this.GetAllByCarId(rental.CarId);
            //var userRentedCars = this.GetByCustomerId(rental.CustomerId);
            //foreach (var item in userRentedCars.Data)
            //{
            //    if (!item.ReturnDate.HasValue)
            //    {
            //        return new ErrorResult("Müşteri adına kiralanmış araç mevcut.");
            //    }
            //}
            //foreach (var car in cars.Data)
            //{
            //    if (!car.ReturnDate.HasValue)
            //    {
            //        return new ErrorResult("Araç Zaten Kiranlanmış.");
            //    }
            //}
            /**/

            var result = BusinessRules.Run(
                CheckIfCarRented(rental.CarId),
                CheckIfCustomerRentLimitExceeded(rental)
            );
            if(result != null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult("Araç Kiralandı");

        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(this._rentalDal.GetAll());
        }

        public IDataResult<Rental> GetByCarId(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId).LastOrDefault();
            return new SuccessDataResult<Rental>(result);
        }
        public IDataResult<List<Rental>> GetAllByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId));
        }

        public IDataResult<List<Rental>> GetByCustomerId(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == id));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(),"Kiralama Detayları Getirildi.");
        }

        public IDataResult<List<RentalDetailDto>> GetRentedCarDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentedCarDetails());
        }

        public IResult CheckIfCarRented(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId && (r.ReturnDate >= DateTime.Now || r.ReturnDate == null)).Any();
            if (result)
            {
                return new ErrorResult("Araç kiralanmis.");
            }
            return new SuccessResult();
        }

        private IResult CheckIfCustomerRentLimitExceeded(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CustomerId == rental.CustomerId && (r.ReturnDate >= DateTime.Now || r.ReturnDate == null));
            if (result.Count >= 1)
            {
                return new ErrorResult("Bu musteri daha fazla arac kiralayamaz.");
            }
            return new SuccessResult();
        }
    }
}
