using System;

namespace ValidationCore
{
    public static class Validation
    {
        public static int TryParseIntValueInLine(string line)
        {
            int result;

            if (!int.TryParse(line, out result))
            {
                return int.MinValue;
            }

            return result;
        }

    }
}
