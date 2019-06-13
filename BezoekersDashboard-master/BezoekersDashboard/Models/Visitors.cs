using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BezoekersDashboard.Models
{
    public class Visitors
    {
        public int Id { get; set; }
        public string Zipcode { get; set; }
        public int Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

    }
}
