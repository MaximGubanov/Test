using System;
using System.Linq;

namespace GasNetwork.Extensions
{
    public static class IntExtensions
    {
        public static string ConvertToByteString(this int number)
        {
            return new string(Convert.ToString((byte)number, 2).Reverse().ToArray());
        }

        public static string ConvertStatusLockToString(this int number, string correctorType) 
        {
            //TODO: нужно разобраться с правильным возвращением
            if (correctorType == "Lis200")
                if (number == 1)
                    return "«Замок потребителя»; «Замок поставщика»; «Замок производителя»; «Калибровочный замок»;";

            if (correctorType == "Flowsic")
                switch (number)
                {
                    case 0: return "Гость";
                    case 1: return "Пользователь 3";
                    case 2: return "Пользователь 2";
                    case 3: return "Пользователь 1";
                    case 4: return "Авторизированный пользователь 3";
                    case 5: return "Авторизированный пользователь 2";
                    case 6: return "Авторизированный пользователь 1";
                }

            return "";
        }
    }
}