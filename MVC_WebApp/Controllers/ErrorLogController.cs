using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_WebApp.DB;

namespace MVC_WebApp.Controllers
{
    public class ErrorLogController : Controller
    {
        private MainDbContext _context;

        public ErrorLogController(MainDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ErrorLogs.ToList());
        }

        public ActionResult Details()
        {
            var lastError = _context.ErrorLogs.OrderBy(x => x.Id).Last();
            return View(lastError);
        }

    }
}
