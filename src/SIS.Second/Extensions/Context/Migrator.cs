namespace SIS.Second.Extensions.Context
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Npgsql;

    public static class Migrator
    {
        /// <summary>
        /// Функция, мигрирующая контекст данных.
        /// </summary>
        public static void UseAutomaticMigrations<T>(this IApplicationBuilder app, Action<T>? seedAction = null)
            where T : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            if (!context.Database.IsRelational())
                return;
            using var connection = context.Database.GetDbConnection();

            context.Database.Migrate();

            switch (connection)
            {
                case NpgsqlConnection npgConnection:
                    npgConnection.Open();
                    npgConnection.ReloadTypes();
                    break;
            }

            seedAction?.Invoke(context);
            context.SaveChanges();
        }
    }
}