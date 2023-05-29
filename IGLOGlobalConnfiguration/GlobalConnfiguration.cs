using Basilisk.ViewModel;

namespace IGLOGlobalConnfiguration
{
    public class GlobalConfiguration
    {
        public static string _passowrdDefault = "indocyber";
        public static IEnumerable<MenuViewModel> GetMenuByRole(string roleName)
        {
            var data = new List<MenuViewModel>()
            {
                new MenuViewModel{Role="Admin",Controller="Home",Action="Index",Title="Home" },
                new MenuViewModel{Role="Admin",Controller="Kelas",Action="Index",Title="Kelas" },
                new MenuViewModel{Role="Admin",Controller="Mahasiswa",Action="Index",Title="Mahasiswa" },
                new MenuViewModel{Role="Admin",Controller="Matakuliah",Action="Index",Title="Matakuliah" },
                new MenuViewModel{Role="Admin",Controller="User",Action="Index",Title="User" },
                new MenuViewModel{Role="Admin",Controller="StudiRencana",Action="Index",Title="Rencana Studi" },

                new MenuViewModel{Role="Mahasiswa",Controller="Home",Action="Index",Title="Home" },
                new MenuViewModel{Role="Mahasiswa",Controller="RencanaStudi",Action="Index",Title="Rencana Studi" },

            };
            return data;
        }
    }
}