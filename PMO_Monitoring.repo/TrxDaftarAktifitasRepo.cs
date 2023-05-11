using AutoMapper;
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
    public class TrxDaftarAktifitasRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();

        public TrxDaftarAktifitasRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TrxDaftaraktifita, VMTrxDaftarAktifitas>();
                cfg.CreateMap<VMTrxDaftarAktifitas, TrxDaftaraktifita>();
                cfg.CreateMap<TrxProject, VMTrxProject>();
                cfg.CreateMap<VMTrxProject, TrxProject>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMTrxDaftarAktifitas> getAllData(int? IdDivisi, int idProject)
        {
            List<VMTrxDaftarAktifitas> dataView = (from da in db.TrxDaftaraktifitas
                                                   join d in db.MstDivisis on da.Divisiid equals d.Id
                                                   join s in db.MstStatuses on da.Status equals s.Id
                                                   join p in db.TrxProjects on da.Projectid equals p.Id
                                                   where da.Deleted == false && da.Divisiid == IdDivisi && da.Projectid == idProject
                                                   orderby da.Urutan ascending
                                                   select new VMTrxDaftarAktifitas
                                                   {
                                                       Id = da.Id,
                                                       Divisiid = da.Divisiid,
                                                       NamaDivisi = d.Name,
                                                       Projectid = da.Projectid,
                                                       Projectname = p.Projectname,
                                                       Urutan = da.Urutan,
                                                       Aktifitas = da.Aktifitas,
                                                       Keterangan = da.Keterangan,
                                                       Status = da.Status,
                                                       NamaStatus = s.Keterangan,
                                                       CodeStatus = s.Code,
                                                       Startdate = da.Startdate,
                                                       Enddate = da.Enddate,
                                                       ActualStartdate = da.ActualStartdate,
                                                       ActualEnddate = da.ActualEnddate,
                                                       Progress = da.Progress,
                                                       ActualProgress = da.ActualProgress,
                                                       Approvalby = da.Approvalby,
                                                       Approvaldate = da.Approvaldate,
                                                       Approvalstatus = da.Approvalstatus,
                                                       Komentar = da.Komentar
                                                   }).ToList();
            return dataView;
        }
        public VMTrxDaftarAktifitas GetById(int id)
        {
            VMTrxDaftarAktifitas dataView = (from da in db.TrxDaftaraktifitas
                                             join d in db.MstDivisis on da.Divisiid equals d.Id
                                             join s in db.MstStatuses on da.Status equals s.Id
                                             join p in db.TrxProjects on da.Projectid equals p.Id
                                             where da.Deleted == false && da.Id == id
                                             select new VMTrxDaftarAktifitas
                                             {
                                                 Id = da.Id,
                                                 Divisiid = da.Divisiid,
                                                 NamaDivisi = d.Name,
                                                 Projectid = da.Projectid,
                                                 Projectname = p.Projectname,
                                                 Urutan = da.Urutan,
                                                 Aktifitas = da.Aktifitas,
                                                 Keterangan = da.Keterangan,
                                                 Status = da.Status,
                                                 NamaStatus = s.Keterangan,
                                                 CodeStatus = s.Code,
                                                 Startdate = da.Startdate,
                                                 Enddate = da.Enddate,
                                                 ActualStartdate = da.ActualStartdate,
                                                 ActualEnddate = da.ActualEnddate,
                                                 Progress = da.Progress,
                                                 ActualProgress = da.ActualProgress,
                                                 Approvalby = da.Approvalby,
                                                 Approvaldate = da.Approvaldate,
                                                 Createdby = da.Createdby,
                                                 Createddate = da.Createddate,
                                                 Updatedby = da.Updatedby,
                                                 Updateddate = da.Updateddate,
                                                 Approvalstatus = da.Approvalstatus,
                                                 Komentar = da.Komentar
                                             }).FirstOrDefault();
            return dataView;
        }
        public VMResponse Create(VMTrxDaftarAktifitas dataView)
        {
            TrxDaftaraktifita dataModel = GetMapper().Map<TrxDaftaraktifita>(dataView);
            dataModel.Divisiid = (int)dataView.Divisiid;
            dataModel.Projectid = dataView.Projectid;
            dataModel.Urutan = dataView.Urutan;
            dataModel.Aktifitas = dataView.Aktifitas;
            dataModel.Keterangan = dataView.Keterangan;
            dataModel.Status = dataView.Status;
            dataModel.Startdate = dataView.Startdate;
            dataModel.Enddate = dataView.Enddate;
            dataModel.ActualStartdate = dataView.ActualStartdate;
            dataModel.ActualEnddate = dataView.ActualEnddate;
            dataModel.Progress = dataView.Progress;
            dataModel.ActualProgress = dataView.ActualProgress;

            dataModel.Createdby = dataView.Createdby;
            dataModel.Createddate = DateTime.Now;
            dataModel.Deleted = false;
            try
            {
                db.Add(dataModel);
                db.SaveChanges();
                response.Message = "data success saved";
                response.Entity = dataModel;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "failed saved : " + e.Message;
            }
            return response;
        }
        public VMResponse Edit(VMTrxDaftarAktifitas dataView)
        {
            TrxDaftaraktifita dataModel = db.TrxDaftaraktifitas.Find(dataView.Id);
            dataModel.Divisiid = (int)dataView.Divisiid;
            dataModel.Projectid = dataView.Projectid;
            dataModel.Urutan = dataView.Urutan;
            dataModel.Aktifitas = dataView.Aktifitas;
            dataModel.Status = dataView.Status;
            dataModel.Startdate = dataView.Startdate;
            dataModel.Enddate = dataView.Enddate;
            dataModel.Keterangan = dataView.Keterangan;
            dataModel.ActualStartdate = dataView.ActualStartdate;
            dataModel.ActualEnddate = dataView.ActualEnddate;
            dataModel.Progress = dataView.Progress;
            dataModel.Approvalstatus = null;
            if(dataView.ActualProgress != null)
            {               
                dataModel.ActualProgress = (decimal)dataView.ActualProgress;
            }
            dataModel.Updatedby = dataView.Updatedby;
            dataModel.Updateddate = DateTime.Now;
            try
            {
                db.Update(dataModel);
                db.SaveChanges();
                response.Message = "data success saved";
                response.Entity = dataModel;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "failed saved : " + e.Message;
                response.Entity = GetMapper().Map<TrxDaftaraktifita>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMTrxDaftarAktifitas dataView)
        {
            TrxDaftaraktifita dataModel = db.TrxDaftaraktifitas.Find(dataView.Id);
            dataModel.Deleted = true;
            dataModel.Updatedby = dataView.Updatedby;
            dataModel.Updateddate = DateTime.Now;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();
                response.Message = "data success saved";
                response.Entity = dataModel;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "failed saved : " + e.Message;
                response.Entity = GetMapper().Map<VMTrxDaftarAktifitas>(dataModel);
            }
            return response;
        }
        public VMResponse Approv(int idProject, int idUser)
        {
            List<TrxDaftaraktifita> dataModel = db.TrxDaftaraktifitas.Where(x => x.Projectid == idProject).ToList();
            foreach (var item in dataModel)
            {
                var datum = db.TrxDaftaraktifitas.Where(x => x.Id == item.Id).FirstOrDefault();
                datum.Approvalstatus = "A";
                datum.Approvalby = idUser;
                datum.Approvaldate = DateTime.Now;

                datum.Updatedby = idUser;
                datum.Updateddate = DateTime.Now;
                db.Update(datum);
                db.SaveChanges();
            }            
            try
            {              
                response.Message = "data success saved";                
            }
            catch (Exception e)
            {
                response.Success = false;
            }
            return response;
        }
        public VMResponse Reject(int idProject, int idUser)
        {
            List<TrxDaftaraktifita> dataModel = db.TrxDaftaraktifitas.Where(x => x.Projectid == idProject).ToList();
            foreach (var item in dataModel)
            {
                var datum = db.TrxDaftaraktifitas.Where(x => x.Id == item.Id).FirstOrDefault();
                datum.Approvalstatus = "R";
                datum.Approvalby = idUser;
                datum.Approvaldate = DateTime.Now;

                datum.Updatedby = idUser;
                datum.Updateddate = DateTime.Now;
                db.Update(datum);
                db.SaveChanges();
            }
            try
            {
                response.Message = "data success saved";
            }
            catch (Exception e)
            {
                response.Success = false;
            }
            return response;
        }
        public MstDivisi getDivisi(int idDivisi)
        {
            var dt = db.MstDivisis.Where(x => x.Id == idDivisi).FirstOrDefault();
            return dt;
        }
        public VMResponse Komentar(VMTrxDaftarAktifitas dataView)
        {
            TrxDaftaraktifita dataModel = db.TrxDaftaraktifitas.Find(dataView.Id);
            dataModel.Komentar = dataView.Komentar;
            dataModel.Updatedby = dataView.Updatedby;
            dataModel.Updateddate = DateTime.Now;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();
                response.Message = "data success saved";
                response.Entity = dataModel;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "failed saved : " + e.Message;
                response.Entity = GetMapper().Map<VMTrxDaftarAktifitas>(dataModel);
            }
            return response;
        }
        public List<VMTrxProject> getProject(int IdDivisi)
        {

            List<TrxProject> dataModel = db.TrxProjects.Where(a => a.Divisiid == IdDivisi && a.Deleted == false).ToList();
            List<VMTrxProject> dataView = GetMapper().Map<List<VMTrxProject>>(dataModel);

            return dataView;
        }
        public bool CheckProgress(decimal progress, int id)
        {
            TrxDaftaraktifita datum = db.TrxDaftaraktifitas.Find(id);
            if (progress > datum.Progress)
            {
                return true;
            }
            return false;
        }
        public List<VMTrxDaftarAktifitas> getAllDataProject(int? IdDivisi)
        {
            List<VMTrxDaftarAktifitas> dataView = (from da in db.TrxDaftaraktifitas
                                                   join p in db.TrxProjects on da.Projectid equals p.Id
                                                   where da.Deleted == false && da.Divisiid == IdDivisi
                                                   group da by da.Projectid into d
                                                   select new VMTrxDaftarAktifitas
                                                   {                                                       
                                                       Projectid = d.Select(x => x.Projectid).FirstOrDefault()                                                   
                                                   }).ToList();
            return dataView;
        }
        public List<VMTrxDaftarAktifitas> getApprov(int idProject)
        {
            List<VMTrxDaftarAktifitas> datamodel = (from da in db.TrxDaftaraktifitas
                                              where da.Deleted == false && da.Projectid == idProject
                                              group da by new
                                              {
                                                  da.Projectid,
                                                  da.Approvalstatus
                                              } into d
                                              select new VMTrxDaftarAktifitas
                                              {
                                                  Approvalstatus = d.Select(x => x.Approvalstatus).FirstOrDefault()
                                              }).ToList();
            return datamodel;
        }
        public string GetLegendColor(string CodeStatus)
        {
            var color = "";
            if(CodeStatus == "BM")
            {
                color = "btn btn-secondary";
            }
            else if(CodeStatus == "P")
            {
                color = "btn btn-success";
            }
            else if (CodeStatus == "PD")
            {
                color = "btn btn-danger";
            }
            else if (CodeStatus == "D")
            {
                color = "btn btn-primary";
            }
            return color;
        }
    }
}
