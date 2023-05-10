using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class MstWorkflowController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstWorkflowRepo mstWorkflowRepo;
        private readonly MstJabatanRepo mstJabatanRepo;
        private readonly MstMenuRepo mstMenuRepo;
        public MstWorkflowController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstWorkflowRepo = new MstWorkflowRepo(db);
            this.mstMenuRepo = new MstMenuRepo(db);
            this.mstJabatanRepo = new MstJabatanRepo(db);
        }
        public IActionResult Index(string SearchString)
        {
            ViewBag.Search = SearchString;
            List<VMMstWorkflow> dataview = mstWorkflowRepo.getAllData();
            if (!string.IsNullOrEmpty(SearchString))
            {
                dataview = dataview.Where(s => s.NamaMenu.ToLower().Contains(SearchString.ToLower())).ToList();
            }
            return View(dataview);
        }
        public IActionResult Create()
        {
            ViewBag.DropDownMenu = mstMenuRepo.getAllData();
            ViewBag.DropDownJabatan = mstJabatanRepo.getAllData();
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMMstWorkflow dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstWorkflowRepo.Create(dataView);
            }
            ViewBag.DropDownMenu = mstMenuRepo.getAllData();
            ViewBag.DropDownJabatan = mstJabatanRepo.getAllData();
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.DropDownMenu = mstMenuRepo.getAllData();
            ViewBag.DropDownJabatan = mstJabatanRepo.getAllData();
            VMMstWorkflow dataView = mstWorkflowRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstWorkflow dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstWorkflowRepo.Edit(dataView);
            }
             ViewBag.DropDownMenu = mstMenuRepo.getAllData();
            ViewBag.DropDownJabatan = mstJabatanRepo.getAllData();
            return Json(respon);
        }


        public IActionResult Delete(int id)
        {
            VMMstWorkflow dataView = mstWorkflowRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMMstWorkflow dataView)
        {
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Updatedby = IdUser;
            VMResponse respon = mstWorkflowRepo.Delete(dataView);
            
            if (respon.Success)
            {
                return RedirectToAction("Index");
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMMstWorkflow dataView = mstWorkflowRepo.GetById(id);
            return View(dataView);
        }
    }
}
