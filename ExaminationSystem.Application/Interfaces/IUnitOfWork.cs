using ExaminationSystem.Application.Interfaces.Repositories;
using ExaminationSystem.Domain.Common;

namespace ExaminationSystem.Application.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
