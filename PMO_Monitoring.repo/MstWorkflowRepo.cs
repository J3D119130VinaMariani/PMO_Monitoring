using AutoMapper;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PMO_Monitoring.repo
{
    public class MstWorkflowRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();
        public MstWorkflowRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstWorkflow, VMMstWorkflow>();
                cfg.CreateMap<VMMstWorkflow, MstWorkflow>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMMstWorkflow> getAllData()
        {
            List<VMMstWorkflow> dataView = (from w in db.MstWorkflows
                                        join m in db.MstMenus on w.Menuid equals m.Id
                                        join j in db.MstJabatans on w.Jabatanid equals j.Id 
                                        where w.Deleted == false
                                        select new VMMstWorkflow
                                        {
                                            Id = w.Id,
                                            Menuid = w.Menuid,
                                            NamaMenu = m.Nama,
                                            Sequence = w.Sequence,
                                            Jabatanid = w.Id,
                                            NamaJabatan = j.Nama,
                                        }).ToList();
            return dataView;
        }
        public VMMstWorkflow GetById(int id)
        {
            VMMstWorkflow dataView = (from w in db.MstWorkflows
                                            join m in db.MstMenus on w.Menuid equals m.Id
                                            join j in db.MstJabatans on w.Jabatanid equals j.Id
                                            where w.Deleted == false && w.Id == id
                                            select new VMMstWorkflow
                                            {
                                                Id = w.Id,
                                                Menuid = w.Menuid,
                                                NamaMenu = m.Nama,
                                                Sequence = w.Sequence,
                                                Jabatanid = w.Id,
                                                NamaJabatan = j.Nama,
                                                Updatedby = w.Updatedby,
                                                Updateddate = w.Updateddate,
                                                Createdby = w.Createdby,
                                                Createddate = w.Createddate
                                            }).FirstOrDefault();
            return dataView;
        }
        public VMResponse Create(VMMstWorkflow dataView)
        {
            MstWorkflow dataModel = GetMapper().Map<MstWorkflow>(dataView);
            dataModel.Menuid = dataView.Menuid;
            dataModel.Sequence = dataView.Sequence;
            dataModel.Jabatanid = dataView.Jabatanid;
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
        public VMResponse Edit(VMMstWorkflow dataView)
        {
            MstWorkflow dataModel = db.MstWorkflows.Find(dataView.Id);
            dataModel.Menuid = dataView.Menuid;
            dataModel.Sequence = dataView.Sequence;
            dataModel.Jabatanid = dataView.Jabatanid;
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
                response.Entity = GetMapper().Map<VMMstWorkflow>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMMstWorkflow dataView)
        {
            MstWorkflow dataModel = db.MstWorkflows.Find(dataView.Id);

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
                response.Entity = GetMapper().Map<VMMstWorkflow>(dataModel);
            }
            return response;
        }
    }
}
