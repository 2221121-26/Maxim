namespace C_tasks
{
    public static class zad_2
    {
        static void Main(string[] args)
        {
            string c = Console.ReadLine();
            var invalidChars = c.Where(c => !char.IsLower(c));
            bool isValid = true;
            foreach (char ch in c)
            {
                if (ch < 'a' || ch > 'z')
                {
                    Console.Write(ch);
                    isValid = false;
                }
            }
            if (isValid)
            {
                if (c.Length % 2 == 0)
                {
                    Console.WriteLine($"{perevorot(c[..(c.Length / 2)]) + perevorot(c.Substring(c.Length / 2))}");
                }
                else
                {
                    Console.WriteLine($"{perevorot(c) + c}");
                }
            }

            static string perevorot(string message)
            {
                string hc = "";
                foreach (var ch in message)
                {
                    hc = ch + hc;
                }
                return hc;
            }
        }
    }
}
