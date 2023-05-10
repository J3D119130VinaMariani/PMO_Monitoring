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
    public class MstMenuRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();
        
        public MstMenuRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstMenu, VMMstMenu>();
                cfg.CreateMap<VMMstMenu, MstMenu>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMMstMenu> getAllData()
        {
            List<MstMenu> dataModel = db.MstMenus.Where(a => a.Deleted == false).ToList();
            List<VMMstMenu> dataView = GetMapper().Map<List<VMMstMenu>>(dataModel);
            return dataView;
        }
        public VMMstMenu GetById(int id)
        {
            MstMenu dataModel = db.MstMenus.Find(id);
            VMMstMenu dataView = GetMapper().Map<VMMstMenu>(dataModel);
            return dataView;
        }
        public VMResponse Create(VMMstMenu dataView)
        {
            MstMenu dataModel = GetMapper().Map<MstMenu>(dataView);
            dataModel.Modul = dataView.Modul;
            dataModel.Nama = dataView.Nama;
            dataModel.Path = dataView.Path;
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
        public VMResponse Edit(VMMstMenu dataView)
        {
            MstMenu dataModel = db.MstMenus.Find(dataView.Id);
            dataModel.Modul = dataView.Modul;
            dataModel.Nama = dataView.Nama;
            dataModel.Path = dataView.Path;
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
                response.Entity = GetMapper().Map<VMMstMenu>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMMstMenu dataView)
        {
            MstMenu dataModel = db.MstMenus.Find(dataView.Id);

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
                response.Entity = GetMapper().Map<VMMstMenu>(dataModel);
            }
            return response;
        }
    }
}
