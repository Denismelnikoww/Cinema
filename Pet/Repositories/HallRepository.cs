using Pet.Models;

namespace Pet.Repositories
{
    public class HallRepository
    {
        private readonly AppDbContext _context;

        public HallRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }


    }
}
