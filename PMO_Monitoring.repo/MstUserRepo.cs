using AutoMapper;
using MiniProject302.viewmodels;
using PMO_Monitoring.datamodels;
using PMO_viewmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PMO_repo
{
    public class MstUserRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();
        int IdUser = 1;
        public MstUserRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstUser, VMMstUser>();
                cfg.CreateMap<VMMstUser, MstUser>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMMstUser> getAllData()
        {
            List<MstUser> dataModel = db.MstUsers.Where(a => a.Deleted == false).ToList();
            List<VMMstUser> dataView = GetMapper().Map<List<VMMstUser>>(dataModel);
            return dataView;
        }

        public VMMstUser GetById(int id)
        {
            MstUser dataModel = db.MstUsers.Find(id);
            VMMstUser dataView = GetMapper().Map<VMMstUser>(dataModel);
            return dataView;
        }
        public VMResponse Create(VMMstUser dataView)
        {
            MstUser dataModel = GetMapper().Map<MstUser>(dataView);
            dataModel.Nama= dataView.Nama;
            dataModel.Divisi = dataView.Divisi;
            dataModel.Username = dataView.Username;
            dataModel.Password = dataView.Password;
            dataModel.Createdby = IdUser;
            dataModel.Createddate = DateTime.Now;
            //dataModel.Updatedby = IdUser;
            //dataModel.Updateddate = DateTime.Now;
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
        public VMResponse Edit(VMMstUser dataView)
        {
            MstUser dataModel = db.MstUsers.Find(dataView.Id);
            dataModel.Nama = dataView.Nama;
            dataModel.Divisi = dataView.Divisi;
            dataModel.Username = dataView.Username;
            dataModel.Password = dataView.Password;
            dataModel.Updatedby = IdUser;
            dataModel.Updateddate = DateTime.Now;
            dataModel.Deleted = false;
            //dataModel.IsDelete = false;
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
                response.Entity = GetMapper().Map<VMMstUser>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMMstUser dataView)
        {
            MstUser dataModel = db.MstUsers.Find(dataView.Id);

            dataModel.Deleted = true;
            dataModel.Updatedby = IdUser;
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
                response.Entity = GetMapper().Map<VMMstUser>(dataModel);
            }
            return response;
        }

    }
}
