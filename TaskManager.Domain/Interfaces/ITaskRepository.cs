using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();

    Task<TaskItem?> GetByIdAsync(int id);

    Task<int> AddAsync(TaskItem task);

    Task<int> UpdateAsync(TaskItem task);

    Task<int> DeleteAsync(int id);
}