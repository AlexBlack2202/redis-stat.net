// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOptions.cs" company="">
//   
// </copyright>
// <summary>
//   The Options interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.Models
{
    /// <summary>The Options interface.</summary>
    public interface IOptions
    {
        /// <summary>Gets the interval.</summary>
        int Interval { get; }

        /// <summary>Gets a value indicating whether verbose.</summary>
        bool Verbose { get; }

        /// <summary>Gets the password.</summary>
        string Password { get; }

        /// <summary>Gets the password.</summary>
        string[] Hosts { get; }
    }
}