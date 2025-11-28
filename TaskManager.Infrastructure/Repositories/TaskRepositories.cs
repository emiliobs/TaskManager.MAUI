using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepositories : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepositories(AppDbContext context)
    {
        this._context = context;
    }

    public Task<int> AddAsync(TaskItem task)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TaskItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaskItem?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(TaskItem task)
    {
        throw new NotImplementedException();
    }
}