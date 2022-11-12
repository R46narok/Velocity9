﻿using Microsoft.EntityFrameworkCore;
using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Application.Interfaces;

public interface IRepository<T, K> where T : IEntity<K>
{
    public List<T> GetAll();
    public Task<T?> GetByIdAsync(K id, bool track = true);

    public Task CreateAsync(T entity);
    public Task DeleteAsync(T entity);
    public Task UpdateAsync(T entity);
}

public class RepositoryBase<T, K, C> : IRepository<T, K>
    where T : class, IEntity<K>
    where C : DbContext
{
    protected readonly C Context;

    protected RepositoryBase(C context)
    {
        Context = context;
    }


    public virtual List<T> GetAll()
    {
        return Context
            .Set<T>()
            .AsQueryable()
            .ToList();
    }

    public virtual async Task<T?> GetByIdAsync(K id, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<T>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Id!.Equals(id));
        }

        return await Context
            .Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public async Task CreateAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
    }
}