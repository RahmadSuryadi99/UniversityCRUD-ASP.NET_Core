using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        private static UserRepository instance = new UserRepository();

        public static UserRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var User = _context.Users.SingleOrDefault(a => a.Id == (int)id);
                    _context.Users.Remove(User);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<User> GetAll()
        {
            var context = new IGLOUniversityContext();

            return context.Users;
        }

        public User GetSingle(object id)
        {
            var user = new User();
            using (var context = new IGLOUniversityContext())
            {
                user = context.Users.SingleOrDefault(a => a.Id == (int)id);
            }
            return user;
        }

        public bool Insert(User model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    _context.Users.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(User model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var user = _context.Users.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel<User, User>(user, model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string GetRole(string username)
        {
            using (var context = new IGLOUniversityContext())
            {
                var checkUser = context.Users.SingleOrDefault(a => a.UserName == username).Status == "Aktif";
                if (checkUser)
                {
                    var cekAdmin = context.Users.SingleOrDefault(a => a.UserName == username).IsAdmin == true;
                    if (cekAdmin)
                    {
                        return "Admin";
                    }
                    else
                    {

                        return "Mahasiswa";
                    }

                }
                else
                {
                    return "Guest";
                }
            }
        }
        public bool GetIsAuthentication(string username, string password)
        {
            using (var context = new IGLOUniversityContext())
            {
                //var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
                var accountByUsername = context.Users.SingleOrDefault(a => (a.UserName == username && a.Password == password)&&a.Status=="Aktif");
             
                if (accountByUsername != null)
                {
                    return true;
                }
                return false;
            }
        }

    }

}