using System;

namespace KlinikaVeterinareTI2.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IVaccineRepository Vaccine { get; }
        IAnimalRepository Animal { get; }
        IFamilyRepository Family { get; }
        IRaceRepository Race { get; }
        IVisitRepository Visit { get; }
        ISp_Call Sp_Call { get; }
        void Save();
    }
}
