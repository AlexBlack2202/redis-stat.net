// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRedisClient.cs" company="">
//   
// </copyright>
//  <summary>
//   IRedisClient.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.common.Models
{
    using System.Collections.Generic;

    /// <summary>The RedisClient interface.</summary>
    public interface IRedisClient
    {
        #region Public Methods and Operators

        /// <summary>The info.</summary>
        /// <returns>The <see cref="IEnumerable{RedisServerDto}"/>.</returns>
        IEnumerable<RedisServer> Info();

        #endregion
    }
}