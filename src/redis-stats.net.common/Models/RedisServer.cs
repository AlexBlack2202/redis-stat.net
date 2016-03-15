// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedisServer.cs" company="">
//   
// </copyright>
//  <summary>
//   RedisServer.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stats.net.common.Models
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>The Redis servers DTO.</summary>
    [DataContract]
    public class RedisServer
    {
        #region Public Properties

        /// <summary>Gets or sets the host.</summary>
        [DataMember]
        public string Host { get; set; }

        /// <summary>Gets or sets the information.</summary>
        [DataMember]
        public IEnumerable<RedisInformation> Information { get; set; }

        /// <summary>Gets or sets the port.</summary>
        [DataMember]
        public string Port { get; set; }

        /// <summary>Gets or sets the error.</summary>
        [DataMember]
        public string Error { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.Host, this.Port);
        }

        #endregion
    }
}