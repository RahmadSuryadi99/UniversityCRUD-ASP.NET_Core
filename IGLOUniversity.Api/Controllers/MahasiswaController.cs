using Basilisk.ViewModel;
using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Mahasiswa;
using Microsoft.AspNetCore.Mvc;

namespace IGLOUniversity.Api.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class MahasiswaController : ControllerBase
        {
            [HttpGet]
            public IEnumerable<GridMahasiswaViewModel> GetAll()
            {
                var result = MahasiswaProvider.GetProvider().GetALLData();
                return result;
            }
            [HttpGet]
            [Route("{id}")]
            public GridMahasiswaViewModel GetById(int id)
            {
                var result = MahasiswaProvider.GetProvider().GetEditMahasiswa(id);
                return result;
            }
            [HttpPost]
            public string Insert(GridMahasiswaViewModel model)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        MahasiswaProvider.GetProvider().Save(model);
                        return "Berhasil Tambah Data";
                    }
                    catch (Exception)
                    {
                        return "Gagal Insert Data";
                    }
                }
                return "Gagal Insert Data";
            }
            [HttpPut]
            public string Edit(GridMahasiswaViewModel model)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        MahasiswaProvider.GetProvider().Save(model);
                        return "Berhasil Update Data";
                    }
                    catch (Exception)
                    {
                        return "Gagal Update Data";
                    }
                }
                return "Gagal Update Data";
            }
          
            [HttpDelete]
            public JsonResultViewModel Delete(int id)
            {
            return MahasiswaProvider.GetProvider().Delete(id);
               
            
            }

        }
}
