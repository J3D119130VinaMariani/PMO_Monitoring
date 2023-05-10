using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class TrxProjectController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly TrxProjectRepo trxProjectRepo;
        private readonly MstDivisiRepo mstDivisirepo;
        public TrxProjectController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.trxProjectRepo = new TrxProjectRepo(db);
            this.mstDivisirepo = new MstDivisiRepo(db);

        }
        public IActionResult Index(string SearchString)
        {
            ViewBag.Search = SearchString;
            int IdDivisi = (int)HttpContext.Session.GetInt32("IdDivisi");
            int IdRole = (int)HttpContext.Session.GetInt32("IdRole");
            List<VMTrxProject> dataview = trxProjectRepo.getAllData(IdDivisi, IdRole);
            if (!string.IsNullOrEmpty(SearchString))
            {
                dataview = dataview.Where(s => s.Projectname.ToLower().Contains(SearchString.ToLower())).ToList();
            }
            return View(dataview);
        }
        public IActionResult Create()
        {
            ViewBag.DropDownDivisi = mstDivisirepo.getAllData();
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMTrxProject dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;
            if (ModelState.IsValid)
            {
                respon = trxProjectRepo.Create(dataView);
            }
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.DropDownDivisi = mstDivisirepo.getAllData();
            VMTrxProject dataView = trxProjectRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMTrxProject dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Lastupdatedby = IdUser;
            if (ModelState.IsValid)
            {
                respon = trxProjectRepo.Edit(dataView);
            }
            return Json(respon);
        }
        public IActionResult Delete(int id)
        {
            VMTrxProject dataView = trxProjectRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMTrxProject dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Lastupdatedby = IdUser;
            VMResponse respon = trxProjectRepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index");
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMTrxProject dataView = trxProjectRepo.GetById(id);
            return View(dataView);
        }
    }
}
