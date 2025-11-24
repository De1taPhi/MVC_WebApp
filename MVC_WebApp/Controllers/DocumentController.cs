using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_WebApp.DB;
using MVC_WebApp.Models;
using System;

namespace MVC_WebApp.Controllers
{
    public class DocumentController : Controller
    {
        // GET: DocumentController
        private MainDbContext _context;

        public DocumentController(MainDbContext context)
        {
            _context = context;
        }



        public ActionResult Index()
        {
            return View(_context.Documents.ToList());
        }

        // GET: DocumentController/Details/5
        public ActionResult Details(int id)
        {
            var doc = _context.Documents.Where(x => x.Id == id).First();
            doc.Details = _context.DocumentDetails.Where(x => x.DocumentId == id).ToList();
            return View(doc);
        }

        // GET: DocumentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentController/CreateDetail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            var doc = new Document
            {
                Number = collection["Number"],
                Value = Convert.ToDouble(collection["Value"]),
                Description = collection["Description"],
                Date = DateTime.Parse(collection["Date"])
            };

            try
            {
                _context.Documents.Add(doc);
                _context.SaveChanges(true);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _context.Documents.Remove(doc);
                var err = new ErrorLog
                {
                    ErrorTime = DateTime.Now,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    StackTrace = ex.StackTrace
                };
                _context.ErrorLogs.Add(err);
                _context.SaveChanges();

                return RedirectToRoute("Default", new { controller = "ErrorLog", action = "Details"});
            }
        }


        public ActionResult CreateDetail(int id)
        {
            return View(new DocumentDetail {DocumentId = id, Value = 0, Name = "Name" });
        }

        // POST: DocumentController/CreateDetail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDetail(IFormCollection collection)
        {
            try
            {
                var detail = new DocumentDetail
                {
                    Name = collection["Name"],
                    DocumentId = int.Parse(collection["DocumentId"]),
                    Value = double.Parse(collection["Value"])
                };
                _context.DocumentDetails.Add(detail);

                var doc = _context.Documents.Where(x => x.Id == detail.DocumentId).First();
                doc.Value += detail.Value;
                _context.SaveChanges(true);

                return RedirectToAction(nameof(Details), new { id = detail.DocumentId });
            }
            catch
            {
                return View();
            }
        }

        // GET: DocumentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_context.Documents.Where(x => x.Id == id).First());
        }

        // POST: DocumentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var doc = _context.Documents.Where(x => x.Id == id).First();
            
            try
            {
                doc.Number = collection["Number"];
                doc.Value = Convert.ToDouble(collection["Value"]);
                doc.Description = collection["Description"];
                doc.Date = DateTime.Parse(collection["Date"]);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _context.Entry(doc).State = EntityState.Detached;
                doc = _context.Documents.Find(id);
                _context.ErrorLogs.Add(new ErrorLog
                {
                    ErrorTime = DateTime.Now,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    StackTrace = ex.StackTrace
                });
                _context.SaveChanges();
                return RedirectToRoute("Default", new { controller = "ErrorLog", action = "Details" });

            }
        }

        public ActionResult EditDetail(int id)
        {
            return View(_context.DocumentDetails.Where(x => x.Id == id).First());
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetail(int id, IFormCollection collection)
        {
            try
            {
                var detail = _context.DocumentDetails.Where(x => x.Id == id).First();
                detail.Name = collection["Name"];
                detail.DocumentId = int.Parse(collection["DocumentId"]);
                detail.Value = double.Parse(collection["Value"]);
                _context.SaveChanges();



                var doc = _context.Documents.Where(x => x.Id == detail.DocumentId).First();
                doc.Value = _context.DocumentDetails.Where(x => x.DocumentId == detail.DocumentId).Select(y => y.Value).Sum();
                _context.SaveChanges();
                 
                return RedirectToAction(nameof(Details), new {id = detail.DocumentId});
            }
            catch
            {
                return View();
            }
        }


        // GET: DocumentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_context.Documents.Where(x => x.Id == id).First());
        }

        // POST: DocumentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _context.Documents.Remove(_context.Documents.Where(x => x.Id == id).First());
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteDetail(int id)
        {
            return View(_context.DocumentDetails.Where(x => x.Id == id).First());
        }

        // POST: DocumentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDetail(int id, IFormCollection collection)
        {
            try
            {
                var detail = _context.DocumentDetails.Where(x => x.Id == id).First();
                var documentId = detail.DocumentId;
                _context.DocumentDetails.Remove(detail);
                _context.SaveChanges();

                
                var doc = _context.Documents.Where(x => x.Id == documentId).First();
                doc.Value = _context.DocumentDetails.Where(x => x.DocumentId == documentId).Select(y => y.Value).Sum();
                _context.SaveChanges();

                return RedirectToAction(nameof(Details), new {id = documentId});
            }
            catch
            {
                return View();
            }
        }
    }
}
