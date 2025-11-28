using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Data;

public class AppDbContext
{
    private SQLiteAsyncConnection? _database;

    public async Task InitializeAsync(string dbPath)
    {
        if (_database != null)
            return;

        _database = new SQLiteAsyncConnection(dbPath);
        await _database.CreateTableAsync<TaskItem>();
    }

    public SQLiteAsyncConnection Database
    {
        get
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Database not initialized. Call InitializeAsync first.");
            }

            return _database;
        }
    }
}