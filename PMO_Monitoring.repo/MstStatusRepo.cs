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
    public class MstStatusRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();
        
        public MstStatusRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstStatus, VMMstStatus>();
                cfg.CreateMap<VMMstStatus, MstStatus>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMMstStatus> getAllData()
        {
            List<MstStatus> dataModel = db.MstStatuses.Where(a => a.Deleted == false).ToList();
            List<VMMstStatus> dataView = GetMapper().Map<List<VMMstStatus>>(dataModel);
            return dataView;
        }
        public VMMstStatus GetById(int id)
        {
            MstStatus dataModel = db.MstStatuses.Find(id);
            VMMstStatus dataView = GetMapper().Map<VMMstStatus>(dataModel);
            return dataView;
        }
        public VMResponse Create(VMMstStatus dataView)
        {
            MstStatus dataModel = GetMapper().Map<MstStatus>(dataView);
            dataModel.Code = dataView.Code;
            dataModel.Keterangan = dataView.Keterangan;
            dataModel.Createdby = (int)dataView.Createdby;
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
        public VMResponse Edit(VMMstStatus dataView)
        {
            MstStatus dataModel = db.MstStatuses.Find(dataView.Id);
            dataModel.Code = dataView.Code;
            dataModel.Keterangan = dataView.Keterangan;
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
                response.Entity = GetMapper().Map<VMMstStatus>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMMstStatus dataView)
        {
            MstStatus dataModel = db.MstStatuses.Find(dataView.Id);

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
                response.Entity = GetMapper().Map<VMMstStatus>(dataModel);
            }
            return response;
        }
    }
}
