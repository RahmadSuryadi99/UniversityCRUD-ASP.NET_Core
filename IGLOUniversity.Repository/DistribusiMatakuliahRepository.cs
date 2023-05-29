using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class DistribusiMatakuliahRepository : BaseRepository, IRepository<DistribusiMatakuliah>
    {
        private static DistribusiMatakuliahRepository instance = new DistribusiMatakuliahRepository();

        public static DistribusiMatakuliahRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var DistribusiMatakuliah = _context.DistribusiMatakuliahs.SingleOrDefault(a => a.Id == (int)id);
                    _context.DistribusiMatakuliahs.Remove(DistribusiMatakuliah);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<DistribusiMatakuliah> GetAll()
        {
            var context = new IGLOUniversityContext();

            return context.DistribusiMatakuliahs;
        }

        public DistribusiMatakuliah GetSingle(object id)
        {
            var DistribusiMatakuliah = new DistribusiMatakuliah();
            using (var context = new IGLOUniversityContext())
            {
                DistribusiMatakuliah = context.DistribusiMatakuliahs.SingleOrDefault(a => a.Id == (int)id);
            }
            return DistribusiMatakuliah;
        }

        public bool Insert(DistribusiMatakuliah model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    _context.DistribusiMatakuliahs.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(DistribusiMatakuliah model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var DistribusiMatakuliah = _context.DistribusiMatakuliahs.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel<DistribusiMatakuliah, DistribusiMatakuliah>(DistribusiMatakuliah, model);
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