using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;
using System.Data;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class MstRoleController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstRoleRepo mstRoleRepo;
        public MstRoleController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstRoleRepo = new MstRoleRepo(db);
        }
        public IActionResult Index(string SearchString)
        {
            ViewBag.Search = SearchString;
            List<VMMstRole> dataview = mstRoleRepo.getAllData();
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
        public JsonResult Create(VMMstRole dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstRoleRepo.Create(dataView);
            }
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            VMMstRole dataView = mstRoleRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstRole dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstRoleRepo.Edit(dataView);
            }
            return Json(respon);
        }


        public IActionResult Delete(int id)
        {
            VMMstRole dataView = mstRoleRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMMstRole dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            VMResponse respon = mstRoleRepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index");
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMMstRole dataView = mstRoleRepo.GetById(id);
            return View(dataView);
        }
    }
}
