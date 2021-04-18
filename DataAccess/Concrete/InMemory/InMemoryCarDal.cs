using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            this._cars = new List<Car> {
                new Car(){Description="Car1 desc", DailyPrice=1},
                new Car(){Description="Car2 desc", DailyPrice=2},
                new Car(){Description="Car3 desc", DailyPrice=3},
                new Car(){Description="Car4 desc", DailyPrice=4},
                new Car(){Description="Car5 desc", DailyPrice=5},
                new Car(){Description="Car6 desc", DailyPrice=6},
            };
        }

        public void Add(Car car)
        {
            this._cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = this._cars.SingleOrDefault(c => c.Id == car.Id);
            this._cars.Remove(carToDelete);
        }

        public void Update(Car car)
        {
            Car carToUpdate = this._cars.SingleOrDefault(c => c.Id == car.Id);
            this.Delete(carToUpdate);
            this.Add(car);

        }

        public List<Car> GetAll()
        {
            return this._cars;
        }

        public Car GetById(int id)
        {
            return this._cars.Find(c => c.Id == id);
        }

        public Car MostCheap()
        {
            var cheapPrice = this._cars.Min(c => c.DailyPrice);
            return this._cars.SingleOrDefault(car => car.DailyPrice == cheapPrice);
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }
    }
}
