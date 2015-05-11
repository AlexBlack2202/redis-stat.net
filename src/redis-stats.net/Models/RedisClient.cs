// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedisConnection.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the RedisConnection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;

    using StackExchange.Redis;

    /// <summary>The redis client.</summary>
    public class RedisClient : IRedisClient
    {
        /// <summary>The client.</summary>
        private readonly ConnectionMultiplexer client;

        /// <summary>Initializes a new instance of the <see cref="RedisClient"/> class.</summary>
        /// <param name="options">The options.</param>
        public RedisClient(IOptions options)
        {
            var configurationOptions = new ConfigurationOptions()
                              {
                                  AllowAdmin = true,
                                  AbortOnConnectFail = false,
                                  Password = options.Password,
                                  ConnectTimeout = 50000
                              };

            Array.ForEach(options.Hosts, s => configurationOptions.EndPoints.Add(s));
            this.client = ConnectionMultiplexer.Connect(configurationOptions);
        }

        /// <summary>The info.</summary>
        /// <returns>The <see cref="IEnumerable{RedisServerDto}"/>.</returns>
        public IEnumerable<RedisServer> Info()
        {
            var endpoints = this.client.GetEndPoints().Cast<DnsEndPoint>();
            var servers = (from endpoint in endpoints
                           let server = this.client.GetServer(endpoint)
                           select
                               new RedisServer
                                   {
                                       Host = endpoint.Host,
                                       Port = endpoint.Port.ToString(CultureInfo.InvariantCulture),
                                       Information = server.Info().Select(a =>new RedisInformation{
                                           Section = a.Key,
                                           Values = a.ToDictionary(b => b.Key,c => c.Value)})
                                   }).ToList();
            return servers;
        }
    }
}