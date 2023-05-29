using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class MatakuliahRepository : BaseRepository, IRepository<Matakuliah>
    {
        private static MatakuliahRepository instance = new MatakuliahRepository();

        public static MatakuliahRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var matakuliah = _context.Matakuliahs.SingleOrDefault(a => a.Id == (int)id);
                    _context.Matakuliahs.Remove(matakuliah);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Matakuliah> GetAll()
        {
            var context = new IGLOUniversityContext();

            return context.Matakuliahs;
        }

        public Matakuliah GetSingle(object id)
        {
            var matakuliah = new Matakuliah();
            using (var context = new IGLOUniversityContext())
            {
                matakuliah = context.Matakuliahs.SingleOrDefault(a => a.Id == (int)id);
            }
            return matakuliah;
        }

        public bool Insert(Matakuliah model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    _context.Matakuliahs.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Matakuliah model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var matakuliah = _context.Matakuliahs.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel<Matakuliah, Matakuliah>(matakuliah, model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }

}