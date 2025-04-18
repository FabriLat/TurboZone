﻿namespace Domain.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    List<T> GetAll();
    T? GetById<TId>(TId id);
    T Add(T entity);
    void Delete(T entity);
    void Update(T entity);
}
