using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWith.Filters.impl
{
    public class MagicBytesFilter : IFilter
    {
        public static IFilter Build(object config) {
            return new MagicBytesFilter(
                ((IEnumerable<object>) config)
                    .Select(n => n.ToString())
                    .SelectMany(n => {
                                    if (n.StartsWith("0x")) {
                                        return new[] { byte.Parse(n.Substring(2), NumberStyles.HexNumber) };
                                    } else {
                                        return Encoding.ASCII.GetBytes(n);
                                    }
                                })
                    .ToArray()
            );
        }

        private readonly byte[] _firstBytes;

        public MagicBytesFilter(byte[] firstBytes) {
            _firstBytes = firstBytes ?? throw new ArgumentNullException(nameof(firstBytes));
        }

        public bool Accepts(string path) {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) {
                return false;
            }

            using (var file = fileInfo.OpenRead()) {
                for (int i = 0, b = _firstBytes[i]; i < _firstBytes.Length; b = _firstBytes[++i]) {
                    int fb = file.ReadByte();
                    if (fb == -1 || fb != b) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
