using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContet;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContet = appDbContext;
        }

        public IEnumerable<Pie> GetAllPies()
        {
            return _appDbContet.Pies;
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContet.Pies.FirstOrDefault(p => p.Id == pieId);
        }
    }
}
