#region

using Domain.Abstractions;
using Domain.Common;
using Humanizer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

#endregion

namespace Infrastructure.Persistence.Common;

public class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    public readonly IMongoDatabase MongoDatabase;

    private readonly IMongoCollection<TEntity> _mongoDbCollection;

    protected BaseRepository(IOptions<MongoDbSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        MongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        var collectionName = typeof(TEntity).Name.Pluralize();

        collectionName = collectionName == "People" ? "Persons" : collectionName;

        _mongoDbCollection = MongoDatabase.GetCollection<TEntity>(collectionName);
    }

    public virtual async Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var externalId = await GenerateUniqueExternalIdAsync(cancellationToken);

        entity.ExternalId = externalId;

        await _mongoDbCollection.InsertOneAsync(entity, null, cancellationToken);

        var filterDefinition = Builders<TEntity>.Filter.Eq(document => document.InternalId, entity.InternalId);

        var documentCursor = await _mongoDbCollection.FindAsync<TEntity>(filterDefinition, cancellationToken: cancellationToken);

        return await documentCursor.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<int> GenerateUniqueExternalIdAsync(CancellationToken cancellationToken = default)
    {
        var findOptions = new FindOptions<TEntity>
        {
            Sort = Builders<TEntity>.Sort.Descending(document => document.ExternalId),
            Limit = 1
        };

        var documentCursor = await _mongoDbCollection.FindAsync(FilterDefinition<TEntity>.Empty, findOptions, cancellationToken);

        var document = await documentCursor.FirstOrDefaultAsync(cancellationToken);

        return document is null ? 0 : document.ExternalId + 1;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Pagination? pagination = null,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = filter is null ? FilterDefinition<TEntity>.Empty : Builders<TEntity>.Filter.Where(filter);

        pagination ??= new Pagination();

        var findOptions = new FindOptions<TEntity>
        {
            Skip = (pagination.PageNumber - 1) * pagination.PageSize,
            Limit = pagination.PageSize
        };

        var documentCursor = await _mongoDbCollection.FindAsync(
            filterDefinition,
            findOptions,
            cancellationToken);

        return await documentCursor.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByInternalIdAsync(Guid internalId, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<TEntity>.Filter.Eq(document => document.InternalId, internalId);

        var findOptions = new FindOptions<TEntity>
        {
            Limit = 1
        };

        var documentCursor = await _mongoDbCollection.FindAsync(
            filterDefinition,
            findOptions,
            cancellationToken);

        return await documentCursor.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByExternalIdAsync(int externalId, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<TEntity>.Filter.Eq(document => document.ExternalId, externalId);

        var findOptions = new FindOptions<TEntity>
        {
            Limit = 1
        };

        var documentCursor = await _mongoDbCollection.FindAsync(
            filterDefinition,
            findOptions,
            cancellationToken);

        return await documentCursor.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var filterDefinition = new FilterDefinitionBuilder<TEntity>()
            .Eq(document => document.InternalId, entity.InternalId);

        var result = await _mongoDbCollection.FindOneAndReplaceAsync(filterDefinition, entity, null, cancellationToken);

        return result;
    }

    public virtual async Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var filterDefinition = new FilterDefinitionBuilder<TEntity>()
            .Where(document => document.InternalId == entity.InternalId);

        var result = await _mongoDbCollection.FindOneAndDeleteAsync(filterDefinition, null, cancellationToken);

        return result;
    }
}