using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelpers;
using Core.Utilities.Helpers.FileHelpers.Images;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {

        ICarImageDal _carImageDal;
        ICarService _carService;
        IImageFileHelper _fileHelper;
        public CarImageManager(ICarImageDal carImageDal, ICarService carService, IImageFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _carService = carService;
            _fileHelper = fileHelper;
        }

        //[SecuredOperation("carimage.add,admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(int carId, IFormFile formFile)
        {
            var result = BusinessRules.Run(CheckIfMaxImageExceeded(carId), _carService.CheckIfCarExists(carId));
            if (result != null)
            {
                return result;
            }

            var uploadResult = _fileHelper.Add(formFile);
            if (!uploadResult.Success)
            {
                return uploadResult;
            }


            var carImage = new CarImage
            {
                CarId = carId,
                Date = DateTime.Now,
                ImagePath = uploadResult.Data
            };
            _carImageDal.Add(carImage);
            return new SuccessResult("Görsel başarı ile yüklendi");
        }

        public IResult Delete(CarImage carImage)
        {
            var image = Get(carImage);
            if (image.Data == null)
            {
                return new ErrorResult("Böyle bir resim yok");
            }

            var deleteResult = _fileHelper.Delete(image.Data.ImagePath);
            if (!deleteResult.Success)
            {
                return deleteResult;
            }

            _carImageDal.Delete(carImage);
            return new SuccessResult();
            
        }

        public IDataResult<CarImage> Get(CarImage carImage)
        {
            var result = _carImageDal.Get(ci => ci.Id == carImage.Id);
            return new SuccessDataResult<CarImage>(result);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            var images = GetAllImagesOrDefaultByCarId(carId).Data;
            return new SuccessDataResult<List<CarImage>>(images, "Arabaya ait resimler getirildi.");
        }

        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        /*******/
        private IDataResult<List<CarImage>> GetAllImagesOrDefaultByCarId(int carId)
        {
            var images = _carImageDal.GetAll(ci => ci.CarId == carId);
            var result = images.Count > 0 ? images
                :
                new List<CarImage> {
                    new CarImage { CarId = carId, ImagePath = GetDefaultImage(), Date = DateTime.Now }
                };

            return new SuccessDataResult<List<CarImage>>(result);
        }

        private IResult CheckIfMaxImageExceeded(int carId)
        {
            var count = GetByCarId(carId).Data.Count;
            if (count >= 5)
            {
                return new ErrorResult("Daha fazla resim ekleyemezsiniz.");
            }
            return new SuccessResult();
        }

        private string GetDefaultImage()
        {
            return @"\wwwroot\Uploads\Images\default.png";
        }

    }
}
