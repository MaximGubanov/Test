using System;
using System.Linq;

namespace GasNetwork.Converters
{
    public class IntToByteStringConverter
    {
        public static string ConvertIntToByteString(int number)
        {
            return new string(Convert.ToString((byte)number, 2).Reverse().ToArray());
        }
    }
}