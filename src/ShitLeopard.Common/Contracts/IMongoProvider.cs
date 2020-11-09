using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using MongoDB.Entities;

namespace ShitLeopard.Common.Contracts
{
    public interface IMongoProvider
    {
        public IMongoDatabase ConnectAsync();

        public IMongoCollection<T> GetMongoCollection<T>(string name = null);
    }

    public class MongoProvider : IMongoProvider
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private static readonly ConcurrentDictionary<string, string> _keyValuePairs = new ConcurrentDictionary<string, string>();

        public MongoProvider(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public IMongoDatabase ConnectAsync()
        {

           
            var connectionString = _connectionStringProvider.GetConnectionString();
            var client = new MongoClient(connectionString);
            return client.GetDatabase("ShitLeopard");
        }

        public IMongoCollection<T> GetMongoCollection<T>(string name = null)
        {
            name = name ?? _keyValuePairs.GetOrAdd(typeof(T).FullName, (key) =>
             {
                 var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(DisplayAttribute)) as DisplayAttribute;
                 if (attribute != null)
                 {
                     return attribute.Name;
                 }
                 return typeof(T).Name;
             });

            return ConnectAsync().GetCollection<T>(name);
        }
    }
}