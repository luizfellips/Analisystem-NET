namespace Analisystem.Helper
{
    public static class CpfValidator
    {
        
        public static int getFirstDigit(string cpfNumber)
        {
            int sum = 0;
            int firstDigit = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += ((cpfNumber[i] - 48) * (10 - i));
            }
            if (sum % 11 < 2)
            {
                firstDigit = 0;
            }
            else if (sum % 11 >= 2)
            {
                firstDigit = 11 - (sum % 11);
            }
            return firstDigit;
        }

        public static int getSecondDigit(string cpfNumber)
        {
            int sum = 0;
            int secondDigit = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += ((cpfNumber[i] - 48) * (11 - i));
            }
            if (sum % 11 < 2)
            {
                secondDigit = 0;
            }
            else if (sum % 11 >= 2)
            {
                secondDigit = 11 - (sum % 11);
            }
            return secondDigit;
        }

        public static string CpfFormatter(string unformattedCPF)
        {
            string value = "";
            for(char i = (char)0;i < 11; i++)
            {
                value += unformattedCPF[i];
                if(i == 2 || i == 5)
                {
                    value += "." ;
                }
                if(i == 8)
                {
                    value += "-";
                }
            }
            return value;
        }
    }
}
