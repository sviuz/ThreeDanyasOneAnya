using System;

namespace Other {
    public static class StringGenerator
    {
        public static string GenerateString() => Guid.NewGuid().ToString("N");

        public static string GenerateDate() {
            DateTime start = new DateTime(1995, 1, 1);
            return start.AddDays(new Random().Next((DateTime.Today - start).Days)).ToString("d").Replace("/",".");
        }
    }
}
