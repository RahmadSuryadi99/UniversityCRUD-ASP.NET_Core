using IGLOUniversity.DataAccess.Models;

namespace IGLOUniversity.Repository
{
    public class MahasiswaRepository : BaseRepository, IRepository<Mahasiswa>
    {
        private static MahasiswaRepository instance = new MahasiswaRepository();

        public static MahasiswaRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var Mahasiswa = _context.Mahasiswas.SingleOrDefault(a => a.Id == (int)id);
                    _context.Mahasiswas.Remove(Mahasiswa);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Mahasiswa> GetAll()
        {
            var context = new IGLOUniversityContext();

            return context.Mahasiswas;
        }

        public Mahasiswa GetSingle(object id)
        {
            var Mahasiswa = new Mahasiswa();
            using (var context = new IGLOUniversityContext())
            {
                Mahasiswa = context.Mahasiswas.SingleOrDefault(a => a.Id == (int)id);
            }
            return Mahasiswa;
        }

        public bool Insert(Mahasiswa model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    _context.Mahasiswas.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Mahasiswa model)
        {
            try
            {
                using (var _context = new IGLOUniversityContext())
                {
                    var mahasiswa = _context.Mahasiswas.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel<Mahasiswa, Mahasiswa>(mahasiswa, model);
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