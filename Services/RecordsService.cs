using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using lapGen.persistance;

namespace lapGen.Services
{
    public interface IRecordsService
    {
        Record CreateRecord(int time, int driverID, int carID, int lapNumber);
        void UpdateRecord(int recordID,int time, int driverID, int carID, int lapNumber);
        void DeleteRecord(int RecordID);
    }

    public class RecordsService : IRecordsService
    {
        private readonly DataCon dataCon;
        public RecordsService(DataCon dataCon)
        {
            this.dataCon = dataCon;
        }
        public Record CreateRecord(int time, int driverID, int carID, int lapNumber)
        {
            Record rec = new Record()
            {
                timeInSeconds = time,
                driverID = driverID,
                carID = carID
            };
            //check for existing lap to update lap or create new
            var lapInDb = dataCon.Laps.FirstOrDefault(l => l.lapNumber == lapNumber);
            if (lapInDb != null)
            {
                //update lap
                if(rec.lap!=null)
                {
                    rec.lap.ID = lapInDb.ID;
                }
                else
                {
                    rec.lap = new Lap();
                    rec.lap.ID = lapInDb.ID;
                }
                
                lapInDb.records.Add(rec);
            }
            else
            {
                //create new
                Lap newLap = new Lap()
                {
                    lapNumber = lapNumber
                };
                newLap.records.Add(rec);
                dataCon.Laps.Add(newLap);
            }
            dataCon.SaveChanges();
            return rec;
        }
        public void UpdateRecord(int recordID,int time, int driverID, int carID, int lapNumber)
        {
            foreach (Record rec in dataCon.Records)
            {
                //check for existing lap to update lap or create new
                var lapInDb = dataCon.Laps.FirstOrDefault(l => l.lapNumber == lapNumber);
                if(rec.ID == recordID)
                {
                    rec.timeInSeconds = time;
                    rec.carID = carID;
                    rec.driverID = driverID;
                    if (lapInDb != null)
                    {
                        //update lap
                        if(rec.lap == null)
                        {
                            rec.lap = lapInDb;
                        }
                        rec.lap.ID = lapInDb.ID;
                        lapInDb.records.Add(rec);
                    }
                    else
                    {
                        //create new Lap
                        Lap newLap = new Lap()
                        {
                            lapNumber = lapNumber
                        };
                        newLap.records.Add(rec);
                        dataCon.Laps.Add(newLap);
                    }
                    dataCon.SaveChanges();
                }
            } 
        }
        public void DeleteRecord(int RecordID)
        {
            var item = dataCon.Records.Where(r => r.ID == RecordID).ToList();
            dataCon.Records.Remove(item[0]);
            dataCon.SaveChanges();
        }
    }
}