using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IMessageService _messageService;
        IRentalService _rentalService;

        public CarManager(ICarDal carDal, IRentalService rentalService,IMessageService messageService)
        {
            _carDal = carDal;
            _rentalService = rentalService;
            _messageService = messageService;
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(this._carDal.GetAll(), _messageService.CarListed);
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(this._carDal.Get(c => c.Id == id), _messageService.CarFound);
        }
        public IDataResult<CarDetailDto> GetDetailById(int id)
        {
            var result = _carDal.GetCarDetails().Where(c => c.Id == id).SingleOrDefault();
            result.Rentable = _rentalService.CheckIfCarRented(result.Id).Success;
            return new SuccessDataResult<CarDetailDto>(result, _messageService.CarFound);
        }

        public IDataResult<List<CarDetailDto>> GetDetailsByColorIdAndBrandId(int colorId, int brandId)
        {

            brandId = brandId.GetType() != typeof(string) ? brandId : 0;
            colorId = colorId.GetType() != typeof(string) ? colorId : 0;
            colorId = colorId > 0 ? colorId : 0;
            brandId = brandId > 0 ? brandId : 0;

            var filteredCars = _carDal.GetCarDetails().Where(c => c.ColorId == colorId && c.BrandId == brandId).ToList();
            if (filteredCars.Count > 0) {
                return new SuccessDataResult<List<CarDetailDto>>(filteredCars, "Belirtilen filtre için sonuçlar"); 
            }
            return new ErrorDataResult<List<CarDetailDto>>(default, "Belirtilen filtre için sonuç bulunamadı.");

        }

        //[SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            //ValidationTool.Validate(new CarValidator(), car);
            this._carDal.Add(car);
            return new SuccessResult(_messageService.CarAdded);

        }

        public IResult Delete(Car car)
        {
            this._carDal.Delete(car);
            return new SuccessResult(_messageService.CarDeleted);
        }

        public IResult Update(Car car)
        {
            this._carDal.Update(car);
            return new SuccessResult(_messageService.CarUpdated);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(this._carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(this._carDal.GetAll(c => c.ColorId == colorId));
        }
        public IDataResult<List<CarDetailDto>> GetCarsDetailByColorId(int colorId)
        {
            var cars = GetCarDetails().Data;
            var carsFromColorId = GetCarsByColorId(colorId).Data;
            var result = from car in cars
                         join carFromColorId in carsFromColorId
                         on car.Id equals carFromColorId.Id
                         select new CarDetailDto
                         {
                             Id = car.Id,
                             BrandId = car.BrandId,
                             BrandName = car.BrandName,
                             ColorId = car.ColorId,
                             ColorName = car.ColorName,
                             DailyPrice = car.DailyPrice,
                             Description = car.Description,
                             Model = car.Model,
                             ModelYear = car.ModelYear
                         };
            return new SuccessDataResult<List<CarDetailDto>>(result.ToList());
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByBrandId(int brandId)
        {
            var cars = GetCarDetails().Data;
            var carsFromBrandId = GetCarsByBrandId(brandId).Data;
            var result = from car in cars
                         join carFromBrandId in carsFromBrandId
                         on car.Id equals carFromBrandId.Id
                         select new CarDetailDto
                         {
                             Id = car.Id,
                             BrandId = car.BrandId,
                             BrandName = car.BrandName,
                             ColorId = car.ColorId,
                             ColorName = car.ColorName,
                             DailyPrice = car.DailyPrice,
                             Description = car.Description,
                             Model = car.Model,
                             ModelYear = car.ModelYear
                         };
            return new SuccessDataResult<List<CarDetailDto>>(result.ToList());
        }

        public IDataResult<List<Car>> GetCarsByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(this._carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), _messageService.CarListed);
        }



        public IResult CheckIfCarExists(int carId)
        {
            var result = _carDal.GetAll(c => c.Id == carId).Any();
            if (!result)
            {
                return new ErrorResult("Böyle bir araç mevcut değil.");
            }
            return new SuccessResult();
        }
    }
}
