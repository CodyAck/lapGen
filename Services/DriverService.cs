using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using lapGen.persistance;

namespace lapGen.Services
{
    public interface IDriverService
    {
        Driver CreateDriver(string driverName, int driverRank);
        Driver DeleteDriver(int DriverID);
        Driver UpdateDriver(int driverID, string DriverName, int driverRank);
    }

    public class DriverService : IDriverService
    {
        private readonly DataCon dataCon;
        public DriverService(DataCon dataCon)
        {
            this.dataCon = dataCon;

        }
        public Driver CreateDriver(string driverName, int driverRank)
        {
            Driver Driver = new Driver()
            {
                active = true,
                name = driverName,
                rank = driverRank
            };
            dataCon.Drivers.Add(Driver);
            dataCon.SaveChanges();
            return Driver;
        }
        public Driver UpdateDriver(int driverID, string DriverName, int driverRank)
        {
            Driver UpdatedDriverRef = new Driver();
            foreach (Driver item in dataCon.Drivers)
            {
                if (item.ID == driverID)
                {
                    item.rank = driverRank;
                    item.name = DriverName;

                    UpdatedDriverRef = item;
                }
            }
            dataCon.SaveChanges();
            return UpdatedDriverRef;
        }
        public Driver DeleteDriver(int DriverID)
        {
            Driver DeletedDriverRef = new Driver();
            foreach (Driver item in dataCon.Drivers)
            {
                if (item.ID == DriverID)
                {
                    item.active = false;
                    DeletedDriverRef = item;
                }
            }
            dataCon.SaveChanges();
            return DeletedDriverRef;
        }
    }
}