using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_repo;
using PMO_viewmodel;
using System.Collections.Generic;
using System.Data;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class MstUserController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstUserRepo mstUserRepo;
        private readonly MstDivisiRepo mstDivisirepo;
        private readonly MstRoleRepo mstRoleRepo;
        private readonly MstJabatanRepo mstJabatanRepo;
        private static VMPage page = new VMPage();
        public MstUserController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstUserRepo = new MstUserRepo(db);
            this.mstDivisirepo = new MstDivisiRepo(db);
            this.mstRoleRepo = new MstRoleRepo(db);
            this.mstJabatanRepo = new MstJabatanRepo(db);
        }
        public IActionResult Index(VMPage pg)
        {
            ViewBag.Search = pg.searchString;
            var dataview = from a in mstUserRepo.getAllData()
                           select a;

            if (!string.IsNullOrEmpty(pg.searchString))
            {
                dataview = dataview.Where(s => s.Nama.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }
            int pageSize = 5;
            page = pg;
            return View(PaginatedList<VMMstUser>.CreateAsync(dataview, pg.pageNumber ?? 1, pageSize));
        }
        public IActionResult Create()
        {
            ViewBag.DropDownDivisi = mstDivisirepo.getAllData();
            ViewBag.DropDownRole = mstRoleRepo.getAllData();
            ViewBag.DropDownJabatan = mstJabatanRepo.getAllData();
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMMstUser dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;

            respon = mstUserRepo.Create(dataView);

            ViewBag.DropDownDivisi = mstDivisirepo.getAllData();
            ViewBag.DropDownRole = mstRoleRepo.getAllData();
            ViewBag.DropDownJabatan = mstJabatanRepo.getAllData();
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.DropDownDivisi = mstDivisirepo.getAllData();
            ViewBag.DropDownRole = mstRoleRepo.getAllData();
            ViewBag.DropDownJabatan = mstJabatanRepo.getAllData();
            VMMstUser dataView = mstUserRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstUser dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;

            respon = mstUserRepo.Edit(dataView);

            ViewBag.DropDownDivisi = mstDivisirepo.getAllData();
            ViewBag.DropDownRole = mstRoleRepo.getAllData();
            ViewBag.DropDownJabatan = mstJabatanRepo.getAllData();
            return Json(respon);
        }

        public IActionResult Delete(int id)
        {
            VMMstUser dataView = mstUserRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMMstUser dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;

            VMResponse respon = mstUserRepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index", page);
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMMstUser dataView = mstUserRepo.GetById(id);
            return View(dataView);
        }

    }

}
