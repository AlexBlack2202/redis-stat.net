// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedisInformation.cs" company="">
//   
// </copyright>
//  <summary>
//   RedisInformation.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stats.net.common.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>The redis information dto.</summary>
    [DataContract]
    public class RedisInformation
    {
        #region Public Properties

        /// <summary>Gets or sets the section.</summary>
        [DataMember]
        public string Section { get; set; }

        /// <summary>Gets or sets the values.</summary>
        [DataMember]
        public Dictionary<string, string> Values { get; set; }

        #endregion
    }
}