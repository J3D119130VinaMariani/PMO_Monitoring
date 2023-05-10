using AutoMapper;
using PMO_Monitoring.datamodels;
using PMO_Monitoring.viewmodel;
using PMO_viewmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PMO_repo
{
    public class MstUserRepo
    {
        private readonly PMO_MonitoringContext db;
        VMResponse response = new VMResponse();
       
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
            List<VMMstUser> dataView = (from u in db.MstUsers
                                        join d in db.MstDivisis on u.Divisi equals d.Id into dd
                                        from datadivisi in dd.DefaultIfEmpty()
                                        join j in db.MstJabatans on u.Jabatan equals j.Id into jj
                                        from datajabatan in jj.DefaultIfEmpty()
                                        join r in db.MstRoles on u.Role equals r.Id into rr
                                        from datarole in rr.DefaultIfEmpty()
                                        where u.Deleted == false
                                        select new VMMstUser
                                        {
                                            Id = u.Id,
                                            Nama = u.Nama,
                                            Username = u.Username,
                                            Password = u.Password,
                                            NamaDivisi = datadivisi.Name,
                                            NamaJabatan = datajabatan.Nama,
                                            NamaRole = datarole.Name,
                                            Email = u.Email
                                        }).ToList();
            return dataView;
        }

        public VMMstUser GetById(int id)
        {
            VMMstUser dataView = (from u in db.MstUsers
                                  join d in db.MstDivisis on u.Divisi equals d.Id into dd
                                  from datadivisi in dd.DefaultIfEmpty()
                                  join j in db.MstJabatans on u.Jabatan equals j.Id into jj
                                  from datajabatan in jj.DefaultIfEmpty()
                                  join r in db.MstRoles on u.Role equals r.Id into rr
                                  from datarole in rr.DefaultIfEmpty()
                                  where u.Deleted == false && u.Id == id
                                  select new VMMstUser
                                  {
                                      Id = u.Id,
                                      Nama = u.Nama,
                                      Username = u.Username,
                                      Password = u.Password,
                                      Divisi = u.Divisi,
                                      Jabatan = u.Jabatan,
                                      Role = u.Role,
                                      Email = u.Email,
                                      Createdby = u.Createdby,
                                      Createddate = u.Createddate,
                                      Updatedby = u.Updatedby,
                                      Updateddate = u.Updateddate, 
                                      NamaDivisi = datadivisi.Name,
                                      NamaJabatan = datajabatan.Nama,
                                      NamaRole = datarole.Name
                                  }).FirstOrDefault();
            return dataView;
        }
        public VMResponse Create(VMMstUser dataView)
        {
            MstUser data = db.MstUsers.Where(x => x.Email == dataView.Email && x.Deleted == false).FirstOrDefault();
            if (data == null)
            {
                MstUser dataModel = GetMapper().Map<MstUser>(dataView);
                dataModel.Nama = dataView.Nama;
                dataModel.Divisi = dataView.Divisi;
                dataModel.Username = dataView.Username;
                dataModel.Password = dataView.Password;
                dataModel.Email = dataView.Email;
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
            else
            {
                response.Success = false;
                response.Message = "Email Sudah terdaftar ";
                return response;
            }
            
        }
        public VMResponse Edit(VMMstUser dataView)
        {
            MstUser data = db.MstUsers.Where(x => x.Email == dataView.Email && x.Id!= dataView.Id && x.Deleted == false).FirstOrDefault();
            if(data == null)
            {
                MstUser dataModel = db.MstUsers.Find(dataView.Id);
                dataModel.Nama = dataView.Nama;
                dataModel.Divisi = dataView.Divisi;
                dataModel.Jabatan = dataView.Jabatan;
                dataModel.Role = dataView.Role;
                dataModel.Username = dataView.Username;
                dataModel.Password = dataView.Password;
                dataModel.Email = dataView.Email;
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
                    response.Entity = GetMapper().Map<VMMstUser>(dataModel);
                }
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Email Sudah Digunakan Akun Lain ";
                return response;
            }

        }
        public VMResponse Delete(VMMstUser dataView)
        {
            MstUser dataModel = db.MstUsers.Find(dataView.Id);

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
                response.Entity = GetMapper().Map<VMMstUser>(dataModel);
            }
            return response;
        }

    }
}
