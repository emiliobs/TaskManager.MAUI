using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TaskManager.Applications.Services;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure;
using TaskManager.MAUI.Views;

namespace TaskManager.MAUI.ViewModels;

public partial class TaskListViewModel : ObservableObject
{
    private readonly TaskService _taskService;

    [ObservableProperty]
    private ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private bool isLoading;

    public TaskListViewModel(TaskService taskService)
    {
        this._taskService = taskService;
    }

    [RelayCommand]
    private async Task LoadTaskAsync()
    {
        try
        {
            IsLoading = true;
            var taskList = await _taskService.GetAllTaskAsync();
            tasks = new ObservableCollection<TaskItem>(taskList.OrderByDescending(t => t.CreatedAt));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load task: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task EditTaskAsync(TaskItem task)
    {
        var parameters = new Dictionary<string, object>
        {
            {"TaskId", task.Id },
        };

        await Shell.Current.GoToAsync(nameof(TaskDetailPage), parameters);
    }

    [RelayCommand]
    private async Task DeleteTaskAsync(TaskItem task)
    {
        bool confirm = await Shell.Current.DisplayAlertAsync(
             "Delete Task",
             $"Are you sure want to delete '{task.Title}'?",
             "Delete",
             "Cancel");

        if (confirm)
        {
            await _taskService.DeleteTaskAsync(task.Id);
            await LoadTaskAsync();
        }
    }

    [RelayCommand]
    private async Task ToogleCompletionAsync(TaskItem task)
    {
        await _taskService.ToogleTaskCompletionnAsync(task.Id);
        await LoadTaskAsync();
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadTaskAsync();
    }
}