using System.Security.Cryptography;
using System.Text;

namespace API_ADOPTAPATAS_3.Utility
{
    public class Encrip
    {
        public string HashPassword(string password)
        {
            using (SHA512 sha212 = SHA512.Create())
            {
                // Convierte la contraseña en bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Calcula el hash SHA-512
                byte[] hashBytes = sha212.ComputeHash(passwordBytes);

                // Convierte el hash en una representación hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }


    }
}
