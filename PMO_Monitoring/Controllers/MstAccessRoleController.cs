using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class MstAccessRoleController : Controller
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstAccessRoleRepo mstAccessRoleRepo;
        private readonly MstMenuRepo mstMenuRepo;
        private readonly MstRoleRepo mstRoleRepo;
        private static VMPage page = new VMPage();
        public MstAccessRoleController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstAccessRoleRepo = new MstAccessRoleRepo(db);
            this.mstRoleRepo = new MstRoleRepo(db);
            this.mstMenuRepo = new MstMenuRepo(db);
        }
        public IActionResult Index(VMPage pg)
        {
            ViewBag.Search = pg.searchString;
            var dataview = from a in mstAccessRoleRepo.getAllData()
                             select a;
            
            if (!string.IsNullOrEmpty(pg.searchString))
            {
                dataview = dataview.Where(a => a.NamaMenu.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }
            int pageSize = 5;
            page = pg;
            return View(PaginatedList<VMMstRoleAccess>.CreateAsync(dataview, pg.pageNumber ?? 1, pageSize));
        }
        public IActionResult Create()
        {
            ViewBag.DropDownMenu = mstMenuRepo.getAllData();
            ViewBag.DropDownRole = mstRoleRepo.getAllData();
            return View();
        }
        [HttpPost]
        public JsonResult Create(VMMstRoleAccess dataView)
        {
            VMResponse respon = new VMResponse();
            int IdUser = (int)HttpContext.Session.GetInt32("IdUser");
            dataView.Createdby = IdUser;
            if (ModelState.IsValid)
            {
                respon = mstAccessRoleRepo.Create(dataView);
            }
            ViewBag.DropDownMenu = mstMenuRepo.getAllData();
            ViewBag.DropDownRole = mstRoleRepo.getAllData();
            return Json(respon);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.DropDownMenu = mstAccessRoleRepo.getNamaMenu(id);
            ViewBag.DropDownRole = mstRoleRepo.getAllData();
            VMMstRoleAccess dataView = mstAccessRoleRepo.GetById(id);
            return View(dataView);
        }

        [HttpPost]
        public JsonResult Edit(VMMstRoleAccess dataView)
        {
            VMResponse respon = new VMResponse();
            if (ModelState.IsValid)
            {
                respon = mstAccessRoleRepo.Edit(dataView);
            }
            ViewBag.DropDownMenu = mstAccessRoleRepo.getNamaMenu(dataView.Id);
            ViewBag.DropDownRole = mstRoleRepo.getAllData();
            return Json(respon);
        }


        public IActionResult Delete(int id)
        {
            VMMstRoleAccess dataView = mstAccessRoleRepo.GetById(id);
            return View(dataView);
        }
        [HttpPost]
        public IActionResult Delete(VMMstRoleAccess dataView)
        {
            
            VMResponse respon = mstAccessRoleRepo.Delete(dataView);
            if (respon.Success)
            {
                return RedirectToAction("Index",page);
            }
            return View(respon.Entity);
        }
        public IActionResult Detail(int id)
        {
            VMMstRoleAccess dataView = mstAccessRoleRepo.GetById(id);
            return View(dataView);
        }
        public JsonResult GetNamaMenu(int Roleid)
        {
            List<VMMstRoleAccess> datum = mstAccessRoleRepo.getAllData().
                                            Where(a => a.Roleid == Roleid).ToList();
            List<VMMstMenu> data = mstMenuRepo.getAllData();
            if(datum.Count != 0)
            {
                foreach(var dat in datum)
                {
                    foreach(var dt in data)
                    {
                        if (dat.Menuid == dt.Id)
                        {
                            data.Remove(dt);
                            break;
                        }
                    }
                }
                return Json(data);
            }
            else
            {
                return Json(data);
            }            
        }
    }
}
