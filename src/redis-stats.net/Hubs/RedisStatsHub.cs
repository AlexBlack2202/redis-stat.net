// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedisStatsHub.cs" company="">
//   
// </copyright>
//  <summary>
//   RedisStatsHub.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNet.SignalR;

    using redis_stat.net.Models;

    /// <summary>The redis stats hub.</summary>
    public class RedisStatsHub : Hub
    {
        #region Fields

        /// <summary>The redis stats.</summary>
        private readonly IRedisStatistics redisStats;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="RedisStatsHub"/> class.</summary>
        /// <param name="redisStats">The redis stats.</param>
        public RedisStatsHub(IRedisStatistics redisStats)
        {
            this.redisStats = redisStats;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The get all stocks.</summary>
        /// <returns>The <see cref="IEnumerable{Stats}" />.</returns>
        public IEnumerable<Stats> GetAllStats()
        {
            return this.redisStats.GetAllStats();
        }

        /// <summary>
        ///     Called when the connection connects to this hub instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task" />
        /// </returns>
        public override Task OnConnected()
        {
            this.Groups.Add(this.Context.ConnectionId, "Redis");
            return base.OnConnected();
        }

        #endregion
    }
}