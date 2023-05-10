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
    public class MstRoleRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();
        
        public MstRoleRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstRole, VMMstRole>();
                cfg.CreateMap<VMMstRole, MstRole>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMMstRole> getAllData()
        {
            List<MstRole> dataModel = db.MstRoles.Where(a => a.Deleted == false).ToList();
            List<VMMstRole> dataView = GetMapper().Map<List<VMMstRole>>(dataModel);
            return dataView;
        }
        public VMMstRole GetById(int id)
        {
            MstRole dataModel = db.MstRoles.Find(id);
            VMMstRole dataView = GetMapper().Map<VMMstRole>(dataModel);
            return dataView;
        }
        public VMResponse Create(VMMstRole dataView)
        {
            MstRole dataModel = GetMapper().Map<MstRole>(dataView);
            dataModel.Name = dataView.Name;
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
        public VMResponse Edit(VMMstRole dataView)
        {
            MstRole dataModel = db.MstRoles.Find(dataView.Id);
            dataModel.Name = dataView.Name;
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
                response.Entity = GetMapper().Map<VMMstRole>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMMstRole dataView)
        {
            MstRole dataModel = db.MstRoles.Find(dataView.Id);

            dataModel.Deleted = true;
            dataModel.Updatedby = dataView.Updatedby;
            dataModel.Updateddate = DateTime.Now;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();
                response.Message = "data success saved";
                response.Entity = dataModel;


                var data = db.MstUsers.Where(x => x.Role == dataView.Id).ToList();
                if (data.Count != 0)
                {
                    foreach (var item in data)
                    {
                        var datum = db.MstUsers.Where(x => x.Id == item.Id).FirstOrDefault();
                        datum.Role = 0;
                        db.Update(datum);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "failed saved : " + e.Message;
                response.Entity = GetMapper().Map<VMMstRole>(dataModel);
            }
            return response;
        }
    }
}
