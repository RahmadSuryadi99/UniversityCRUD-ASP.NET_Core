using Basilisk.ViewModel;
using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.Matakuliah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class MatakuliahProvider : BaseProvider
    {
        private static MatakuliahProvider _instance = new MatakuliahProvider();
        public static MatakuliahProvider GetProvider()
        {
            return _instance;
        }
        public IndexMatakuliahViewModel GetIndex(int page = 1)
        {
            var data = MatakuliahRepository.GetRepository().GetAll();
            var grid = (from d in data
                         select new GridMatakuliahViewModel
                         {
                             Id = d.Id,
                             Nama = d.Nama,
                             Deskripsi = d.Deskripsi,
                             Sks = d.Sks,
                         }).ToList();
            var skip = GetSkip(page);


            var model = new IndexMatakuliahViewModel
            {
                TotalData = grid.Count(),
                TotalHalaman = TotalHalaman(grid.Count()),
                Grid = grid.Skip(skip).Take(_totalDataPerPage).ToList()
            };

            return model;

        }
        public bool Insert(UpsertMatakuliahViewModel model)
        {

            try
            {
                var mataKuliah = new Matakuliah();
                MappingModel<Matakuliah, UpsertMatakuliahViewModel>(mataKuliah, model);
                if (MatakuliahRepository.GetRepository().Insert(mataKuliah))
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

        public bool Update(UpsertMatakuliahViewModel model)
        {
            try
            {
                var mataKuliah = new Matakuliah();
                MappingModel<Matakuliah, UpsertMatakuliahViewModel>(mataKuliah, model);
                if (MatakuliahRepository.GetRepository().Update(mataKuliah))
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
        public bool Save(UpsertMatakuliahViewModel model)
        {
            if (model.Id == 0)
            {
                return Insert(model);
            }
            else
            {

                return Update(model);
            }
        }
        public GridMatakuliahViewModel GetEdit(int id)
        {
            var data = MatakuliahRepository.GetRepository().GetSingle(id);
            var model = new GridMatakuliahViewModel();
            MappingModel<GridMatakuliahViewModel, Matakuliah>(model, data);
            return model;
        }
        public bool Delete(int id)
        {
            var checkKelas = KelasRepository.GetRepository().GetAll().Any(a=>a.IdMatakuliah == id);
            if (!checkKelas)
            {
                MatakuliahRepository.GetRepository().Delete(id);
                return true;
            }
            return false;
        }
        public List<DropDownViewModel> GetDataMatakuliah()
        {

           
            var matkuls = MatakuliahRepository.GetRepository().GetAll();
            //var kelas = KelasRepository.GetRepository().GetAll();
            var data = (from m in matkuls
                        select new DropDownViewModel
                        {
                            IntValue = m.Id,
                            Text = $"{m.Nama}",
                        }).ToList();
            return data;
        }

    }

}
