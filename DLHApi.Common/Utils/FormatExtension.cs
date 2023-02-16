namespace DLHApi.Common.Utils
{
    public static class FormatExtension
    {
        public static string ToMVIDFormat (this int mvid)
        {
            var part1 = (mvid / 100000).ToString("0000");
            var part2 = (mvid % 100000).ToString();
            return $"{part1}-{part2}";
        }

        //to avoid sonar cube null exceptions.
        public static string? ToDateTime(DateTime? date)
        {
            if (date != null)
            {
                string dt1 = ((DateTime)date).ToString("yyyy/MM/dd");
                string dt2 = dt1.Replace("-", "/");
                return dt2;
            }

            return "";
        }

        //to avoid sonar cube null exceptions.
        public static string? ToNameFormat(string? firstName, string? middleName, string? lastName)
        {
            string FullName = "";
            if (!string.IsNullOrEmpty(lastName))
                FullName = FullName + lastName;
            if (!string.IsNullOrEmpty(firstName))
                FullName = (!string.IsNullOrEmpty(FullName)) ? FullName + ", "  + firstName : firstName;
            if (!string.IsNullOrEmpty(middleName))
                FullName = (!string.IsNullOrEmpty(FullName)) ? FullName + " " + middleName : middleName;
            
            return FullName;
        }
    }
}
