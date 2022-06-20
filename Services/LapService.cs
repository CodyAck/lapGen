using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using lapGen.persistance;

namespace lapGen.Services
{
    public interface ILapService
    {
        Lap CreateLap(int LapNumber);
        void DeleteLap(int LapID);
        Lap UpdateLap(int LapID, int LapNumber);
    }

    public class LapService : ILapService
    {
        private readonly DataCon dataCon;
        public LapService(DataCon dataCon)
        {
            this.dataCon = dataCon;

        }
        public Lap CreateLap(int LapNumber)
        {
            Lap Lap = new Lap()
            {
                lapNumber = LapNumber
            };
            dataCon.Laps.Add(Lap);
            dataCon.SaveChanges();
            return Lap;
        }
        public Lap UpdateLap(int LapID, int LapNumber)
        {
            Lap UpdatedLapRef = new Lap();
            foreach (Lap item in dataCon.Laps)
            {
                if (item.ID == LapID)
                {
                    item.lapNumber = LapNumber;
                    UpdatedLapRef = item;
                }
            }
            dataCon.SaveChanges();
            return UpdatedLapRef;
        }
        public void DeleteLap(int LapID)
        {
            var item = dataCon.Laps.Where(l => l.ID == LapID).ToList();
            dataCon.Laps.Remove(item[0]);
            dataCon.SaveChanges();
        }
    }
}