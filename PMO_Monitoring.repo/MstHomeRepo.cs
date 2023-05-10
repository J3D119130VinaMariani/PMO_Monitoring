using Antlr.Runtime;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMO_Monitoring.repo
{
    public class MstHomeRepo
    {
        private readonly PMO_MonitoringContext db;

        VMResponse response = new VMResponse();

        public MstHomeRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TrxDaftaraktifita, VMTrxDaftarAktifitas>();
                cfg.CreateMap<VMTrxDaftarAktifitas, TrxDaftaraktifita>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMTrxProject> getAllDataAktifitasProject(int IdDivisi)
        {

            List<VMTrxProject> dataView = (from p in db.TrxProjects
                                           join d in db.MstDivisis on p.Divisiid equals d.Id
                                           join da in db.TrxDaftaraktifitas on p.Id equals da.Projectid
                                           where p.Deleted == false && p.Divisiid == IdDivisi && da.Deleted == false
                                           group p by p.Projectname into d
                                           select new VMTrxProject
                                           {
                                               Projectname = d.Select(x => x.Projectname).FirstOrDefault(),
                                               Id = d.Select(x => x.Id).FirstOrDefault(),
                                               Divisiid = d.Select(x => x.Divisiid).FirstOrDefault()
                                           }).ToList();
            return dataView;

        }
        public string getActualProgress(int idProject)
        {
            List<VMTrxDaftarAktifitas> dataView = (from da in db.TrxDaftaraktifitas
                                                   where da.Projectid == idProject && da.Deleted == false
                                                   select new VMTrxDaftarAktifitas
                                                   {
                                                       Progress = da.Progress,
                                                       ActualProgress = da.ActualProgress

                                                   }).ToList();
            decimal? actualPogress = 0;
            foreach (var item in dataView)
            {
                if(item.ActualProgress == null)
                {
                    item.ActualProgress = 0;
                }
                actualPogress = actualPogress + item.ActualProgress;
                actualPogress = actualPogress;
            }
            string x = actualPogress.ToString();
            string y = x.Replace(",", ".");
            return y;
        }
        public string getDateProgress(int idProject)
        {
            List<VMTrxDaftarAktifitas> dataView = (from da in db.TrxDaftaraktifitas
                                                   where da.Projectid == idProject && da.Deleted == false
                                                   select new VMTrxDaftarAktifitas
                                                   {
                                                       Progress = da.Progress,
                                                       ActualProgress = da.ActualProgress,
                                                       Startdate = da.Startdate,
                                                       Enddate = da.Enddate,
                                                       ActualStartdate = da.ActualStartdate,
                                                       ActualEnddate = da.ActualEnddate,
                                                       lamaProgress = (da.Enddate-da.Startdate).TotalDays,
                                                       lamaActualProgres = ((da.ActualEnddate??DateTime.Now) - (da.ActualStartdate??DateTime.Now)).TotalDays                                                       
                                                   }).ToList();

            double dateProgress = 0;
            double dateActualProgress = 0;
            foreach (var item in dataView)
            {
                dateProgress = dateProgress + item.lamaProgress;
                dateProgress = dateProgress;

                dateActualProgress = dateActualProgress + item.lamaActualProgres;
                dateActualProgress = dateActualProgress;
            }
            double persentaseDate = Math.Round(( dateActualProgress / dateProgress * 100),2);
            string x = persentaseDate.ToString();
            string y = x.Replace(",", ".");
            return y;
        }
        public string getAllDataAktifitas(int? IdDIvisi, int IdProject)
        {
            List<VMTrxDaftarAktifitas> dataView = (from da in db.TrxDaftaraktifitas
                                                   join p in db.TrxProjects on da.Projectid equals p.Id
                                                   join s in db.MstStatuses on da.Status equals s.Id
                                                   where da.Deleted == false && p.Divisiid == IdDIvisi && da.Projectid == IdProject
                                                   orderby da.Urutan ascending
                                                   select new VMTrxDaftarAktifitas
                                                   {
                                                       Projectid = da.Projectid,
                                                       Projectname = p.Projectname,
                                                       Status = da.Status,
                                                       NamaStatus = s.Code
                                                   }).ToList();
            decimal from = 0;
            decimal to = 0;
            List<string> NewData = new List<string>();
            string dat = "";
            string a = "";
            string b = "";
            for (int i = 0; i < dataView.Count; i++)
            {
                var datum = dataView.FirstOrDefault();

                to = Math.Round(((decimal)from + ((decimal)100 / (decimal)dataView.Count)),2);
                a = from.ToString().Replace(",", ".");
                b = to.ToString().Replace(",", ".");
                if (dataView[i].NamaStatus == "BM")
                {
                    if (i != dataView.Count - 1)
                    {
                        dat = " { " + '"' + "from" + '"' + ":" + a + "," + '"' + "to" + '"' + ":" + b + "," + '"' + "color" + '"' + ":" + '"' + "rgba(169, 169, 169, 1)" + '"' + "},";
                    }
                    else
                    {
                        dat = " { " + '"' + "from" + '"' + ":" + a + "," + '"' + "to" + '"' + ":" + b + "," + '"' + "color" + '"' + ":" + '"' + "rgba(169, 169, 169, 1)" + '"' + "}";
                    }

                }
                else if (dataView[i].NamaStatus == "P")
                {
                    if (i != dataView.Count - 1)
                    {
                        dat = " { " + '"' + "from" + '"' + ":" + a + "," + '"' + "to" + '"' + ":" + b + "," + '"' + "color" + '"' + ":" + '"' + "rgba(0,255,0,.15)" + '"' + "},";
                    }
                    else
                    {
                        dat = " { " + '"' + "from" + '"' + ":" + a + "," + '"' + "to" + '"' + ":" + b + "," + '"' + "color" + '"' + ":" + '"' + "rgba(0,255,0,.15)" + '"' + "}";
                    }

                }
                else if (dataView[i].NamaStatus == "PD")
                {
                    if (i != dataView.Count - 1)
                    {
                        dat = " { " + '"' + "from" + '"' + ":" + a + "," + '"' + "to" + '"' + ":" + b + "," + '"' + "color" + '"' + ":" + '"' + "rgba(255,30,0,.25)" + '"' + "},";
                    }
                    else
                    {
                        dat = " { " + '"' + "from" + '"' + ":" + a + "," + '"' + "to" + '"' + ":" + b + "," + '"' + "color" + '"' + ":" + '"' + "rgba(255,30,0,.25)" + '"' + "}";
                    }

                }
                else if (dataView[i].NamaStatus == "D")
                {
                    if (i != dataView.Count - 1)
                    {
                        dat = " { " + '"' + "from" + '"' + ":" + a + "," + '"' + "to" + '"' + ":" + b + "," + '"' + "color" + '"' + ":" + '"' + "rgba(0,0,255,.25)" + '"' + "},";
                    }
                    else
                    {
                        dat = " { " + '"' + "from" + '"' + ":" + a + "," + '"' + "to" + '"' + ":" + b + "," + '"' + "color" + '"' + ":" + '"' + "rgba(0,0,255,.25)" + '"' + "}";
                    }
                }
                NewData.Add(dat);
                from = to;
            }
            List<string> temp = new List<string>();
            string y = "";
            string z = "";
            foreach (var item in NewData)
            {
                y = item.Replace(@"\", "");
                z = z + y;
                y = z;
            }
            return z;
        }
    }
}
