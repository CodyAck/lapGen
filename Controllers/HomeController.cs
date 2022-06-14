using System.Diagnostics;
using lapGen.persistance;
using lapGen.Utils.Interfaces;
using lapGen.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using lapGen.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using lapGen.Objects;

namespace lapGen.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataCon dbContext;
    public bool LapGenToggle = false;
    //private readonly ITest test;

    public HomeController(ILogger<HomeController> logger, DataCon dbContext)
    {
        _logger = logger;
        this.dbContext = dbContext;
        //this.test = test;
    }

    public IActionResult Crud()
    {
        var cars = dbContext.Cars.ToList();
        var drivers = dbContext.Drivers.ToList();
        //HttpContext.Session.SetObjectAsJson("Drivers",drivers);
        var laps = dbContext.Laps.ToList();
        //var raceReport = dbContext.Races.Include(r => r.track.laps).ToList();
        var viewMod = new GeneralDataViewModel();
        foreach(Lap item in laps)
        {
            LapsDisplay temp = new LapsDisplay();
            temp.lap_I_D = item.ID;
            temp.lapTime = item.recordedLapTime;
            var tempSelectString = dbContext.Cars.Where(car => car.ID == item.carID).Select(car => car.name).ToList();
            temp.carName = tempSelectString[0].ToString();

            var tempSelectInt = dbContext.Cars.Where(car => car.ID == item.carID).Select(car => car.number).ToList();
            temp.carNum =  Convert.ToInt32(tempSelectInt[0]);

            tempSelectString = dbContext.Drivers.Where(driver => driver.ID == item.recordedDriverID).Select(driver => driver.name).ToList();
            temp.recordedDriverName = tempSelectString[0].ToString();
            viewMod.displayLaps.Add(temp);
        }
        viewMod.cars = cars;
        viewMod.drivers = drivers;
        viewMod.laps = laps;
        
        //viewMod.driverNames = viewMod.GetDrivers_IE();
        
        return View(viewMod);
    }

    public IActionResult Index()
    {
        var laps = dbContext.Laps.ToList();
        var cars = dbContext.Cars.ToList();
        var drivers = dbContext.Drivers.ToList();
        var viewMod = new LapGenViewModel();
        viewMod.laps = laps;
        viewMod.cars = cars;
        viewMod.drivers =drivers;
        return View(viewMod);
    }
    public IActionResult Reporting()
    {
        var laps = dbContext.Laps.ToList();
        var cars = dbContext.Cars.ToList();
        var drivers = dbContext.Drivers.ToList();
        var viewMod = new ReportingViewModel();
        viewMod.laps = laps;
        viewMod.cars = cars;
        viewMod.drivers =drivers;
        return View(viewMod);
    }

    public IActionResult Car()
    {
        return View();
    }

    public string CalculateLapTime(string currentTime, int inc)
    {
        //time is hours:Minutes:Seconds
        string[] data = currentTime.Split(":");
        int hours = Convert.ToInt32(data[0]);
        int minutes = Convert.ToInt32(data[1]);
        int seconds = Convert.ToInt32(data[2]);
        int nanoSeconds = Convert.ToInt32(data[3]);
        string returnString = "";

        seconds +=inc;
        if(seconds > 60)
        {
            seconds = 0;
            minutes++;
        }
        if(minutes > 60)
        {
            minutes = 0;
            hours++;
        }
        
        if(hours < 10)
        returnString += returnString + "0" + hours +":";
        else
        returnString += returnString + hours +":";

        if(minutes < 10)
        returnString += returnString + "0" + minutes +":";
        else
        returnString += returnString + minutes +":";

        if(seconds < 10)
        returnString += returnString + "0" + seconds +":";
        else
        returnString += returnString + seconds +":";


        return returnString;
        
    }

    public async Task<IActionResult> GenLaps(string ddlCarVal, string ddlDriverVal){
        LapGenToggle = true;
        string previusLapTime = "00:00:00";
        int lapCountTime = 1;
        while (LapGenToggle)
        {
            int lapnumber = 1;
            Lap newLap = new Lap();
            using (dbContext)
            {
                int selectedDriver = Convert.ToInt32(ddlCarVal);
                int selectedCar = Convert.ToInt32(ddlDriverVal);
                newLap.carID = selectedCar;
                newLap.recordedDriverID = selectedDriver;
                newLap.lapNumber = lapnumber;
                previusLapTime = CalculateLapTime(previusLapTime,lapCountTime);
                newLap.recordedLapTime = previusLapTime;
                dbContext.Laps.Add(newLap);
                await dbContext.SaveChangesAsync();

                lapnumber++;

            }
        }
        return RedirectToAction("Index");
    }

    public void StopLaps(){
        LapGenToggle = false;
    }

    [HttpPost]
    #region Create
        public async Task<IActionResult> CreateDriver(GeneralDataViewModel model)
        {
            using (dbContext)
            {
                dbContext.Drivers.Add(model.newDriver);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Crud");
            }
        }
        public async Task<IActionResult> CreateCar(GeneralDataViewModel model)
        {
            using (dbContext)
            {
                model.newCar.SetAsStockCar();
                int selectedValue = Convert.ToInt32(Request.Form["ddl_driver"]);
                List<Driver> dbRef_Drivers = dbContext.Drivers.ToList();
                foreach (Driver item in dbRef_Drivers)
                {
                    if(item.ID == selectedValue)
                    {
                        model.newCar.currentDriver = item.ID;
                        model.newCar.possibleDrivers.Add(item);
                    }
                } 
                dbContext.Cars.Add(model.newCar);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Crud");
            }
        }
        
        
        public async Task<IActionResult> CreateLap(GeneralDataViewModel model)
        {
            using (dbContext)
            {
                int selectedDriver = Convert.ToInt32(Request.Form["ddl_drivers"]);
                int selectedCar = Convert.ToInt32(Request.Form["ddl_cars"]);
                int selectedRace = Convert.ToInt32(Request.Form["ddl_races"]);
                List<Driver> dbRef_Drivers = dbContext.Drivers.ToList();
                List<Car> dbRef_Cars = dbContext.Cars.ToList();
                model.newLap.carID = selectedCar;
                model.newLap.recordedDriverID = selectedDriver;


                // foreach (Driver item in dbRef_Drivers)
                // {
                //     if(item.ID == selectedDriver)
                //     {
                //         item.name = model.newDriver.name;
                //         item.rank = model.newDriver.rank;
                        
                        
                //     }
                // }
                
                dbContext.Laps.Add(model.newLap);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Crud");
            }
        }
    #endregion
    #region Read
    
    #endregion
    #region Update
        public async Task<IActionResult> UpdateDriver(GeneralDataViewModel model)
        {
            using (dbContext)
            {
                int selectedValue = Convert.ToInt32(Request.Form["ddl_drivers"]);
                List<Driver> dbRef_Drivers = dbContext.Drivers.ToList();

                foreach (Driver item in dbRef_Drivers)
                {
                    if(item.ID == selectedValue)
                    {
                        item.name = model.newDriver.name;
                        item.rank = model.newDriver.rank;
                        dbContext.Drivers.Update(item);
                        await dbContext.SaveChangesAsync();
                        
                    }
                }
                
                
                return RedirectToAction("Crud");
            }
        }
        public async Task<IActionResult> UpdateCar(GeneralDataViewModel model)
        {
            using (dbContext)
            {
                int selectedValueCar = Convert.ToInt32(Request.Form["ddl_cars"]);
                int selectedValueDriver = Convert.ToInt32(Request.Form["ddl_drivers"]);
                List<Car> dbRef_Cars = dbContext.Cars.ToList();

                foreach (Car item in dbRef_Cars)
                {
                    if(item.ID == selectedValueCar)
                    {
                        item.name = model.newCar.name;
                        item.number = model.newCar.number;
                        item.currentDriver = selectedValueDriver;
                        dbContext.Cars.Update(item);
                        await dbContext.SaveChangesAsync();
                        
                    }
                }
                return RedirectToAction("Crud");
            }
        }
        public async Task<IActionResult> UpdateLap(GeneralDataViewModel model)
        {
            using (dbContext)
            {
                int selectedValueLap = Convert.ToInt32(Request.Form["ddl_laps"]);
                int selectedValueCar = Convert.ToInt32(Request.Form["ddl_cars"]);
                int selectedValueDriver = Convert.ToInt32(Request.Form["ddl_drivers"]);
                List<Lap> dbRef_Laps = dbContext.Laps.ToList();

                foreach (Lap item in dbRef_Laps)
                {
                    if(item.ID == selectedValueLap)
                    {
                        
                        item.recordedDriverID = selectedValueDriver;
                        item.carID = selectedValueCar;
                        item.lapNumber = model.newLap.lapNumber;
                        item.recordedLapTime = model.newLap.recordedLapTime;
                        dbContext.Laps.Update(item);
                        await dbContext.SaveChangesAsync();
                        
                    }
                }
                return RedirectToAction("Crud");
            }
        }
        
        

    #endregion
    #region Delete
    public async Task<IActionResult> DeleteData(GeneralDataViewModel model)
        {
            string command = "";
            int deleteID = -1;
            using (dbContext)
            {
                foreach(string item in Request.Form.Keys)
                {
                    if(item.Substring(0,6).Equals("Delete"))
                    {
                        command = item.Substring(item.IndexOf("_") + 1).ToUpper();
                        break;
                    }
                }

                switch(command){
                    case "DRIVER":
                        deleteID = Convert.ToInt32(Request.Form["ddl_driver"]);
                        foreach(Driver item in dbContext.Drivers.ToList())
                        {
                            if(item.ID == deleteID)
                            {
                                item.active = false;
                                dbContext.Drivers.Update(item);
                                break;
                            }
                        }
                        
                    break;
                    case "CAR":
                        deleteID = Convert.ToInt32(Request.Form["ddl_car"]);
                        foreach(Car item in dbContext.Cars.ToList())
                        {
                            if(item.ID == deleteID)
                            {
                                item.active = false;
                                dbContext.Cars.Update(item);
                                break;
                            }
                        }
                    break;

                    case "LAP":
                        deleteID = Convert.ToInt32(Request.Form["ddl_lap"]);
                        foreach(Lap item in dbContext.Laps.ToList())
                        {
                            if(item.ID == deleteID)
                            {
                                dbContext.Laps.Remove(item);
                                break;
                            }
                        }
                    break;
                    
                }
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Crud");
        }
    
    #endregion
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}