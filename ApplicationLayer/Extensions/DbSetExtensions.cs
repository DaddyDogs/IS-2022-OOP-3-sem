using ApplicationLayer.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Extensions;

public static class DbSetExtensions
{
    public static T GetEntity<T>(this DbSet<T> set, Guid id, CancellationToken cancellationToken)
        where T : class
    {
        T? entity = set.Find(new object[] { id }, cancellationToken);

        if (entity is null)
            throw NotFoundException.EntityNotFoundException<T>(id);

        return entity;
    }
}