// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="">
//   
// </copyright>
// <summary>
//   The constants.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.common.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>The constants.</summary>
    public static class Constants
    {
        /// <summary>The history length.</summary>
        public const int HistoryLength = 50;

        /// <summary>The stat table rows.</summary>
        public const int StatTableRows = 10;

        /// <summary>The default format.</summary>
        public const string DefaultFormat = " {0,8} {0,2} {0,2} {0,4} {0,3} {0,5} {0,5} {0,5} {0,5} {0,5} {0,5} {0,5} {0,5}";

        /// <summary>The verbose format.</summary>
        public const string VerboseFormat = " {0,8} {0,2} {0,2} {0,4} {0,3} {0,5} {0,5} {0,5} {0,5} {0,5} {0,5} {0,5} {0,5}";

        /// <summary>The labels.</summary>
        public static readonly Dictionary<string, string> Labels = new Dictionary<string, string> 
                                                                                        {
                                                                                            { "at", "time" },
                                                                                            { "used_cpu_user", "us" },
                                                                                            { "used_cpu_sys", "sy" },
                                                                                            { "connected_clients", "cl" },
                                                                                            { "blocked_clients", "bcl" },
                                                                                            { "used_memory", "mem" },
                                                                                            { "used_memory_rss", "rss" },
                                                                                            { "mem_fragmentation_ratio", "frag" },
                                                                                            { "total_commands_processed", "cmd" },
                                                                                            { "total_commands_processed_per_second", "cmd/s" },
                                                                                            { "expired_keys", "exp" },
                                                                                            { "expired_keys_per_second", "exp/s" },
                                                                                            { "evicted_keys", "evt" },
                                                                                            { "evicted_keys_per_second", "evt/s" },
                                                                                            { "keys", "keys" },
                                                                                            { "keyspace_hits", "hit" },
                                                                                            { "keyspace_hits_per_second", "hit/s" },
                                                                                            { "keyspace_misses", "mis" },
                                                                                            { "keyspace_misses_per_second", "mis/s" },
                                                                                            { "keyspace_hit_ratio", "hit%" },
                                                                                            { "keyspace_hit_ratio_per_second", "hit%/s" },
                                                                                            { "aof_current_size", "aofcs" },
                                                                                            { "aof_base_size", "aofbs" },
                                                                                            { "rdb_changes_since_last_save", "chsv" },
                                                                                            { "pubsub_channels", "psch" },
                                                                                            { "pubsub_patterns", "psp" }
                                                                                        };

        /// <summary>The format.</summary>
        public static readonly Dictionary<string, string> Format = new Dictionary<string, string> 
                                                                                        {
                                                                                            { "at", "{0,8}" },
                                                                                            { "used_cpu_user", "{0,2}" },
                                                                                            { "used_cpu_sys", "{0,2}" },
                                                                                            { "connected_clients", "{0,5}" },
                                                                                            { "blocked_clients", "{0,3}" },
                                                                                            { "used_memory", "{0,5}" },
                                                                                            { "used_memory_rss", "{0,5}" },
                                                                                            { "mem_fragmentation_ratio", "{0,5}" },
                                                                                            { "total_commands_processed", "{0,5}" },
                                                                                            { "total_commands_processed_per_second", "{0,5}" },
                                                                                            { "expired_keys", "{0,5}" },
                                                                                            { "expired_keys_per_second", "{0,5}" },
                                                                                            { "evicted_keys", "{0,5}" },
                                                                                            { "evicted_keys_per_second", "{0,5}" },
                                                                                            { "keys", "{0,5}" },
                                                                                            { "keyspace_hits", "hit" },
                                                                                            { "keyspace_hits_per_second", "{0,5}" },
                                                                                            { "keyspace_misses", "{0,5}" },
                                                                                            { "keyspace_misses_per_second", "{0,5}" },
                                                                                            { "keyspace_hit_ratio", "{0,5}" },
                                                                                            { "keyspace_hit_ratio_per_second", "{0,6}" },
                                                                                            { "aof_current_size", "{0,5}" },
                                                                                            { "aof_base_size", "{0,5}" },
                                                                                            { "rdb_changes_since_last_save", "{0,5}" },
                                                                                            { "pubsub_channels", "{0,5}" },
                                                                                            { "pubsub_patterns", "{0,5}" }
                                                                                        };

        /// <summary>The colors.</summary>
        public static readonly  Dictionary<string, string[]> Colors = new Dictionary<string, string[]> 
                                                                                        {
                                                                                            { "at", new[] { "bold" } },
                                                                                            { "used_cpu_user", new[] { "yellow", "bold" } },
                                                                                            { "used_cpu_sys", new[] { "yellow" } },
                                                                                            { "connected_clients", new[] { "cyan", "bold" } },
                                                                                            { "blocked_clients", new[] { "cyan", "bold" } },
                                                                                            { "used_memory", new[] { "green" } },
                                                                                            { "used_memory_rss", new[] { "green" } },
                                                                                            { "mem_fragmentation_ratio", new[] { "green" } },
                                                                                            { "total_commands_processed", new[] { "blue", "bold" } },
                                                                                            { "total_commands_processed_per_second", new[] { "blue", "bold" } },
                                                                                            { "expired_keys", new[] { "red" } },
                                                                                            { "expired_keys_per_second", new[] { "red" } },
                                                                                            { "evicted_keys", new[] { "red", "bold" } },
                                                                                            { "evicted_keys_per_second", new[] { "red", "bold" } },
                                                                                            { "keys", new[] { "bold" } },
                                                                                            { "keyspace_hits", new[] { "magenta", "bold" } },
                                                                                            { "keyspace_hits_per_second", new[] { "magenta", "bold" } },
                                                                                            { "keyspace_misses", new[] { "magenta" } },
                                                                                            { "keyspace_misses_per_second", new[] { "magenta" } },
                                                                                            { "keyspace_hit_ratio", new[] { "magenta", "bold" } },
                                                                                            { "keyspace_hit_ratio_per_second", new[] { "magenta", "bold" } },
                                                                                            { "aof_current_size", new[] { "cyan" } },
                                                                                            { "aof_base_size", new[] { "cyan" } },
                                                                                            { "rdb_changes_since_last_save", new[] { "green", "bold" } },
                                                                                            { "pubsub_channels", new[] { "cyan", "bold" } },
                                                                                            { "pubsub_patterns", new[] { "cyan", "bold" } }
                                                                                        };

        /// <summary>The measures.</summary>
        public static readonly Dictionary<string, string[]> Measures = new Dictionary<string, string[]>
                                                                  {
                                                                      {
                                                                          "static",
                                                                          new[]
                                                                              {
                                                                                  "redis_version",
                                                                                  "redis_mode",
                                                                                  "process_id",
                                                                                  "uptime_in_seconds",
                                                                                  "uptime_in_days",
                                                                                  "role",
                                                                                  "connected_slaves",
                                                                                  "aof_enabled",
                                                                                  "rdb_bgsave_in_progress",
                                                                                  ////"bgsave_in_progress",
                                                                                  "rdb_last_save_time"
                                                                                  ////"last_save_time"
                                                                              }
                                                                      },
                                                                      {
                                                                          "default",
                                                                          new[]
                                                                              {
                                                                                  "at",
                                                                                  "used_cpu_user",
                                                                                  "used_cpu_sys",
                                                                                  "connected_clients",
                                                                                  "blocked_clients",
                                                                                  "used_memory",
                                                                                  "used_memory_rss",
                                                                                  "keys",
                                                                                  "total_commands_processed_per_second",
                                                                                  "expired_keys_per_second",
                                                                                  "evicted_keys_per_second",
                                                                                  "keyspace_hit_ratio_per_second",
                                                                                  "keyspace_hits_per_second",
                                                                                  "keyspace_misses_per_second",
                                                                                  "aof_current_size",
                                                                              }
                                                                      },
                                                                      {
                                                                          "verbose",
                                                                          new[]
                                                                              {
                                                                                  "at",
                                                                                  "used_cpu_user",
                                                                                  "used_cpu_sys",
                                                                                  "connected_clients",
                                                                                  "blocked_clients",
                                                                                  "used_memory",
                                                                                  "used_memory_rss",
                                                                                  "mem_fragmentation_ratio",
                                                                                  "keys",
                                                                                  "total_commands_processed_per_second",
                                                                                  "total_commands_processed",
                                                                                  "expired_keys_per_second",
                                                                                  "expired_keys",
                                                                                  "evicted_keys_per_second",
                                                                                  "evicted_keys",
                                                                                  "keyspace_hit_ratio_per_second",
                                                                                  "keyspace_hits_per_second",
                                                                                  "keyspace_hits",
                                                                                  "keyspace_misses_per_second",
                                                                                  "keyspace_misses",
                                                                                  "aof_current_size",
                                                                                  "aof_base_size",
                                                                                  "rdb_changes_since_last_save",
                                                                                  "changes_since_last_save",
                                                                                  "pubsub_channels",
                                                                                  "pubsub_patterns"
                                                                              }
                                                                      }
                                                                  };

        /// <summary>The types.</summary>
        public static readonly Dictionary<string, Type> Types = new Dictionary<string, Type>()
                                                    {
                                                        { "at", typeof(float) },
                                                        { "used_cpu_user", typeof(float) },
                                                        { "used_cpu_sys", typeof(float) },
                                                        { "connected_clients", typeof(int) },
                                                        { "blocked_clients", typeof(int) },
                                                        { "used_memory", typeof(int) },
                                                        { "used_memory_rss", typeof(int) },
                                                        { "mem_fragmentation_ratio", typeof(float) },
                                                        { "total_commands_processed", typeof(int) },
                                                        { "total_commands_processed_per_second", typeof(float) },
                                                        { "expired_keys", typeof(int) },
                                                        { "expired_keys_per_second", typeof(float) },
                                                        { "evicted_keys", typeof(int) },
                                                        { "evicted_keys_per_second", typeof(float) },
                                                        { "keys", typeof(int) },
                                                        { "keyspace_hits", typeof(int) },
                                                        { "keyspace_hits_per_second", typeof(float) },
                                                        { "keyspace_misses", typeof(int) },
                                                        { "keyspace_misses_per_second", typeof(float) },
                                                        { "keyspace_hit_ratio", typeof(int) },
                                                        { "keyspace_hit_ratio_per_second", typeof(float) },
                                                        { "aof_current_size", typeof(int) },
                                                        { "aof_base_size", typeof(int) },
                                                        { "changes_since_last_save", typeof(int) },
                                                        { "rdb_changes_since_last_save", typeof(int) },
                                                        { "pubsub_channels", typeof(int) },
                                                        { "pubsub_patterns", typeof(int) },
                                                        { "redis_version", typeof(string) },
                                                        { "redis_mode", typeof(string) },
                                                        { "process_id", typeof(int) },
                                                        { "uptime_in_seconds", typeof(int) },
                                                        { "uptime_in_days", typeof(int) },
                                                        { "role", typeof(string) },
                                                        { "connected_slaves", typeof(int) },
                                                        { "aof_enabled", typeof(int) },
                                                        { "rdb_bgsave_in_progress", typeof(int) },
                                                        { "bgsave_in_progress", typeof(int) },
                                                        { "rdb_last_save_time", typeof(int) },
                                                        { "last_save_time", typeof(int) }
                                                    };
    }
}