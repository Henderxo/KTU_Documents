using RabbitMQ.Client;
using System;

namespace Plain.RabbitMQ
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private bool _disposed;

        public ConnectionProvider(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("RabbitMQ URL is null or empty.");
            }

            Console.WriteLine($"RabbitMQ URL: {url}");

            _factory = new ConnectionFactory
            {
                Uri = new Uri(url)
            };

            try
            {
                _connection = _factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating RabbitMQ connection: {ex.Message}");
                throw;
            }
        }


        public IConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _connection?.Close();

            _disposed = true;
        }
    }
}
