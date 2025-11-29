using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TaskManager.Applications.Services;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;
using TaskManager.MAUI.ViewModels;
using TaskManager.MAUI.Views;

namespace TaskManager.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Database
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "task.db3");
            builder.Services.AddSingleton(s =>
            {
                var context = new AppDbContext();
                context.InitializeAsync(dbPath).Wait();
                return context;
            });

            // Repositories
            builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

            // Services
            builder.Services.AddSingleton<TaskService>();

            // ViewModels
            builder.Services.AddTransient<TaskListViewModel>();
            builder.Services.AddTransient<TaskDetailViewModel>();

            // Views
            builder.Services.AddTransient<TaskListPage>();
            builder.Services.AddTransient<TaskDetailPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}