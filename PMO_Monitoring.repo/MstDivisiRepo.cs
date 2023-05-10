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
    public class MstDivisiRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();
        
        public MstDivisiRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstDivisi, VMMstDivisi>();
                cfg.CreateMap<VMMstDivisi, MstDivisi>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMMstDivisi> getAllData()
        {
            List<MstDivisi> dataModel = db.MstDivisis.Where(a => a.Deleted == false).ToList();
            List<VMMstDivisi> dataView = GetMapper().Map<List<VMMstDivisi>>(dataModel);
            return dataView;
        }
        public VMMstDivisi GetById(int id)
        {
            MstDivisi dataModel = db.MstDivisis.Find(id);
            VMMstDivisi dataView = GetMapper().Map<VMMstDivisi>(dataModel);
            return dataView;
        }
        public VMResponse Create(VMMstDivisi dataView)
        {
            MstDivisi dataModel = GetMapper().Map<MstDivisi>(dataView);
            dataModel.Acronim = dataView.Acronim;
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
        public VMResponse Edit(VMMstDivisi dataView)
        {
            MstDivisi dataModel = db.MstDivisis.Find(dataView.Id);
            dataModel.Acronim = dataView.Acronim;
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
                response.Entity = GetMapper().Map<VMMstDivisi>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMMstDivisi dataView)
        {
            MstDivisi dataModel = db.MstDivisis.Find(dataView.Id);

            dataModel.Deleted = true;
            dataModel.Updatedby = dataView.Updatedby;
            dataModel.Updateddate = DateTime.Now;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();
                response.Message = "data success saved";
                response.Entity = dataModel;

                var data = db.MstUsers.Where(x => x.Divisi == dataView.Id).ToList();
                if (data.Count != 0)
                {
                    foreach (var item in data)
                    {
                        var datum = db.MstUsers.Where(x => x.Id == item.Id).FirstOrDefault();
                        datum.Divisi = 0;
                        db.Update(datum);
                        db.SaveChanges();
                    }
                }             
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "failed saved : " + e.Message;
                response.Entity = GetMapper().Map<VMMstDivisi>(dataModel);
            }
            return response;
        }

    }
}
