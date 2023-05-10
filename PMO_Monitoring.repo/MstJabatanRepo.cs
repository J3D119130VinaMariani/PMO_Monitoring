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
    public class MstJabatanRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();
        
        public MstJabatanRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstJabatan, VMMstJabatan>();
                cfg.CreateMap<VMMstJabatan, MstJabatan>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMMstJabatan> getAllData()
        {
            List<MstJabatan> dataModel = db.MstJabatans.Where(a => a.Deleted == false).ToList();
            List<VMMstJabatan> dataView = GetMapper().Map<List<VMMstJabatan>>(dataModel);
            return dataView;
        }
        public VMMstJabatan GetById(int id)
        {
            MstJabatan dataModel = db.MstJabatans.Find(id);
            VMMstJabatan dataView = GetMapper().Map<VMMstJabatan>(dataModel);
            return dataView;
        }
        public VMResponse Create(VMMstJabatan dataView)
        {
            MstJabatan dataModel = GetMapper().Map<MstJabatan>(dataView);
            dataModel.Nama = dataView.Nama;
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
        public VMResponse Edit(VMMstJabatan dataView)
        {
            MstJabatan dataModel = db.MstJabatans.Find(dataView.Id);
            dataModel.Nama = dataView.Nama;
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
                response.Entity = GetMapper().Map<VMMstJabatan>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMMstJabatan dataView)
        {
            MstJabatan dataModel = db.MstJabatans.Find(dataView.Id);

            dataModel.Deleted = true;
            dataModel.Updatedby = dataView.Updatedby;
            dataModel.Updateddate = DateTime.Now;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();
                response.Message = "data success saved";
                response.Entity = dataModel;

                var data = db.MstUsers.Where(x => x.Jabatan == dataView.Id).ToList();
                if (data.Count != 0)
                {
                    foreach (var item in data)
                    {
                        var datum = db.MstUsers.Where(x => x.Id == item.Id).FirstOrDefault();
                        datum.Jabatan = 0;
                        db.Update(datum);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "failed saved : " + e.Message;
                response.Entity = GetMapper().Map<VMMstJabatan>(dataModel);
            }
            return response;
        }

    }
}
