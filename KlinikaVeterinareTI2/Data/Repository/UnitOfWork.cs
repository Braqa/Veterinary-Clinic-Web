using KlinikaVeterinareTI2.Data.Repository.IRepository;

namespace KlinikaVeterinareTI2.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            User = new UserRepository(_context);
            Vaccine = new VaccineRepository(_context);
            Animal = new AnimalRepository(_context);
            Family = new FamilyRepository(_context);
            Race = new RaceRepository(_context);
            Visit = new VisitRepository(_context);
            Sp_Call = new Sp_Call(_context);
        }

        public IUserRepository User { get; private set; }
        public IVaccineRepository Vaccine { get; private set; }
        public IAnimalRepository Animal { get; private set; }
        public IFamilyRepository Family { get; private set; }
        public IRaceRepository Race { get; private set; }
        public IVisitRepository Visit { get; private set; }
        public ISp_Call Sp_Call { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
