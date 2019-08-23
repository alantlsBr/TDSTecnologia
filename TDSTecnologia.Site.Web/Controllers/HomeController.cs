using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDSTecnologia.Site.Infrastructure.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TDSTecnologia.Site.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.CursoDao.ToListAsync());
        }

        private readonly AppContexto _context;

        public HomeController(AppContexto context)
        {
            _context = context;
        }
    }
}
