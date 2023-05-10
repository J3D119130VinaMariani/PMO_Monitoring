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
    public class TrxDaftarAktifitasController : Controller
    {

        private readonly PMO_MonitoringContext db;
        private readonly TrxDaftarAktifitasRepo daftarAktifitasRepo;
        private readonly MstDivisiRepo mstDivisiRepo;
        private readonly MstStatusRepo mstStatusRepo;
        public TrxDaftarAktifitasController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.daftarAktifitasRepo = new TrxDaftarAktifitasRepo(db);
            this.mstDivisiRepo = new MstDivisiRepo(db);
            this.mstStatusRepo = new MstStatusRepo(db);
        }
        public IActionResult Index(string SearchString)
        {
            ViewBag.Search = SearchString;
            //int IdDivisi = (int)HttpContext.Session.GetInt32("IdDivisi");
            //List<VMTrxDaftarAktifitas> dataview = daftarAktifitasRepo.getAllData(IdDivisi);
            //if (!string.IsNullOrEmpty(SearchString))
            //{
            //    dataview = dataview.Where(s => s.Projectname.ToLower().Contains(SearchString.ToLower())).ToList();
            //}
            //return View(dataview);
            return View();
        }
        public IActionResult Create(int idDivisi)
        {
            //ViewBag.DropDownDivisi = mstDivisiRepo.getAllData();
            int IdDivisi = (int)HttpContext.Session.GetInt32("IdDivisi");
            ViewBag.DropDownStatus = mstStatusRepo.getAllData();
            ViewBag.DropDownProject = daftarAktifitasRepo.getProject(IdDivisi);
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMTrxDaftarAktifitas dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;

                respon = daftarAktifitasRepo.Create(dataView);

          
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            int IdDivisi = (int)HttpContext.Session.GetInt32("IdDivisi");
            ViewBag.DropDownDivisi = mstDivisiRepo.getAllData();
            ViewBag.DropDownStatus = mstStatusRepo.getAllData();
            ViewBag.DropDownProject = daftarAktifitasRepo.getProject(IdDivisi);
            VMTrxDaftarAktifitas dataView = daftarAktifitasRepo.GetById(id);
            return View(dataView);
        }


        [HttpPost]
        public JsonResult Edit(VMTrxDaftarAktifitas dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                respon = daftarAktifitasRepo.Edit(dataView);
            }
            return Json(respon);
        }


        public IActionResult Delete(int id)
        {
            VMTrxDaftarAktifitas dataView = daftarAktifitasRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMTrxDaftarAktifitas dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            VMResponse respon = daftarAktifitasRepo.Delete(dataView);
           
            if (respon.Success)
            {
                return RedirectToAction("Index");
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            int IdDivisi = (int)HttpContext.Session.GetInt32("IdDivisi");
            ViewBag.DropDownProject = daftarAktifitasRepo.getProject(IdDivisi);
            VMTrxDaftarAktifitas dataView = daftarAktifitasRepo.GetById(id);
            return View(dataView);
        }
        public JsonResult Approv(int idProject)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            if (ModelState.IsValid)
            {
                respon = daftarAktifitasRepo.Approv(idProject, IdUser);
            }
            return Json(respon);
        }
        public JsonResult Reject(int idProject)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            if (ModelState.IsValid)
            {
                respon = daftarAktifitasRepo.Reject(idProject, IdUser);
            }
            return Json(respon);
        }
        public IActionResult Komentar(int id)
        {
            VMTrxDaftarAktifitas dataView = daftarAktifitasRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Komentar(VMTrxDaftarAktifitas dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            VMResponse respon = daftarAktifitasRepo.Komentar(dataView);

            if (respon.Success)
            {
                return RedirectToAction("Index");
            }
            return View(respon.Entity);
        }
        public JsonResult CheckProgress(string progress, int id)
        {
            //decimal x = decimal.Parse(progress.Replace(".", ","));
            decimal x = decimal.Parse(progress.Replace(",", "."));
            bool data = daftarAktifitasRepo.CheckProgress(x, id);
            return Json(data);
        }
    }
}
