using AlliedSolutionsGlobalSdnBhd.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace AlliedSolutionsGlobalSdnBhd.Services
{
    public class AccessDatabaseService<T> where T : class
{
    private readonly string _connectionString;

    // Constructor that takes IConfiguration
    public AccessDatabaseService(IConfiguration configuration)
    {
        // Retrieve the connection string from the configuration
        _connectionString = configuration.GetConnectionString("AccessDbConnection");
    }

    public async Task<List<T>> GetDataFromAccessDatabaseAsync(string tableName)
    {
        var result = new List<T>();
        using (var connection = new OleDbConnection(_connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = $"SELECT * FROM {tableName}";
                using (var command = new OleDbCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            T data = Activator.CreateInstance<T>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var propertyName = reader.GetName(i);
                                    var property = typeof(T).GetProperty(propertyName);

                                    if (property != null && !reader.IsDBNull(i))
                                    {
                                        var value = reader.GetValue(i);

                                        // Only process DateTime, string, and bool
                                        if (property.PropertyType == typeof(DateTime))
                                        {
                                            property.SetValue(data, Convert.ToDateTime(value));
                                        }
                                        else if (property.PropertyType == typeof(string))
                                        {
                                            property.SetValue(data, value.ToString());
                                        }
                                        else if (property.PropertyType == typeof(bool?))
                                        {
                                            bool? nullableBool = reader.IsDBNull(i) ? (bool?)null : reader.GetBoolean(i);
                                            property.SetValue(data, nullableBool);
                                        }

                                        else
                                        {
                                        }
                                    }
                                    else if (property != null && reader.IsDBNull(i))
                                    {
                                        property.SetValue(data, property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null);
                                    }
                                }
                                result.Add(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, etc.)
            }
        }
        return result;
    }
}

}
