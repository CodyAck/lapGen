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
using lapGen.Services;
using lapGen.Utils;

namespace lapGen.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataCon dbContext;
    private readonly IRecordsService recService;
    private readonly IDriverService driverSerivce;
    private readonly ICarService carService;
    private readonly ILapService lapService;
    public bool LapGenToggle = false;
    private LapUtilitys UtilitysRef = new LapUtilitys();

    public HomeController(ILogger<HomeController> logger, DataCon dbContext,
    IRecordsService recService,IDriverService driverSerivce,ICarService carService, ILapService lapService )
    {
        _logger = logger;
        this.dbContext = dbContext;
        this.recService = recService;
        this.driverSerivce = driverSerivce;
        this.carService = carService;
        this.lapService = lapService;
    }

    public IActionResult Crud()
    {
        var cars = dbContext.Cars.ToList();
        var drivers = dbContext.Drivers.ToList();
        var laps = dbContext.Laps
        .Include(l =>l.records).ThenInclude(r =>r.car)
        .Include(l=>l.records).ThenInclude(r=>r.driver)
        .ToList();
        var records = dbContext.Records.ToList();
        var viewMod = new GeneralDataViewModel();
        viewMod.cars = cars;
        viewMod.drivers = drivers;
        viewMod.laps = laps;
        viewMod.records = records;
        return View(viewMod);
    }

    public IActionResult Index()
    {
        var laps = dbContext.Laps.ToList();
        var cars = dbContext.Cars.ToList();
        var drivers = dbContext.Drivers.ToList();
        var records = dbContext.Records.ToList();
        var viewMod = new LapGenViewModel();
        viewMod.laps = laps;
        viewMod.cars = cars;
        viewMod.drivers =drivers;
        viewMod.records = records;
        return View(viewMod);
    }
    public IActionResult Reporting()
    {
        var laps = dbContext.Laps.ToList();
        var cars = dbContext.Cars.Where(c => c.active).ToList();
        var drivers = dbContext.Drivers.Where(d => d.active).ToList();
        var records = dbContext.Records.ToList();
        var viewMod = new ReportingViewModel();
        viewMod.laps = laps;
        viewMod.cars = cars;
        viewMod.drivers =drivers;
        viewMod.records = records;
        return View(viewMod);
    }
    public void GenLaps(){
        LapGenToggle = true;
        string previusLapTime = "00:00:00";
        int lapCountTime = 1;
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
                driverSerivce.CreateDriver(model.newDriver.name,0);
                return RedirectToAction("Crud");
            }
        }
        public async Task<IActionResult> CreateCar(GeneralDataViewModel model)
        {
            using (dbContext)
            {
                carService.CreateCar(model.newCar.number,model.newCar.name);
                return RedirectToAction("Crud");
            }
        }     
        public async Task<IActionResult> CreateLap(GeneralDataViewModel model)
        {
            using (dbContext)
            {
                lapService.CreateLap(model.newLap.lapNumber);
                return RedirectToAction("Crud");
            }
        }
        public async Task<IActionResult> CreateRecord(GeneralDataViewModel model)
        {
            int selectedDriver = Convert.ToInt32(Request.Form["ddl_drivers"]);
            int selectedCar = Convert.ToInt32(Request.Form["ddl_cars"]);
            recService.CreateRecord(UtilitysRef.ConvertTimeInputToSeconds(model.timeIN),selectedDriver,selectedCar,model.newLap.lapNumber);
            return RedirectToAction("Crud");
        }
#endregion
#region Update
        public async Task<IActionResult> UpdateDriver(GeneralDataViewModel model)
        {
            int selectedValue = Convert.ToInt32(Request.Form["ddl_drivers"]);
            driverSerivce.UpdateDriver(selectedValue, model.newDriver.name, model.newDriver.rank);
            return RedirectToAction("Crud");
        }
        public async Task<IActionResult> UpdateCar(GeneralDataViewModel model)
        {
            int selectedValueCar = Convert.ToInt32(Request.Form["ddl_cars"]);
            carService.UpdateCar(selectedValueCar,model.newCar.number,model.newCar.name);
            return RedirectToAction("Crud");
        }
        public async Task<IActionResult> UpdateLap(GeneralDataViewModel model)
        {
            int selectedValueLap = Convert.ToInt32(Request.Form["ddl_laps"]);
            lapService.UpdateLap(selectedValueLap,model.newLap.lapNumber);
            return RedirectToAction("Crud");
        }
        public async Task<IActionResult> UpdateRecord(GeneralDataViewModel model)
        {
            int selectedValueRecord = Convert.ToInt32(Request.Form["ddl_records"]);
            int selectedValueCar = Convert.ToInt32(Request.Form["ddl_cars"]);
            int selectedValueDriver = Convert.ToInt32(Request.Form["ddl_drivers"]);
            recService.UpdateRecord(selectedValueRecord,UtilitysRef.ConvertTimeInputToSeconds(model.timeIN)
            ,selectedValueDriver,selectedValueCar,model.newLap.lapNumber);

            return RedirectToAction("Crud");
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
                        driverSerivce.DeleteDriver(deleteID);
                    break;
                    case "CAR":
                        deleteID = Convert.ToInt32(Request.Form["ddl_car"]);
                        carService.DeleteCar(deleteID);
                    break;
                    case "LAP":
                        deleteID = Convert.ToInt32(Request.Form["ddl_lap"]);
                        lapService.DeleteLap(deleteID);
                    break;
                    case "RECORD":
                        deleteID = Convert.ToInt32(Request.Form["ddl_record"]);
                        recService.DeleteRecord(deleteID);
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