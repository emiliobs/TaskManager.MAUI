using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<List<TaskItem>> GetAllAsync() => await _context.Database.Table<TaskItem>().ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(int id) => await _context.Database.Table<TaskItem>().FirstOrDefaultAsync(t => t.Id == id);

    public async Task<int> AddAsync(TaskItem task)
    {
        task.CreatedAt = DateTime.Now;
        return await _context.Database.InsertAsync(task);
    }

    public async Task<int> UpdateAsync(TaskItem task)
    {
        return await _context.Database.UpdateAsync(task);
    }

    public async Task<int> DeleteAsync(int id) => await _context.Database.DeleteAsync<TaskItem>(id);
}