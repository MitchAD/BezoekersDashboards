using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BezoekersDashboard.Models
{
    public class Roos24
    {
        public int Id { get; set; }
        public Sponsor Sponsors { get; set; }
        public int Transactions { get; set; }
        public int Recieved_Points { get; set; }
        public int Exchanche_Points { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }
    }
}
