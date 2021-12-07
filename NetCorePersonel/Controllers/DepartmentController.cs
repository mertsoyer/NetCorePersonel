using Microsoft.AspNetCore.Mvc;
using NetCorePersonel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePersonel.Controllers
{
    public class DepartmentController : Controller
    {
        Context context = new Context();
        public IActionResult Index()
        {
            var departmanlar = context.Departments.ToList();
            return View(departmanlar);
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {


            return View();
        }

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult DeleteDepartment(int id)
        {
            var departmanSil = context.Departments.Find(id);
            context.Departments.Remove(departmanSil);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateDepartment(int id)
        {
            var departmanGuncelle = context.Departments.Find(id);

            return View(departmanGuncelle);
        }

        [HttpPost]
        public IActionResult UpdateDepartment(Department department)
        {
            var departmanGuncelle =context.Departments.Find(department.Id);
            departmanGuncelle.Name=department.Name;
            context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult DepartmentDetail(int id)
        {
            var degerler=context.Employees.Where(x => x.DepartmentId == id).ToList(); // bağlı olan employeeleri ekrana basmak için
            var birimAd = context.Departments.Where(x => x.Id == id).Select(y => y.Name).FirstOrDefault(); // birim adını ekrana bastırmak için
            ViewBag.departmentName = birimAd;

            return View(degerler);
        }

    }
}
