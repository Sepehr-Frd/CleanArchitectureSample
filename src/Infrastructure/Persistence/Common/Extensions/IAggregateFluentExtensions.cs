using Humanizer;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Common.Extensions;

public static class AggregateFluentExtensions
{
    // TODO: Get object class name
    public static IAggregateFluent<T> IncludeRelations<T>(this IAggregateFluent<T> aggregateFluent, IEnumerable<Expression<Func<T, object?>>> includes)
    {
        foreach (var include in includes)
        {
            var foreignCollectionName = include.ReturnType.Name.Pluralize();
            var localFieldName = include.ReturnType.Name + "ExternalId";
            var foreignFieldName = include.ReturnType.Name;

            aggregateFluent.Lookup(foreignCollectionName, localFieldName, "_id", foreignFieldName);
        }

        return aggregateFluent;
    }
}