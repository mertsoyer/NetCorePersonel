using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCorePersonel.Models;
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
            var personeller=context.Employees.Include(x=> x.Departments).ToList();

            return View(personeller);
        }
    }
}
