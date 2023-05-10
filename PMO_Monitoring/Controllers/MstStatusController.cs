using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class MstStatusController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstStatusRepo mstStatusRepo;
        private static VMPage page = new VMPage();
        public MstStatusController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstStatusRepo = new MstStatusRepo(db);
        }
        public IActionResult Index(VMPage pg)
        {
            ViewBag.Search = pg.searchString;
            var dataview = from s in mstStatusRepo.getAllData()
                           select s;

            if (!string.IsNullOrEmpty(pg.searchString))
            {
                dataview = dataview.Where(s => s.Code.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }
            int pageSize = 5;
            page = pg;
            return View(PaginatedList<VMMstStatus>.CreateAsync(dataview, pg.pageNumber ?? 1, pageSize));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMMstStatus dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstStatusRepo.Create(dataView);
            }
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            VMMstStatus dataView = mstStatusRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstStatus dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstStatusRepo.Edit(dataView);
            }
            return Json(respon);
        }


        public IActionResult Delete(int id)
        {
            VMMstStatus dataView = mstStatusRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMMstStatus dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            VMResponse respon = mstStatusRepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index", page);
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMMstStatus dataView = mstStatusRepo.GetById(id);
            return View(dataView);
        }
    }
}
