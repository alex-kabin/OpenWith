using System;
using System.Collections.Generic;
using System.IO;

namespace OpenWith.Filters.impl
{
    public class SizeFilter : IFilter
    {
        public static SizeFilter Build(object config) {
            long min = long.MinValue;
            long max = long.MaxValue;

            if (config is IDictionary<object, object> dict) {
                if (dict.ContainsKey("min")) {
                    object minObj = dict["min"];
                    if (minObj is string minStr) {
                        min = ParseSizeString(minStr);
                    } else {
                        min = long.Parse(minObj.ToString());
                    }
                }
                if (dict.ContainsKey("max")) {
                    object maxObj = dict["max"];
                    if (maxObj is string maxStr) {
                        max = ParseSizeString(maxStr);
                    } else {
                        max = long.Parse(maxObj.ToString());
                    }
                }
            }
            else if (config is string str) {
                var parts = str.Split(':');
                if (parts.Length == 2) {
                    var minStr = parts[0].Trim();
                    if (!string.IsNullOrEmpty(minStr)) {
                        min = ParseSizeString(minStr);
                    }
                    var maxStr = parts[1].Trim();
                    if (!string.IsNullOrEmpty(maxStr)) {
                        max = ParseSizeString(maxStr);
                    }
                } else {
                    throw new FormatException("Bad size format: " + str);
                }
            }
            if (min > max) {
                throw new ArgumentOutOfRangeException("min", "min should be not greater than max");
            }
            return new SizeFilter(min, max);
        }

        private static long ParseSizeString(string str) {
            if (string.IsNullOrWhiteSpace(str)) {
                throw new FormatException("Empty size");
            }

            str = str.Trim().ToLowerInvariant();

            var size = str;
            var k = 1;
            if (str.EndsWith("b")) {
                size = str.Substring(0, str.Length - 1);
                k = 1;
            } else if (str.EndsWith("k")) {
                size = str.Substring(0, str.Length - 1);
                k = 1024;
            } else if (str.EndsWith("m")) {
                size = str.Substring(0, str.Length - 1);
                k = 1024 * 1024;
            } else if (str.EndsWith("g")) {
                size = str.Substring(0, str.Length - 1);
                k = 1024 * 1024 * 1024;
            }

            try {
                return long.Parse(size) * k;
            }
            catch (Exception ex) {
                throw new FormatException($"Bad size '{str}'", ex);
            }
        }

        public SizeFilter(long minSize, long maxSize) {
            MinSize = minSize;
            MaxSize = maxSize;
        }

        public long MinSize { get; set; }
        public long MaxSize { get; set; }

        public bool Accepts(string path) {
            var fileInfo = new FileInfo(path);
            return fileInfo.Exists && fileInfo.Length >= MinSize && fileInfo.Length <= MaxSize;
        }
    }
}
