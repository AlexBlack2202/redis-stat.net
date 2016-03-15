// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalROutput.cs" company="">
//   
// </copyright>
// <summary>
//   The SignalR output.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.Models
{
    using Microsoft.AspNet.SignalR;

    using redis_stat.net.common.Hubs;
    using redis_stat.net.common.Models;

    /// <summary>The SignalR output.</summary>
    internal class SignalROutput : IOutput
    {
        #region Public Methods and Operators

        /// <summary>The write.</summary>
        /// <param name="stats">The stats.</param>
        public void Write(Stats stats)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<RedisStatsHub>();
            context.Clients.All.broadcastMessage(stats);
        }

        #endregion
    }
}