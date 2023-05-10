using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_repo;
using PMO_viewmodel;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class MstParameterController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstParameterRepo mstParameterRepo;
        private static VMPage page = new VMPage();
        public MstParameterController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstParameterRepo = new MstParameterRepo(db);
        }
        public IActionResult Index(VMPage pg)
        {
            ViewBag.Search = pg.searchString;
            var dataview = from a in mstParameterRepo.getAllData()
                           select a;

            if (!string.IsNullOrEmpty(pg.searchString))
            {
                dataview = dataview.Where(s => s.Code.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }
            int pageSize = 5;
            page = pg;
            return View(PaginatedList<VMMstParameter>.CreateAsync(dataview, pg.pageNumber ?? 1, pageSize));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMMstParameter dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstParameterRepo.Create(dataView);
            }
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            VMMstParameter dataView = mstParameterRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstParameter dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstParameterRepo.Edit(dataView);
            }
            return Json(respon);
        }


        public IActionResult Delete(int id)
        {
            VMMstParameter dataView = mstParameterRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMMstParameter dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            VMResponse respon = mstParameterRepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index", page);
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMMstParameter dataView = mstParameterRepo.GetById(id);
            return View(dataView);
        }
    }
}
