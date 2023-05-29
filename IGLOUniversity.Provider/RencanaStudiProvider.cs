using IGLOUniversity.Provider;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.RencanaStudi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IgloUniversity.Provider
{
    public class RencanaStudiProvider : BaseProvider
    {
        private static RencanaStudiProvider instance = new RencanaStudiProvider();
        public static RencanaStudiProvider GetProvider() { return instance; }



        public IndexRencanaStudiViewModel GetIndex(int page = 1)
        {
            var mahasis = MahasiswaRepository.GetRepository().GetAll();
            var disttr = DistribusiMatakuliahRepository.GetRepository().GetAll();
            var kelas = KelasRepository.GetRepository().GetAll();
            var matakuliah = MatakuliahRepository.GetRepository().GetAll();

            var grid = (from mhs in mahasis.ToList()

                        join d in disttr.ToList() on mhs.Id equals d.IdMahasiswa
                        into a
                        from d in a.DefaultIfEmpty()

                        join k in kelas.ToList() on d?.IdKelas equals k.Id
                        into b
                        from k in b.DefaultIfEmpty()

                        join m in matakuliah.ToList() on k?.IdMatakuliah equals m.Id
                        into c
                        from m in c.DefaultIfEmpty()

                        group m?.Sks by mhs into g
                        //group new { m?.Sks, k.Kapasitas } by new { mhs,k,m } into g
                        select new GridRencanaStudiVIewMode
                        {
                            Id = g.Key.Id,
                            //Sks = (int)g.Sum(a => a.Sks)
                            Alamat = g.Key.Alamat,
                            AsalSma = g.Key.AsalSma,
                            NamaBelakang = g.Key.NamaBelakang,
                            NamaDepan = g.Key.NamaDepan,
                            NamaTengah = g.Key.NamaTengah,
                            Nim = g.Key.Nim,
                            NomorHp = g.Key.NomorHp,
                            Sks = (int)g.Sum()
                        }).ToList();

            var mahasiswa = MahasiswaRepository.GetRepository().GetAll();

            var grids = (from mhs in mahasiswa
                         select new GridRencanaStudiVIewMode
                         {
                             Id = mhs.Id,
                             Alamat = mhs.Alamat,
                             AsalSma = mhs.AsalSma,
                             NamaBelakang = mhs.NamaBelakang,
                             NamaDepan = mhs.NamaDepan,
                             NamaTengah = mhs.NamaTengah,
                             Nim = mhs.Nim,
                             NomorHp = mhs.NomorHp,
                             Sks = 0,
                         }).AsEnumerable();

            var skip = GetSkip(page);
            var model = new IndexRencanaStudiViewModel
            {
                TotalData = grid.Count(),
                TotalHalaman = TotalHalaman(grid.Count()),
                Grid = grid.Skip(skip).Take(_totalDataPerPage).ToList()
            };
            return model;

        }

        public DetailRencanaStudiViewModel GetDetail(int id)
        {
            var dataMhs = MahasiswaRepository.GetRepository().GetSingle(id);


            var dataKelas = (from dis in DistribusiMatakuliahRepository.GetRepository().GetAll().ToList()
                             join mhs in MahasiswaRepository.GetRepository().GetAll().ToList() on dis.IdMahasiswa equals mhs.Id
                             join kelas in KelasRepository.GetRepository().GetAll().ToList() on dis.IdKelas equals kelas.Id
                             join matkul in MatakuliahRepository.GetRepository().GetAll().ToList() on kelas.IdMatakuliah equals matkul.Id
                             where mhs.Id == id
                             select new GridDetailViewModel
                             {
                                 Id = dis.Id,
                                 Nama = kelas.Nama,
                                 IdMatakuliah = matkul.Id,
                                 Matakuliah = matkul.Nama,
                                 SKS = matkul.Sks,
                                 Semester = kelas.Semester,
                                 Kapasitas = kelas.Kapasitas,
                                 Status = dis.Status
                             }).ToList();

            var model = new DetailRencanaStudiViewModel
            {
                AsalSma = dataMhs.AsalSma,
                Grid = dataKelas,
                Nama = $"{dataMhs.NamaDepan} {dataMhs.NamaTengah} {dataMhs.NamaBelakang}",
                NIM = dataMhs.Nim,

            };
            return model;
        }
    }
}
