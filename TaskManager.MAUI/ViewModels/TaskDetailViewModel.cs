using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManager.Applications.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.MAUI.ViewModels;

[QueryProperty(nameof(TaskId), "TaskId")]
public partial class TaskDetailViewModel : ObservableObject
{
    private readonly TaskService _taskService;

    [ObservableProperty]
    private int taskId;

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private DateTime? dueDate;

    [ObservableProperty]
    private TaskPriority priority = TaskPriority.Medium;

    [ObservableProperty]
    private bool isEditMode;

    public TaskDetailViewModel(TaskService taskService)
    {
        this._taskService = taskService;
    }

    private void OnTaskIdChange(int value)
    {
        if (value > 0)
        {
            IsEditMode = true;
            LoadTaskAsync(value);
        }
    }

    private async void LoadTaskAsync(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task != null)
        {
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
            Priority = task.Priority;
        }
    }

    [RelayCommand]
    private async Task SaveTaskAsyn()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            await Shell.Current.DisplayAlertAsync("Validation", "Please enter a title.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Description))
        {
            await Shell.Current.DisplayAlertAsync("Validation", "Please enter a description.", "OK");
            return;
        }

        var task = new TaskItem
        {
            Id = taskId,
            Title = Title,
            Description = Description,
            DueDate = DueDate,
            Priority = priority,
        };

        var success = IsEditMode ? await _taskService.UpdateTaskAsync(task) : await _taskService.CreateTaskAsync(task);

        if (success)
        {
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlertAsync("Error", "Failed to save task.", "OK");
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}