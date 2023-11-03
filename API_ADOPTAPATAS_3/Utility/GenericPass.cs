namespace API_ADOPTAPATAS_3.Utility
{
    public class GenericPass
    {
        public string GenerateRandomPassword()
        {
            int passwordLength = 8; // Longitud de la contraseña
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";
            Random random = new Random();

            char[] password = new char[passwordLength];
            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(password);
        }

    }
}
