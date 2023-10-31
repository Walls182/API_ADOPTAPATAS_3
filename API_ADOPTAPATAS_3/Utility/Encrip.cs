using System.Security.Cryptography;
using System.Text;

namespace API_ADOPTAPATAS_3.Utility
{
    public class Encrip
    {
        public string HashPassword(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                // Convierte la contraseña en bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Calcula el hash SHA-512
                byte[] hashBytes = sha512.ComputeHash(passwordBytes);

                // Convierte el hash en una representación hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Calcular el hash de la contraseña proporcionada
            string enteredPasswordHash = HashPassword(enteredPassword);

            // Comparar los hashes para verificar la contraseña
            return string.Equals(enteredPasswordHash, storedPasswordHash);
        }
    }
}
