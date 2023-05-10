using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class MstJabatanController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstJabatanRepo mstJabatanRepo;
        public MstJabatanController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstJabatanRepo = new MstJabatanRepo(db);
        }
        public IActionResult Index(string SearchString)
        {
            ViewBag.Search = SearchString;
            List<VMMstJabatan> dataview = mstJabatanRepo.getAllData();
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
        public JsonResult Create(VMMstJabatan dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstJabatanRepo.Create(dataView);
            }
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            VMMstJabatan dataView = mstJabatanRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstJabatan dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstJabatanRepo.Edit(dataView);
            }
            return Json(respon);
        }


        public IActionResult Delete(int id)
        {
            VMMstJabatan dataView = mstJabatanRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMMstJabatan dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            VMResponse respon = mstJabatanRepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index");
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMMstJabatan dataView = mstJabatanRepo.GetById(id);
            return View(dataView);
        }
    }
}
