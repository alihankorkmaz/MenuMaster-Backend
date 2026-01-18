using System.Security.Cryptography;
using System.Text;

namespace MenuMaster.Services
{
    public class OtpHelper
    {
        public static string Generate6DigitCode()
        {
            // between 000000 - 999999 create 6 digit code
            var value = RandomNumberGenerator.GetInt32(0, 1_000_000);
            return value.ToString("D6");
        }

        public static string HashCode(string code)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(code));
            return Convert.ToHexString(hash);
        }
    }
}
