// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRedisStatistics.cs" company="">
//   
// </copyright>
//  <summary>
//   IRedisStats.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.common.Models
{
    using System.Collections.Generic;

    /// <summary>The RedisStats interface.</summary>
    public interface IRedisStatistics
    {
        #region Public Methods and Operators

        /// <summary>The get all stocks.</summary>
        /// <returns>The <see cref="IEnumerable{Stats}" />.</returns>
        IEnumerable<Stats> GetAllStats();

        #endregion
    }
}