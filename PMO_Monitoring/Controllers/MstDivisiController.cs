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
    public class MstDivisiController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstDivisiRepo mstDivisirepo;
        public MstDivisiController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstDivisirepo = new MstDivisiRepo(db);
        }
        public IActionResult Index(string SearchString)
        {
            ViewBag.Search = SearchString;
            List<VMMstDivisi> dataview = mstDivisirepo.getAllData();
            if (!string.IsNullOrEmpty(SearchString))
            {
                dataview = dataview.Where(s => s.Name.ToLower().Contains(SearchString.ToLower())).ToList();
            }
            return View(dataview);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMMstDivisi dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstDivisirepo.Create(dataView);
            }
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            VMMstDivisi dataView = mstDivisirepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstDivisi dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstDivisirepo.Edit(dataView);
            }
            return Json(respon);
        }


        public IActionResult Delete(int id)
        {
            VMMstDivisi dataView = mstDivisirepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMMstDivisi dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            VMResponse respon = mstDivisirepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index");
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMMstDivisi dataView = mstDivisirepo.GetById(id);
            return View(dataView);
        }
    }
}
