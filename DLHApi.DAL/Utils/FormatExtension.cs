using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.DAL.Utils
{
    public static class FormatExtension
    {
        public static string ToMVIDFormat (this decimal mvid)
        {
            var part1 = Math.Floor((mvid / 100000)).ToString("0000");
            var part2 = (mvid % 100000).ToString();
            return $"{part1}-{part2}";
        }
    }
}
