using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BezoekersDashboard.Models
{
    public class Parking
    {
        public int Id { get; set; }
        public string ParkingName { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

    }
}
