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

    using redis_stats.net.common.Hubs;
    using redis_stats.net.common.Models;

    /// <summary>The SignalR output.</summary>
    class SignalROutput : IOutput
    {
        public void Write(Stats stats)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<RedisStatsHub>();
            context.Clients.All.broadcastMessage(stats);
        }
    }
}
