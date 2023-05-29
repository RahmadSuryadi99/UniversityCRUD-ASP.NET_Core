using Basilisk.ViewModel;
using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.Kelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class KelasProvider : BaseProvider
    {
        private static KelasProvider _instance = new KelasProvider();
        public static KelasProvider GetProvider()
        {
            return _instance;
        }

        public bool Save(UpsertKelasViewModel model)
        {
            try
            {

                var Kela = new Kela();
                MappingModel<Kela, UpsertKelasViewModel>(Kela, model);
                KelasRepository.GetRepository().Insert(Kela);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IndexKelasViewModel GetIndex(int page = 1)
        {
            var allKela = KelasRepository.GetRepository().GetAll();
            var mataKul = MatakuliahRepository.GetRepository().GetAll();
            var grid = (from k in allKela.ToList()
                        join m in mataKul.ToList() on k.IdMatakuliah equals m.Id
                        select new GridKelasViewModel()
                        {
                            Id = k.Id,
                            IdMatakuliah = k.IdMatakuliah,
                            Kapasitas = k.Kapasitas,
                            Matakuliah = m.Nama,
                            Nama = k.Nama,
                            Semester = k.Semester,
                            Sks = m.Sks,

                        }).ToList();


            var skip = GetSkip(page);


            var model = new IndexKelasViewModel
            {
                TotalData = grid.Count(),
                TotalHalaman = TotalHalaman(grid.Count()),
                Grid = grid.Skip(skip).Take(_totalDataPerPage).ToList()
            };

            return model;

        }

        public GridKelasViewModel GetEditKela(int id)
        {
            var data = KelasRepository.GetRepository().GetSingle(id);
            var model = new GridKelasViewModel();
            MappingModel<GridKelasViewModel, Kela>(model, data);
            return model;
        }
        public bool PostEditKela(UpsertKelasViewModel model)
        {

            try
            {
                var olddata = KelasRepository.GetRepository().GetSingle(model.Id);
                var prod = new Kela();
                MappingModel<Kela, UpsertKelasViewModel>(olddata, model);

                return KelasRepository.GetRepository().Update(olddata);

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
                var CheckDistribusi = DistribusiMatakuliahRepository.GetRepository().GetAll().Any(a => a.IdKelas == id);
                if (CheckDistribusi == false)
                {
                    if (KelasRepository.GetRepository().Delete(id))
                    {
                        result.Success = true;
                        result.Message = "Berhasil menghapus";
                    };
                }
                else
                {
                    result.Success = false;
                    result.Message = "Gagal Menghapus, data Kela digunakan pada table distribusi dan user";
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
