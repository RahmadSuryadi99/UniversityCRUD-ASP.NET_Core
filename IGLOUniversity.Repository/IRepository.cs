using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public interface IRepository<Model>
    {
        public IQueryable<Model> GetAll();
        public Model GetSingle(object id);
        public bool Insert(Model model);
        public bool Update(Model model);
        public bool Delete(object id);

    }
}
