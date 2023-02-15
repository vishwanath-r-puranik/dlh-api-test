namespace DLHApi.DAL.Utils
{
    public static class FormatExtension
    {
        public static string ToMVIDFormat (this int mvid)
        {
            var part1 = (mvid / 100000).ToString("0000");
            var part2 = (mvid % 100000).ToString();
            return $"{part1}-{part2}";
        }
    }
}
