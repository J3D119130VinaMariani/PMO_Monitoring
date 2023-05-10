using AutoMapper;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PMO_Monitoring.repo
{
    public class TrxProjectRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();

        public TrxProjectRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TrxProject, VMTrxProject>();
                cfg.CreateMap<VMTrxProject, TrxProject>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMTrxProject> getAllData(int IdDivisi, int IdRole)
        {
            if(IdRole != 1)
            {
                List<VMTrxProject> dataView = (from p in db.TrxProjects
                                               join d in db.MstDivisis on p.Divisiid equals d.Id
                                               where p.Deleted == false && p.Divisiid == IdDivisi
                                               select new VMTrxProject
                                               {
                                                   Id = p.Id,
                                                   Projectname = p.Projectname,                                          
                                                   Divisiid = p.Divisiid,
                                                   Keterangan = p.Keterangan,
                                                   NamaDivisi = d.Name,
                                                   Createdby = p.Createdby,
                                                   Createddate = p.Createddate,
                                                   Lastupdatedby = p.Lastupdatedby,
                                                   Lastupdateddate = p.Lastupdateddate
                                               }).ToList();
                return dataView;
            }
            else
            {
                List<VMTrxProject> dataView = (from p in db.TrxProjects
                                               join d in db.MstDivisis on p.Divisiid equals d.Id
                                               where p.Deleted == false 
                                               select new VMTrxProject
                                               {
                                                   Id = p.Id,
                                                   Projectname = p.Projectname,
                                                   Divisiid = p.Divisiid,
                                                   Keterangan = p.Keterangan,
                                                   NamaDivisi = d.Name,
                                                   Createdby = p.Createdby,
                                                   Createddate = p.Createddate,
                                                   Lastupdatedby = p.Lastupdatedby,
                                                   Lastupdateddate = p.Lastupdateddate
                                               }).ToList();
                return dataView;
            }
            
        }
        public VMTrxProject GetById(int id)
        {
            VMTrxProject dataView = (from p in db.TrxProjects
                                           join d in db.MstDivisis on p.Divisiid equals d.Id
                                           where p.Deleted == false && p.Id==id
                                           select new VMTrxProject
                                           {
                                               Id = p.Id,
                                               Projectname = p.Projectname,
                                               Divisiid = p.Divisiid,
                                               Keterangan = p.Keterangan,
                                               NamaDivisi = d.Name,
                                               Createdby = p.Createdby,
                                               Createddate = p.Createddate,
                                               Lastupdatedby = p.Lastupdatedby,
                                               Lastupdateddate = p.Lastupdateddate
                                           }).FirstOrDefault();
            return dataView;
        }
        public VMResponse Create(VMTrxProject dataView)
        {
            TrxProject dataModel = GetMapper().Map<TrxProject>(dataView);
            dataModel.Projectname = dataView.Projectname;
            dataModel.Divisiid = dataView.Divisiid;
            dataModel.Keterangan = dataView.Keterangan;
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
        public VMResponse Edit(VMTrxProject dataView)
        {
            TrxProject dataModel = db.TrxProjects.Find(dataView.Id);
            dataModel.Projectname = dataView.Projectname;
            dataModel.Divisiid = dataView.Divisiid;
            dataModel.Keterangan = dataView.Keterangan;
            dataModel.Lastupdatedby = dataView.Lastupdatedby;
            dataModel.Lastupdateddate = DateTime.Now;
            dataModel.Deleted = false;
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
                response.Entity = GetMapper().Map<VMTrxProject>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMTrxProject dataView)
        {
            TrxProject dataModel = db.TrxProjects.Find(dataView.Id);

            dataModel.Deleted = true;
            dataModel.Lastupdatedby = dataView.Lastupdatedby;
            dataModel.Lastupdateddate = DateTime.Now;

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
                response.Entity = GetMapper().Map<VMTrxProject>(dataModel);
            }
            return response;
        }
    }
}
