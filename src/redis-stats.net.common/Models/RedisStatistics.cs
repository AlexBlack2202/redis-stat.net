// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedisStatistics.cs" company="">
//   
// </copyright>
//  <summary>
//   RedisStatistics.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;

    using Autofac;

    using redis_stat.net.common.Attributes;

    /// <summary>The Redis stats.</summary>
    public class RedisStatistics : IRedisStatistics, IStartable
    {
        #region Fields

        /// <summary>The client.</summary>
        private readonly IRedisClient client;

        /// <summary>The options.</summary>
        private readonly IOptions redisStatsOptions;

        /// <summary>The output.</summary>
        private readonly IOutput output;

        /// <summary>The stats.</summary>
        private readonly Queue<Stats> stats = new Queue<Stats>();

        /// <summary>The update interval.</summary>
        private readonly TimeSpan updateInterval;

        /// <summary>Releases the unmanaged resources and disposes the managed resources used by the Timer</summary>
        private bool disposed;

        /// <summary>The previous server information.</summary>
        private IEnumerable<RedisServer> previousServerInformation;

        /// <summary>The timer.</summary>
        private Timer timer;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="RedisStatistics"/> class.</summary>
        /// <param name="client">The client.</param>
        /// <param name="redisStatsOptions">The options.</param>
        /// <param name="output">The output.</param>
        public RedisStatistics(IRedisClient client, IOptions redisStatsOptions, [WithOptions]IOutput output)
        {
            this.updateInterval = TimeSpan.FromSeconds(redisStatsOptions.Interval);
            this.client = client;
            this.redisStatsOptions = redisStatsOptions;
            this.output = output;
            this.stats.Clear();
            this.Start();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The get all stocks.</summary>
        /// <returns>The <see cref="IEnumerable{Stats}" />.</returns>
        public IEnumerable<Stats> GetAllStats()
        {
            return this.stats;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Perform once-off startup processing.
        /// </summary>
        public void Start()
        {
            this.timer = new Timer(this.GetStatistic, null, this.updateInterval, this.updateInterval);
        }
        
        /// <summary>The stop timer.</summary>
        public void StopTimer()
        {
            if (this.timer != null)
            {
                this.timer.Dispose();
                this.timer = null;
            }
        }

        #endregion

        #region Methods

        /// <summary>The dispose.</summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.timer != null)
                {
                    this.timer.Dispose();
                    this.timer = null;
                }
            }

            this.disposed = true;
        }

        /// <summary>The get value from server.</summary>
        /// <param name="serverInformation">The server information.</param>
        /// <param name="host">The host.</param>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetValueFromServer(IEnumerable<RedisServer> serverInformation, string host, string key)
        {
            var result = (from server in serverInformation
                          where server.ToString() == host && server.Information != null
                          from information in server.Information
                          from value in information.Values
                          where value.Key == key
                          select value.Value).FirstOrDefault() ?? "0";

            return result;
        }

        /// <summary>The humanize number.</summary>
        /// <param name="value">The value.</param>
        /// <param name="byteRepresentation">The byte representation.</param>
        /// <returns>The <see cref="object[]"/>.</returns>
        private static object[] HumanizeNumber(string value, bool byteRepresentation = false)
        {
            try
            {
                var converted = Convert.ToDouble(value, CultureInfo.CurrentCulture);
                var result = converted.ToReadableSIUnit();
                return new object[] { byteRepresentation ? result + "B" : result, converted };
            }
            catch (Exception)
            {
                return new object[] { "-", 0 };
            }
        }

        /// <summary>The ratio.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The <see cref="long"/>.</returns>
        private static double Ratio(double x, double y)
        {
            if (x > 0 || y > 0)
            {
                return x / (x + y) * 100d;
            }

            return 0;
        }

        /// <summary>The search value from server.</summary>
        /// <param name="serverInformation">The server information.</param>
        /// <param name="host">The host.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>The <see cref="IEnumerable{String}"/>.</returns>
        private static IEnumerable<string> SearchValueFromServer(IEnumerable<RedisServer> serverInformation, string host, string pattern)
        {
            var regex = new Regex(pattern);
            var result = from server in serverInformation
                         where server.ToString() == host && server.Information != null
                         from information in server.Information
                         from value in information.Values
                         where regex.IsMatch(value.Key)
                         select value.Value;

            return result;
        }

        /// <summary>The subtract.</summary>
        /// <param name="serverInformation">The server information.</param>
        /// <param name="previousServerInformation">The previous server information.</param>
        /// <param name="host">The host.</param>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="double"/>.</returns>
        private static double Subtract(IEnumerable<RedisServer> serverInformation, IEnumerable<RedisServer> previousServerInformation, string host, string key)
        {
            var info = serverInformation as IList<RedisServer> ?? serverInformation.ToList();
            if (info.All(a => a.ToString() != host) || previousServerInformation == null)
            {
                return 0;
            }

            var x = Convert.ToDouble(GetValueFromServer(info, host, key), CultureInfo.InvariantCulture);
            var y = Convert.ToDouble(GetValueFromServer(previousServerInformation, host, key), CultureInfo.InvariantCulture);
            return x - y;
        }

        /// <summary>The get dynamic properties.</summary>
        /// <param name="serverInformation">The server information.</param>
        /// <param name="previousInformation">The previous server information.</param>
        /// <returns>The <see cref="T:Dictionary{String, Dictionary{String, object[]}}"/>.</returns>
        private Dictionary<string, Dictionary<string, object[]>> GetDynamicProperties(IEnumerable<RedisServer> serverInformation, IEnumerable<RedisServer> previousInformation)
        {
            var dynamicFeatures = Constants.Measures[this.redisStatsOptions.Verbose ? "verbose" : "default"];
            var t = new Dictionary<string, Dictionary<string, object[]>>();
            foreach (var key in dynamicFeatures)
            {
                var c = this.redisStatsOptions.Hosts.ToDictionary(host => host, host => GetFormattedValues(key, host, serverInformation, previousInformation));
                c.Add("sum", key == "at" ? c.FirstOrDefault().Value : HumanizeNumber(c.Sum(a => Convert.ToInt64(a.Value[1])).ToString(CultureInfo.InvariantCulture)));
                t.Add(key, c);
            }

            return t;
        }

        /// <summary>The get value from server.</summary>
        /// <param name="key">The key.</param>
        /// <param name="host">The host.</param>
        /// <param name="serverInformation">The server information.</param>
        /// <param name="previousServerInformation">The previous server information.</param>
        /// <param name="host">The host.</param>
        /// <param name="serverInformation">The server information.</param>
        /// <returns>The <see cref="object[]"/>.</returns>
        private static object[] GetFormattedValues(string key, string host, IEnumerable<RedisServer> serverInformation, IEnumerable<RedisServer> previousServerInformation)
        {
            object[] result;
            string value;
            double hits, misses;

            var info = serverInformation as IList<RedisServer> ?? serverInformation.ToList();
            var prevInfo = previousServerInformation != null ? previousServerInformation as IList<RedisServer> ?? previousServerInformation.ToList() : null;

            switch (key)
            {
                case "at":
                    var datetime = DateTime.Now;
                    var time = datetime.ToString("HH:mm:ss", CultureInfo.CurrentCulture);
                    var dateTime = datetime.ToString("yyyy-MM-dd HH:mm:ss zzzz", CultureInfo.CurrentCulture);
                    object[] formated = { time, dateTime };
                    result = formated;
                    break;
                case "used_cpu_user":
                case "used_cpu_sys":
                    value = Convert.ToInt64(Math.Round(Subtract(info, prevInfo, host, key) * 100)).ToString(CultureInfo.CurrentCulture);
                    result = HumanizeNumber(value);
                    break;
                case "keys":
                    var matches = SearchValueFromServer(info, host, "^db[0-9]+$");
                    value = matches.Sum(a => Convert.ToInt64(a.Split(",".ToCharArray())[0].Split("=".ToCharArray())[1], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
                    result = HumanizeNumber(value);
                    break;
                case "evicted_keys_per_second":
                case "expired_keys_per_second":
                case "keyspace_hits_per_second":
                case "keyspace_misses_per_second":
                case "total_commands_processed_per_second":
                    value = Subtract(info, prevInfo, host, key.Replace("_per_second", string.Empty)).ToString(CultureInfo.InvariantCulture);
                    result = HumanizeNumber(value);
                    break;
                case "used_memory":
                case "used_memory_rss":
                case "aof_current_size":
                case "aof_base_size":
                    value = GetValueFromServer(info, host, key);
                    result = HumanizeNumber(value, true);
                    break;
                case "keyspace_hit_ratio":
                    hits = Convert.ToInt64(GetValueFromServer(info, host, "keyspace_hits"), CultureInfo.InvariantCulture);
                    misses = Convert.ToInt64(GetValueFromServer(info, host, "keyspace_misses"), CultureInfo.InvariantCulture);
                    value = Ratio(hits, misses).ToString(CultureInfo.CurrentCulture);
                    result = HumanizeNumber(value);
                    break;
                case "keyspace_hit_ratio_per_second":
                    hits = Subtract(info, prevInfo, host, "keyspace_hits");
                    misses = Subtract(info, prevInfo, host, "keyspace_misses");
                    result = HumanizeNumber(Ratio(hits, misses).ToString(CultureInfo.InvariantCulture));
                    break;
                default:
                    value = GetValueFromServer(info, host, key);
                    result = Constants.Types[key] == typeof(string) ? new object[] { value, value } : HumanizeNumber(value);
                    break;
            }

            return result;
        }

        /// <summary>The get static properties.</summary>
        /// <param name="serverInformation">The server information.</param>
        /// <returns>The <see cref="Dictionary{String, String[]}"/>.</returns>
        private static Dictionary<string, string[]> GetStaticProperties(IEnumerable<RedisServer> serverInformation)
        {
            var staticMeasures = Constants.Measures["static"];
            return staticMeasures.ToDictionary(measure => measure, measure => GetValuesFromServer(serverInformation, measure).Select(a => a.Value).ToArray());
        }

        /// <summary>The get stat.</summary>
        /// <param name="sender">The sender.</param>
        private void GetStatistic(object sender)
        {
            // Get Stats
            var serverInformation = this.client.Info().ToList();
            var stat = this.ProcessInfo(serverInformation, this.previousServerInformation);
            this.stats.Enqueue(stat);
            if (this.stats.Count > Constants.HistoryLength)
            {
                this.stats.Dequeue();
            }

            this.output.Write(stat);
            this.previousServerInformation = serverInformation;
        }

        /// <summary>The get values from server.</summary>
        /// <param name="serverInformation">The server information.</param>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="IEnumerable{RedisServerDto}"/>.</returns>
        private static IEnumerable<KeyValuePair<string, string>> GetValuesFromServer(IEnumerable<RedisServer> serverInformation, string key)
        {
            var list = new List<KeyValuePair<string, string>>();
            foreach (var server in serverInformation)
            {
                var endpoint = server.ToString();
                if (server.Information != null)
                {
                    list.AddRange(from information in server.Information from value in information.Values where value.Key == key select new KeyValuePair<string, string>(endpoint, value.Value));
                }
                else
                {
                    list.Add(new KeyValuePair<string, string>(endpoint, null));
                }
            }

            return list;
        }

        /// <summary>The process info.</summary>
        /// <param name="serverInformation">The server information.</param>
        /// <param name="previousInformation">The information.</param>
        /// <returns>The <see cref="Stats"/>.</returns>
        private Stats ProcessInfo(IEnumerable<RedisServer> serverInformation, IEnumerable<RedisServer> previousInformation)
        {
            var result = new Stats();
            var info = serverInformation as IList<RedisServer> ?? serverInformation.ToList();
            var epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            result.DateTimeRequestedAt = (long)epoch;
            result.StaticProperties = GetStaticProperties(info);
            result.DynamicProperties = this.GetDynamicProperties(info, previousInformation);
            if (info.Any(a => a.Information == null))
            {
                result.Error = info.Where(a => a.Information == null).Select(a => "Can't connect to: " + a.ToString()).Cast<object>().ToArray();
            }

            return result;
        }

        #endregion
    }
}