using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure;

namespace TaskManager.Applications.Services;

public class TaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        this._repository = repository;
    }

    public async Task<List<TaskItem>> GetAllTaskAsync() => await _repository.GetAllAsync();

    public async Task<TaskItem?> GetTaskByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<bool> CreateTaskAsync(TaskItem task)
    {
        var result = await _repository.AddAsync(task);
        return result > 0;
    }

    public async Task<bool> UpdateTaskAsync(TaskItem task)
    {
        var result = await _repository.UpdateAsync(task);
        return result > 0;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var result = await _repository.DeleteAsync(id);
        return result > 0;
    }

    public async Task<bool> ToogleTaskCompletionnAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task is null)
        {
            return false;
        }

        task.IsCompleted = !task.IsCompleted;
        var resul = await _repository.UpdateAsync(task);
        return resul > 0;
    }
}