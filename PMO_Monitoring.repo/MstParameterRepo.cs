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
    public class MstParameterRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();
        
        public MstParameterRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstParameter, VMMstParameter>();
                cfg.CreateMap<VMMstParameter, MstParameter>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMMstParameter> getAllData()
        {
            List<MstParameter> dataModel = db.MstParameters.Where(a => a.Deleted == false).ToList();
            List<VMMstParameter> dataView = GetMapper().Map<List<VMMstParameter>>(dataModel);
            return dataView;
        }
        public VMMstParameter GetById(int id)
        {
            MstParameter dataModel = db.MstParameters.Find(id);
            VMMstParameter dataView = GetMapper().Map<VMMstParameter>(dataModel);
            return dataView;
        }
        public VMResponse Create(VMMstParameter dataView)
        {
            MstParameter dataModel = GetMapper().Map<MstParameter>(dataView);
            dataModel.Code = dataView.Code;
            dataModel.Value = dataView.Value;
            dataModel.Description = dataView.Description;
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
        public VMResponse Edit(VMMstParameter dataView)
        {
            MstParameter dataModel = db.MstParameters.Find(dataView.Id);
            dataModel.Code = dataView.Code;
            dataModel.Value = dataView.Value;
            dataModel.Description = dataView.Description;
            dataModel.Updatedby = dataView.Updatedby;
            dataModel.Updateddate = DateTime.Now;
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
                response.Entity = GetMapper().Map<VMMstParameter>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMMstParameter dataView)
        {
            MstParameter dataModel = db.MstParameters.Find(dataView.Id);

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
                response.Entity = GetMapper().Map<VMMstParameter>(dataModel);
            }
            return response;
        }
    }
}
