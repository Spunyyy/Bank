using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class Utils
    {
        public static string HashString(string inputString)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Konvertujte vstupní řetězec na bajtové pole a hashujte ho
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));

                // Konvertujte hash na řetězec a vraťte ho
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateAccountNumber()
        {
            Random rnd = new Random();
            string number = "";
            for(int i = 0; i<16; i++)
            {
                number += rnd.Next(0, 10).ToString();
            }
            return number;
        }
    }
}
