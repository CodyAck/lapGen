using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using lapGen.persistance;

namespace lapGen.Services
{
    public interface ICarService
    {
        Car CreateCar(int carNum, string carName);
        Car DeleteCar(int carID);
        Car UpdateCar(int carID, int carNum, string carName);
    }

    public class CarService : ICarService
    {
        private readonly DataCon dataCon;
        public CarService(DataCon dataCon)
        {
            this.dataCon = dataCon;

        }
        public Car CreateCar(int carNum, string carName)
        {
            Car car = new Car()
            {
                active = true,
                number = carNum,
                name = carName
            };
            dataCon.Cars.Add(car);
            dataCon.SaveChanges();
            return car;
        }
        public Car UpdateCar(int carID, int carNum, string carName)
        {
            Car UpdatedCarRef = new Car();
            foreach (Car item in dataCon.Cars)
            {
                if (item.ID == carID)
                {
                    item.number = carNum;
                    item.name = carName;

                    UpdatedCarRef = item;
                }
            }
            dataCon.SaveChanges();
            return UpdatedCarRef;
        }
        public Car DeleteCar(int carID)
        {
            Car DeletedCarRef = new Car();
            foreach (Car item in dataCon.Cars)
            {
                if (item.ID == carID)
                {
                    item.active = false;
                    DeletedCarRef = item;
                }
            }
            dataCon.SaveChanges();
            return DeletedCarRef;
        }
    }
}