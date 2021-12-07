using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCorePersonel.Models;
using NetCorePersonel.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePersonel.Controllers
{
    public class EmployeeController : Controller
    {
        Context context = new Context();
        public IActionResult Index()
        {
            // ilişkili tablolar arasında erişim sağlayabilmek için .Include kullanılır. eğer birden çok tablo ile ilişki kurulacaksa ;
            //.Include(x=> x......) dedikten sonra
            //.ThenInclude(x=> x.....) diyerek ilişkili model sınıfları birbirine bağlanır
            var personeller = context.Employees.Include(x => x.Departments).ToList();

            return View(personeller);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            //ViewBag.departmanListele = context.Departments.ToList();
            // Bu şekilde tüm modeli view e gönderip ilgili dataların ayrıştırma işlemini oradan yapıyoruz






            //Burada ise gönderilecek olan datayı controller tarafında ayrıştırıp view'e ayrıştırdığımız datayı gönderiyoruz.
            (from Department in context.Departments select Department).ToList();

            List<SelectListItem> departmanlar = (from x in context.Departments.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.Id.ToString()
                                                 }
              ).ToList();

            ViewBag.departmanListele = departmanlar;

            //Ahmet

            //var vm = new EmployeeViewModel
            //{
            //    Departments = context.Departments.Select(
            //        x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }
            //        ).ToList()
            //};

            //return View(vm);

            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {

            var newEmpoloyee = context.Departments.Where(x => x.Id == employee.Departments.Id).FirstOrDefault();
            employee.Departments = newEmpoloyee;
            context.Employees.Add(employee);
            context.SaveChanges();


            //AHMET
            //Employee employee = request.Payload;
            //var yeniPersonel = context.Departments.Where(x => x.Id == employee.Departments.Id).FirstOrDefault();
            //employee.Departments = yeniPersonel;
            //context.Employees.Add(employee);
            //context.SaveChanges();


            return RedirectToAction("Index");
        }

        
    }
}
