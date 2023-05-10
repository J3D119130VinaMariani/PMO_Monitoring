using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class MstMenuController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstMenuRepo mstMenuRepo;
        public MstMenuController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstMenuRepo = new MstMenuRepo(db);
        }
        public IActionResult Index(string SearchString)
        {
            ViewBag.Search = SearchString;
            List<VMMstMenu> dataview = mstMenuRepo.getAllData();
            if (!string.IsNullOrEmpty(SearchString))
            {
                dataview = dataview.Where(s => s.Nama.ToLower().Contains(SearchString.ToLower())).ToList();
            }
            return View(dataview);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMMstMenu dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstMenuRepo.Create(dataView);
            }
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            VMMstMenu dataView = mstMenuRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstMenu dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstMenuRepo.Edit(dataView);
            }
            return Json(respon);
        }


        public IActionResult Delete(int id)
        {
            VMMstMenu dataView = mstMenuRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMMstMenu dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            VMResponse respon = mstMenuRepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index");
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMMstMenu dataView = mstMenuRepo.GetById(id);
            return View(dataView);
        }
    }
}
