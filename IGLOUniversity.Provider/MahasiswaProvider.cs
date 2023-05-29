using Basilisk.ViewModel;
using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.Mahasiswa;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IGLOUniversity.Provider
{
    public class MahasiswaProvider : BaseProvider
    {
        private static MahasiswaProvider _instance = new MahasiswaProvider();
        public static MahasiswaProvider GetProvider()
        {
            return _instance;
        }

        public bool Save(GridMahasiswaViewModel model)
        {
            try
            {

                var mahasiswa = new Mahasiswa();
                MappingModel<Mahasiswa, GridMahasiswaViewModel>(mahasiswa, model);
                MahasiswaRepository.GetRepository().Insert(mahasiswa);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<GridMahasiswaViewModel> GetALLData()
        {

            var allMahasiswa = MahasiswaRepository.GetRepository().GetAll().AsEnumerable();
            var model = (from mhs in allMahasiswa
                         select new GridMahasiswaViewModel()
                         {
                             Alamat = mhs.Alamat,
                             AsalSma = mhs.AsalSma,
                             Id = mhs.Id,
                             NamaBelakang = mhs.NamaBelakang,
                             NamaDepan = mhs.NamaDepan,
                             NamaTengah = mhs.NamaTengah,
                             Nim = mhs.Nim,
                             NomorHp = mhs.NomorHp
                         }).AsEnumerable();
            return model;
        }
        public IndexMahasiswaViewModel GetIndex(int page = 1)
        {
            var allMahasiswa = MahasiswaRepository.GetRepository().GetAll();

            var grid = (from m in allMahasiswa
                        select new GridMahasiswaViewModel()
                        {
                            Id = m.Id,
                            Alamat = string.IsNullOrEmpty(m.Alamat) ? "N/A" : m.Alamat,
                            AsalSma = string.IsNullOrEmpty(m.AsalSma) ? "N/A" : m.AsalSma,
                            NamaBelakang = m.NamaBelakang,
                            NamaDepan = m.NamaDepan,
                            NamaTengah = m.NamaTengah,
                            Nim = string.IsNullOrEmpty(m.Nim) ? "N/A" : m.Nim,
                            NomorHp = string.IsNullOrEmpty(m.NomorHp) ? "N/A" : m.NomorHp
                        });


            var skip = GetSkip(page);


            var model = new IndexMahasiswaViewModel
            {
                TotalData = grid.Count(),
                TotalHalaman = TotalHalaman(grid.Count()),
                Grid = grid.Skip(skip).Take(_totalDataPerPage).ToList()
            };

            return model;

        }

        public GridMahasiswaViewModel GetEditMahasiswa(int id)
        {
            var data = MahasiswaRepository.GetRepository().GetSingle(id);
            var model = new GridMahasiswaViewModel();
            MappingModel<GridMahasiswaViewModel, Mahasiswa>(model, data);
            return model;
        }
        public bool PostEditMahasiswa(GridMahasiswaViewModel model)
        {

            try
            {
                var oldCatagory = MahasiswaRepository.GetRepository().GetSingle(model.Id);
                var prod = new Mahasiswa();
                MappingModel<Mahasiswa, GridMahasiswaViewModel>(oldCatagory, model);

                return MahasiswaRepository.GetRepository().Update(oldCatagory);

            }
            catch (Exception)
            {
                return false;
            }

        }
        public JsonResultViewModel Delete(int id)
        {
            var result = new JsonResultViewModel();
            try
            {
                var CheckDistribusi = DistribusiMatakuliahRepository.GetRepository().GetAll().Any(a => a.IdMahasiswa == id);
                var CheckUser = UserRepository.GetRepository().GetAll().Any(a => a.MahasiswaId == id);
                if (CheckDistribusi == false && CheckUser == false)
                {
                    if (MahasiswaRepository.GetRepository().Delete(id))
                    {
                        result.Success = true;
                        result.Message = "Berhasil menghapus";

                    };
                }
                else
                {
                    result.Success = false;
                    result.Message = "Gagal Menghapus, data mahasiswa digunakan pada table distribusi dan user";
                }

             
            }
            catch
            {
                result.Success = false;
                result.Message = "Gagal Menghapus";
            }
            return result;
        }
    }
}