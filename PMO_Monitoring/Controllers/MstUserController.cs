using Microsoft.AspNetCore.Mvc;
using MiniProject302.viewmodels;
using PMO_Monitoring.datamodels;
using PMO_repo;
using PMO_viewmodel;
using System.Collections.Generic;

namespace PMO_Monitoring.Controllers
{
    public class MstUserController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstUserRepo mstUserRepo;
        public MstUserController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstUserRepo = new MstUserRepo(db);
        }
        public IActionResult Index()
        {
            List<VMMstUser> dataView = mstUserRepo.getAllData();
            return View(dataView);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMMstUser dataView)
        {
            VMResponse respon = new VMResponse();
            if (ModelState.IsValid)
            {
                respon = mstUserRepo.Create(dataView);
            }
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            VMMstUser dataView = mstUserRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstUser dataView)
        {
            VMResponse respon = new VMResponse();
            if (ModelState.IsValid)
            {
                respon = mstUserRepo.Edit(dataView);
            }
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
            VMResponse respon = mstUserRepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index");
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
