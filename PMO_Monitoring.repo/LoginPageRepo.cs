using AutoMapper;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMO_Monitoring.repo
{
    public class LoginPageRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();

        public LoginPageRepo(PMO_MonitoringContext _db)
        {
            this.db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MstUser, VMMstUser>();
                cfg.CreateMap<VMMstUser, MstUser>();
                cfg.CreateMap<MstMenu, VMMstMenu>();
                cfg.CreateMap<VMMstMenu, MstMenu>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public VMResponse SignIn(VMMstUser dataView)
        {
            MstUser datum = db.MstUsers.
                                Where(a => a.Username == dataView.Username
                                && a.Deleted == false).FirstOrDefault();

            VMMstUser data = GetMapper().Map<VMMstUser>(datum);
            if (datum != null)
            {
                if (datum.Password != dataView.Password)
                {
                    response.Success = false;
                    response.Message = "Password Wrong";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Login Success";
                    response.Entity = data;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Your username is not registered ";
            }
            return response;
        }
        public bool CheckEmail(string Email)
        {
            MstUser datum = db.MstUsers.Where(a => a.Email == Email && a.Deleted == false).FirstOrDefault();
            if (datum != null)
            {
                return true;
            }
            return false;
        }
        public VMMstUser VerifikasiEmail(VMMstUser data)
        {

            MstUser datum = db.MstUsers.Where(a => a.Email == data.Email && a.Deleted == false).FirstOrDefault();
            VMMstUser dataView = GetMapper().Map<VMMstUser>(datum);
            return dataView;
        }
        public VMResponse SetPassword(VMMstUser dataView)
        {
            MstUser dataModel = db.MstUsers.Where(a => a.Id == dataView.Id && a.Deleted == false).FirstOrDefault();
            dataModel.Password = dataView.Password;
            dataModel.Updatedby = dataView.Id;
            dataModel.Updateddate = DateTime.Now;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();

                response.Success = true;
                response.Entity = dataModel;

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Entity = dataModel;
            }
            return response;
        }
        public List<VMMstMenu> GetMenu(int? IdRole, int? IdUser)
        {
           
                var result = (from u in db.MstUsers
                              join r in db.MstRoles on u.Role equals r.Id
                              join ra in db.MstRoleaccesses on r.Id equals ra.Roleid
                              join m in db.MstMenus on ra.Menuid equals m.Id
                              where u.Deleted == false && r.Deleted == false && ra.Deleted == false && m.Deleted == false && u.Role == IdRole && u.Id == IdUser
                              select new VMMstMenu
                              {
                                  Id = m.Id,
                                  Nama = m.Nama,
                                  Path = m.Path

                              }).ToList();

                return result;
        }
    }
}
