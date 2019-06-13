using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BezoekersDashboard.Models;

namespace BezoekersDashboard.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly BezoekersDashboardContext _context;

        public VisitorsController(BezoekersDashboardContext context)
        {
            _context = context;
        }
        public IActionResult Bezoekers()
        {
            return View();
        }

        // GET: Visitors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Visitors.OrderByDescending(s => s.Date.Year).ToListAsync());
        }

        // GET: Visitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitors == null)
            {
                return NotFound();
            }

            return View(visitors);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Zipcode,Amount,Date")] Visitors visitors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visitors);
        }

        [Route("Visitors/auto/generate-records")]
        public async Task<IActionResult> FillTables()
        {
            List<int> year_list = new List<int>();
            year_list.Add(2018);
            year_list.Add(2019);
            List<int> month_list = new List<int>();

            /* 
            Secret tip van 3e jaar
            int i = 1;
            while(i < 13)
            {
                month_list.Add(i);
                i++;
            }
            List<string> time_list = new List<string>();
            int x = 1;
            while (x < 25)
            {
                time_list.Add(x.ToString() + ":00");
                x++;
            }
            */
            month_list.Add(1);
            month_list.Add(2);
            month_list.Add(3);
            month_list.Add(4);
            month_list.Add(5);
            month_list.Add(6);
            month_list.Add(7);
            month_list.Add(8);
            month_list.Add(9);
            month_list.Add(10);
            month_list.Add(11);
            month_list.Add(12);
            List<string> time_list = new List<string>();
            time_list.Add("1:00");
            time_list.Add("2:00");
            time_list.Add("3:00");
            time_list.Add("4:00");
            time_list.Add("5:00");
            time_list.Add("6:00");
            time_list.Add("7:00");
            time_list.Add("8:00");
            time_list.Add("9:00");
            time_list.Add("10:00");
            time_list.Add("11:00");
            time_list.Add("12:00");
            time_list.Add("13:00");
            time_list.Add("14:00");
            time_list.Add("15:00");
            time_list.Add("16:00");
            time_list.Add("17:00");
            time_list.Add("18:00");
            time_list.Add("19:00");
            time_list.Add("20:00");
            time_list.Add("21:00");
            time_list.Add("22:00");
            time_list.Add("23:00");
            time_list.Add("00:00");
            if (ModelState.IsValid)
            {
                Visitors newvisit = new Visitors();
                Random rand = new Random();
                foreach (int year in year_list)
                {
                    foreach (int month in month_list)
                    {
                        int daysinmonth = DateTime.DaysInMonth(year, month);
                        for (int day = 1; day < daysinmonth; day++)
                        {
                            foreach(string time in time_list)
                            {
                                newvisit.Amount = rand.Next(10, 50);
                                string datetimestring = year.ToString() + "-" + month.ToString() + "-" + day.ToString() + " " + time;
                                newvisit.Date = DateTime.Parse(datetimestring);
                                newvisit.Zipcode = "4724CW";
                                newvisit.Id = 0;
                                _context.Add(newvisit);
                                await _context.SaveChangesAsync();
                                newvisit = new Visitors();
                            }
                        }
                    }
                }
            }
            return View("Visitors");
        }
        // GET: Visitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors.FindAsync(id);
            if (visitors == null)
            {
                return NotFound();
            }
            return View(visitors);
        }

        [Route("Visitors/input_zip/{zipcode}")]
        public async Task<IActionResult> GetZip(string zipcode)
        {
            ViewData["input_zipcode"] = zipcode;
            if (zipcode == "" || zipcode == null)
            {
                ViewData["date"] = "test";
                return NotFound();
            }
            ViewData["date"] = zipcode;
            var visit = await _context.Visitors.ToListAsync();
            List<Visitors> realvisit = new List<Visitors>();
            foreach (var item in visit)
            {
                if (item.Zipcode == zipcode)
                {
                    realvisit.Add(await _context.Visitors.FindAsync(item.Id));
                }
            }
            if (realvisit == null)
            {
                return NotFound();
            }
            return View("VisitZipFilter", realvisit.OrderBy(s => s.Date.Year));
        }

        // GET : Visitors/Date/24-12-2018
        [Route("Visitors/input_date/{date}")]
        public async Task<IActionResult> GetDate(string date)
        {

            ViewData["input_date"] = date;
            if (date == "" || date == null)
            {
                ViewData["date"] = "test";
                return NotFound();
            }
            ViewData["date"] = date;
            var visit = await _context.Visitors.ToListAsync();
            List<Visitors> realvisit = new List<Visitors>();
            foreach (var item in visit)
            {
                var itemdate = item.Date.ToString("yyyy-MM-dd");
                if (itemdate == date)
                {
                    realvisit.Add(await _context.Visitors.FindAsync(item.Id));
                }
            }
            if (realvisit == null)
            {
                return NotFound();
            }
            return View("Visitors", realvisit.OrderBy(s => s.Date.Year));
        }

        [Route("Visitors/input_month/{month}")]
        public async Task<IActionResult> GetMonth(string month)
        {
            ViewData["input_month"] = month;
            if (month == "" || month == null)
            {
                ViewData["month"] = "test";
                return NotFound();
            }
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            ViewData["month"] = month;
            var visit = await _context.Visitors.ToListAsync();
            List<Visitors> realvisit = new List<Visitors>();
            foreach (var item in visit)
            {
                var itemmonth = item.Date.ToString("MM");
                if (itemmonth == month)
                {
                    realvisit.Add(await _context.Visitors.FindAsync(item.Id));
                }
            }
            if (realvisit == null)
            {
                return NotFound();
            }
            return View("Visitors", realvisit.OrderBy(s => s.Date.Year));
        }

        [Route("Visitors/input_year/{year}")]
        public async Task<IActionResult> GetYear(string year)
        {
            ViewData["input_year"] = year;
            if (year == "" || year == null)
            {
                ViewData["year"] = "test";
                return NotFound();
            }
            ViewData["month"] = year;
            var visit = await _context.Visitors.ToListAsync();
            List<Visitors> realvisit = new List<Visitors>();
            foreach (var item in visit)
            {
                var itemyear = item.Date.ToString("yyyy");
                if (itemyear == year)
                {
                    realvisit.Add(await _context.Visitors.FindAsync(item.Id));
                }
            }
            if (realvisit == null)
            {
                return NotFound();
            }
            return View("Visitors", realvisit.OrderBy(s => s.Date.Year));
        }

        [Route("Visitors/input_date/{date}/input_zip/{zipcode}")]
        public async Task<IActionResult> GetDateZip(string date, string zipcode)
        {

            ViewData["input_zipcode"] = zipcode;
            ViewData["input_date"] = date;
            if (date == "" || date == null || zipcode == "" || zipcode == null)
            {
                ViewData["date"] = "test";
                return NotFound();
            }
            ViewData["date"] = date;
            var visit = await _context.Visitors.ToListAsync();
            List<Visitors> realvisit = new List<Visitors>();
            foreach (var item in visit)
            {
                var itemdate = item.Date.ToString("yyyy-MM-dd");
                if (itemdate == date)
                {
                    if (item.Zipcode == zipcode)
                    {
                        realvisit.Add(await _context.Visitors.FindAsync(item.Id));
                    }
                }
            }
            if (realvisit == null)
            {
                return NotFound();
            }
            return View("VisitZipFilter", realvisit.OrderBy(s => s.Date.Year));
        }

        [Route("Visitors/input_month/{month}/input_zip/{zipcode}")]
        public async Task<IActionResult> GetMonthZip(string month, string zipcode)
        {

            ViewData["input_zipcode"] = zipcode;
            ViewData["input_month"] = month;
            if (month == "" || month == null || zipcode == "" || zipcode == null)
            {
                ViewData["month"] = "test";
                return NotFound();
            }
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            ViewData["month"] = month;
            var visit = await _context.Visitors.ToListAsync();
            List<Visitors> realvisit = new List<Visitors>();
            foreach (var item in visit)
            {
                var itemmonth = item.Date.ToString("MM");
                if (itemmonth == month)
                {
                    if (item.Zipcode == zipcode)
                    {
                        realvisit.Add(await _context.Visitors.FindAsync(item.Id));
                    }
                }
            }
            if (realvisit == null)
            {
                return NotFound();
            }
            return View("VisitZipFilter", realvisit.OrderBy(s => s.Date.Year));
        }

        [Route("Visitors/input_year/{year}/input_zip/{zipcode}")]
        public async Task<IActionResult> GetYearZip(string year, string zipcode)
        {

            ViewData["input_zipcode"] = zipcode;
            ViewData["input_year"] = year;
            if (year == "" || year == null || zipcode == "" || zipcode == null)
            {
                ViewData["year"] = "test";
                return NotFound();
            }
            ViewData["month"] = year;
            var visit = await _context.Visitors.ToListAsync();
            List<Visitors> realvisit = new List<Visitors>();
            foreach (var item in visit)
            {
                var itemyear = item.Date.ToString("yyyy");
                if (itemyear == year)
                {
                    if (item.Zipcode == zipcode)
                    {
                        realvisit.Add(await _context.Visitors.FindAsync(item.Id));
                    }
                }
            }
            if (realvisit == null)
            {
                return NotFound();
            }
            return View("VisitZipFilter", realvisit.OrderBy(s => s.Date.Year));
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Zipcode,Amount,Date")] Visitors visitors)
        {
            if (id != visitors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorsExists(visitors.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(visitors);
        }

        // GET: Visitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitors == null)
            {
                return NotFound();
            }

            return View(visitors);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitors = await _context.Visitors.FindAsync(id);
            _context.Visitors.Remove(visitors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorsExists(int id)
        {
            return _context.Visitors.Any(e => e.Id == id);
        }
    }
}
