using Business.Concrete;
using Business.Constants;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            CarManager carManager = new CarManager(new InMemoryCarDal());
            var cars = carManager.GetAll();
            var cheapestCar = carManager.MostCheap();
            Console.WriteLine(cheapestCar.Description);
            
            */

            //BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Add(new Brand { BrandName = "Fiat" });
            //brandManager.Add(new Brand { BrandName = "Toyota" });
            //brandManager.Add(new Brand { BrandName = "Mercedes" });

            //brandManager.Add(new Brand { BrandName = "BMW" });
            //brandManager.Add(new Brand { BrandName = "Audi" });
            //brandManager.Add(new Brand { BrandName = "Tesla" });


            //carManager.Add(new Car { BrandId = 3, ColorId = 4, DailyPrice = 125, ModelYear = 2017, Description = "aciklama" });

            //CarManagerTest(new EfCarDal());
            //BrandManagerTest(new EfBrandDal());

            //ColorManagerTest(new EfColorDal());

            //CarDetailsTest();
            //CarManager carManager = new CarManager(new EfCarDal(), new CarCheckManager());
            //var result = carManager.Add(new Car { BrandId = 2, ColorId = 4, DailyPrice = 75, ModelYear = 1999, Description = "99 Model Canavar" });
            //Console.WriteLine(result.Message);

            //KullanıcıEkle(new User { FirstName = "Joe", LastName = "BIDEN", Email = "turpgibibaskan@gmail.com", Password = "beyazsaray" });

            //YeniMusteriEkle(new Customer { UserId = 4003, CompanyName = "Develoopers" });
            //AracKirala(new Rental { CarId = 3, CustomerId = 1002, RentDate = DateTime.Now });

            //CarManager carManager = new CarManager(new EfCarDal(), new TurkishMessage());
            //var result = carManager.GetById(1);
            //Console.WriteLine(result.Data.Model);
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var res = rentalManager.Add(new Rental { CarId = 3, CustomerId = 1002, RentDate = DateTime.Now });
            Console.WriteLine(res.Message);

        }

        private static void AracKirala(Rental rental)
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result = rentalManager.Add(rental);
            Console.WriteLine(result.Message);
        }

        private static void KiralanmisAraclariGoster()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result_ = rentalManager.GetRentedCarDetails();
            foreach (var item in result_.Data)
            {
                Console.WriteLine("{0} {1} - {2} - {3} - {4} ", item.FirstName, item.LastName, item.CompanyName, item.Model, item.ModelYear);
            }
        }

        private static void YeniMusteriEkle(Customer customer)
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            var result = customerManager.Add(customer);
            Console.WriteLine(result.Success);
        }

        private static void KullanıcıEkle(User user)
        {
            UserManager userManager = new UserManager(new EfUserDal());
            var result = userManager.Add(user);
            Console.WriteLine(result.Success);
        }

        private static void CarDetailsTest()
        {
            CarManager carManager = new CarManager(new EfCarDal(), new TurkishMessage());
            var result = carManager.GetCarDetails();
            foreach (var item in result.Data)
            {
                Console.WriteLine("{0}-{1}-{2}-{3}-{4}", item.Id, item.BrandName, item.ColorName, item.DailyPrice, item.Description);
            }
        }

        private static void ColorManagerTest(IColorDal colorDal)
        {
            ColorManager colorManager = new ColorManager(colorDal);
            var result = colorManager.GetAll();

            foreach (var item in result.Data)
            {
                Console.WriteLine("{0}-{1}", item.Id, item.ColorName);
            }
        }

        static void BrandManagerTest(IBrandDal brandDal)
        {
            BrandManager brandManager = new BrandManager(brandDal);
            var result = brandManager.GetByBrandNameLike("tes");
            foreach (var item in result.Data)
            {
                Console.WriteLine("{0}-{1}", item.Id, item.BrandName);
            }
        }

        static void CarManagerTest(ICarDal carDal)
        {
            CarManager carManager = new CarManager(carDal, new TurkishMessage());
            var cars = carManager.GetAll();
            foreach (var car in cars.Data)
            {
                Console.WriteLine("{0}-{1}", car.Id, car.DailyPrice);
            }
        }
    }
}
