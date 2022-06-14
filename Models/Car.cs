using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lapGen.Models
{
    public class Car
    {
        
        public int ID { get; set; }
        public bool active { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string length { get; set; }
        public string height { get; set; }
        public string width { get; set; }
        public string weight { get; set; }
        public string wheelbase { get; set; }

        //this could probaly be just a max
        public int hp_maxMin { get; set; } //based of track, best turning hp
        public int hp_maxMax { get; set; } //based of track, best straight hp
        public string transmission { get; set; }
        //fkey
        //public int driverID { get; set; } //remove driver ID for collection
        public ICollection<Driver> possibleDrivers {get; set;} // navigation prop for db
        public int currentDriver { get; set; }

        public Car(){
            //this.ID = -1;
            this.active = true;
            this.name = "";
            this.number = 0;
            this.length = "";
            this.height = "";
            this.width = "";
            this.weight = "";
            this.wheelbase = "";
            this.hp_maxMax = 0;
            this.hp_maxMin = 0;
            this.transmission = "";
            this.possibleDrivers = new List<Driver>();
            this.currentDriver = 0;
        }

        //dotnet aspnet-codegenerator razorpage -m Lap -dc lapGen.persistance.DataCon -udl -outDir Pages\Lap --referenceScriptLibraries -sqlite

        public void SetAsStockCar(){
            this.length = "193.3";
            this.height = "50.2";
            this.width = "78.4";
            this.weight = "3,200 lb";
            this.wheelbase = "110";
            this.hp_maxMax = 670;
            this.hp_maxMin = 550;
            this.transmission = "5-speed sequential shift (plus reverse)";
        }
    }
}