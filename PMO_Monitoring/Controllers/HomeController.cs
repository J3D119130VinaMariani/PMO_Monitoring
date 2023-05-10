using Microsoft.AspNetCore.Mvc;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.Models;
using PMO_Monitoring.repo;
using PMO_Monitoring.Services;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;
using System.Data;
using System.Diagnostics;

namespace PMO_Monitoring.Controllers
{
    [SessionFilter]
    public class HomeController : Controller
    {

        private readonly TrxProjectRepo trxProjectRepo;
        private readonly PMO_MonitoringContext db;
        private readonly MstHomeRepo mstHomeRepo;
        public HomeController(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.trxProjectRepo = new TrxProjectRepo(db);
            this.mstHomeRepo = new MstHomeRepo(db);
        }

        public IActionResult Index()
        {
            int IdDivisi = (int)HttpContext.Session.GetInt32("IdDivisi");
            List<VMTrxProject> dataview = mstHomeRepo.getAllDataAktifitasProject(IdDivisi);
            return View(dataview);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}