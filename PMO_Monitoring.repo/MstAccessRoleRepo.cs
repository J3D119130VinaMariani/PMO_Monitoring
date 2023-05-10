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
    public class MstAccessRoleRepo
    {
        private readonly PMO_MonitoringContext db;
        private readonly MstMenuRepo mstMenuRepo;
        VMResponse response = new VMResponse();
        
        public MstAccessRoleRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
            this.mstMenuRepo = new MstMenuRepo(db);
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstRoleaccess, VMMstRoleAccess>();
                cfg.CreateMap<VMMstRoleAccess, MstRoleaccess>();
                cfg.CreateMap<MstMenu, VMMstMenu>();
                cfg.CreateMap<VMMstMenu, MstMenu>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMMstRoleAccess> getAllData()
        {
            List<VMMstRoleAccess> dataView = (from ra in db.MstRoleaccesses
                                              join r in db.MstRoles on ra.Roleid equals r.Id
                                              join m in db.MstMenus on ra.Menuid equals m.Id
                                              where ra.Deleted == false
                                              select new VMMstRoleAccess
                                              {
                                                  Id = ra.Id,
                                                  Menuid = ra.Menuid,
                                                  NamaMenu = m.Nama,
                                                  Roleid = ra.Roleid,
                                                  NamaRole = r.Name
                                              }).ToList();
            return dataView;
        }

        public VMMstRoleAccess GetById(int id)
        {         
           VMMstRoleAccess dataView = (from ra in db.MstRoleaccesses
                                              join r in db.MstRoles on ra.Roleid equals r.Id
                                              join m in db.MstMenus on ra.Menuid equals m.Id
                                              where ra.Deleted == false && ra.Id == id
                                              select new VMMstRoleAccess
                                              {
                                                  Id = ra.Id,
                                                  Menuid = ra.Menuid,
                                                  NamaMenu = m.Nama,
                                                  Roleid = ra.Roleid,
                                                  NamaRole = r.Name,
                                                  Createdby = ra.Createdby,
                                                  Createddate = ra.Createddate,
                                              }).FirstOrDefault();
            return dataView;
        }
        public VMResponse Create(VMMstRoleAccess dataView)
        {
            MstRoleaccess dataModel = GetMapper().Map<MstRoleaccess>(dataView);
            dataModel.Menuid = dataView.Menuid;
            dataModel.Roleid = dataView.Roleid;
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
        public VMResponse Edit(VMMstRoleAccess dataView)
        {
            MstRoleaccess dataModel = db.MstRoleaccesses.Find(dataView.Id);
            dataModel.Menuid = dataView.Menuid;
            dataModel.Roleid = dataView.Roleid;
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
                response.Entity = GetMapper().Map<VMMstRoleAccess>(dataModel);
            }
            return response;
        }
        public VMResponse Delete(VMMstRoleAccess dataView)
        {
            MstRoleaccess dataModel = db.MstRoleaccesses.Find(dataView.Id);

            dataModel.Deleted = true;
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
                response.Entity = GetMapper().Map<VMMstRoleAccess>(dataModel);
            }
            return response;
        }
        public List<VMMstMenu> getNamaMenu(int id)
        {
            MstRoleaccess data = db.MstRoleaccesses.Find(id);
            List<MstRoleaccess> dat = db.MstRoleaccesses.Where(x => x.Roleid == data.Roleid && x.Id != id).ToList();
            List<VMMstMenu> listMenu = mstMenuRepo.getAllData();
            List<VMMstMenu> newListMenu = new List<VMMstMenu>();
            if(dat.Count != 0)
            {
                foreach (var item in dat)
                {
                    foreach (var dt in listMenu)
                    {
                        if (dt.Id == item.Menuid)
                        {
                            listMenu.Remove(dt);
                            break;
                        }
                    }
                }
                return listMenu;
            }
            else
            {
                return listMenu;
            }
        }
    }
}
