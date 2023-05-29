using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class KelasRepository : BaseRepository, IRepository<Kela>
    {
        private static KelasRepository instance = new KelasRepository();

        public static KelasRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var kela = _context.Kelas.SingleOrDefault(a => a.Id == (int)id);
                    _context.Kelas.Remove(kela);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Kela> GetAll()
        {
            var context = new IGLOUniversityContext();

            return context.Kelas;
        }

        public Kela GetSingle(object id)
        {
            var kela = new Kela();
            using (var context = new IGLOUniversityContext())
            {
                kela = context.Kelas.SingleOrDefault(a => a.Id == (int)id);
            }
            return kela;
        }

        public bool Insert(Kela model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    _context.Kelas.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Kela model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var kela = _context.Kelas.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel<Kela, Kela>(kela, model);
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