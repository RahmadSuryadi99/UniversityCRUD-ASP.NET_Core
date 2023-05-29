using Basilisk.ViewModel;
using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class UserProvider : BaseProvider
    {
        private static UserProvider instance = new UserProvider();
        public static UserProvider GetProvider()
        {
            return instance;
        }

        public IndexUserViewModel GetIndex(int page = 1)
        {
            var users = UserRepository.GetRepository().GetAll();
            var mahasiswas = MahasiswaRepository.GetRepository().GetAll();
            var grid = (from u in users.ToList()
                        join m in mahasiswas.ToList() on u.MahasiswaId equals m.Id into g
                        from gr in g.DefaultIfEmpty()
                        select new GridUserViewModel
                        {
                            Id = u.Id,
                            IsAdmin = u.IsAdmin,
                            Mahasiswa = gr == null ? "N/A" : string.IsNullOrEmpty(gr.NamaDepan) ? "N/A" : $"{gr.NamaDepan} {gr.NamaTengah} {gr.NamaBelakang}",
                            MahasiswaId = u.MahasiswaId,
                            Password = u.Password,
                            Status = u.Status,
                            UserName = u.UserName,
                        }).ToList();

            var skip = GetSkip(page);

            var model = new IndexUserViewModel
            {

                TotalData = grid.Count(),
                TotalHalaman = TotalHalaman(grid.Count()),
                Grid = grid.Skip(skip).Take(_totalDataPerPage).ToList()
            };
            return model;
        }

        public List<DropDownViewModel> GetDataStatus()
        {
            var data = new List<DropDownViewModel> {
            new DropDownViewModel{ StringValue = "Aktif",Text ="Aktif"  },
            new DropDownViewModel{ StringValue = "Terkunci",Text ="Terkunci"  },
            new DropDownViewModel{ StringValue = "Tidak aktif",Text ="Tidak aktif"  }            
            };
            return data;
        }
        public List<DropDownViewModel> GetDataMahasiswa()
        {

            var mahasiswas = MahasiswaRepository.GetRepository().GetAll();
            var user = UserRepository.GetRepository().GetAll();
            var data = (from m in mahasiswas.ToList()
                        from u in user.ToList()
                        where m.Id != u.MahasiswaId
                        select new DropDownViewModel
                        {
                            IntValue= m.Id,
                            Text = $"{m.NamaDepan} {m.NamaTengah} {m.NamaBelakang}",
                        }).ToList();
            return data;
        }
        public bool Insert(UpsertUserViewModel model)
        {

            try
            {
                var user = new User();
                MappingModel<User,UpsertUserViewModel>(user, model);
                if (UserRepository.GetRepository().Insert(user))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

            return false;
            }
        }

        public bool Update(UpsertUserViewModel model)
        {
            try
            {
                var user = new User();
                MappingModel<User, UpsertUserViewModel>(user, model);
                if (UserRepository.GetRepository().Update(user))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }
            public bool Save(UpsertUserViewModel model)
        {
            if (model.Id == 0)
            {
              return  Insert(model);
            }
            else
            {

                return Update(model);
            }
        }
        public UpsertUserViewModel GetEdit(int id)
        {
            var oldUser = UserRepository.GetRepository().GetSingle(id);
            var model = new UpsertUserViewModel();
            MappingModel(model, oldUser);
            return model;
        }
        public bool Delete(int id)
        {
            try
            {
                var hasil = UserRepository.GetRepository().Delete(id);
                if (!hasil)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool IsAuthentication(LoginViewModel model)
        {
          return  UserRepository.GetRepository().GetIsAuthentication(model.Username, model.Password);
        }
        public string GetRoleName(string username)
        {
            var  userRepository = new UserRepository();
            return userRepository.GetRole(username);
        }
    }
}
