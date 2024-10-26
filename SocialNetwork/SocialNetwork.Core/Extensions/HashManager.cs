using System.Security.Cryptography;
using System.Text;

namespace SocialNetwork.Core.Extensions
{
    public static class HashManager
    {
        private static string GenerateHash(byte[] dataBytes)
        {
            StringBuilder temp = new StringBuilder();

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(dataBytes);

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    temp.Append(hashBytes[i].ToString("x2"));
                }

                return temp.ToString().Substring(0, 16);
            }
        }

        public static string HashCreate(object data)
        {
            string salt = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");

            byte[] dataBytes = Encoding.UTF8.GetBytes(data.ToString() + salt);

            return GenerateHash(dataBytes);
        }

        //todo db hashlog when db got created

        private static string RecoverHash(object data, DateTime dateTime)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data.ToString() + dateTime);

            return GenerateHash(dataBytes);
        }

        //to check password for sign in or something etc.
        public static bool HashCompare(object data, DateTime oldDateTime, string oldHash)
        {
            //data - password, dateTime - date and time of creating account, oldHasg - saved in db hash of pass or etc.
            string result = RecoverHash(data, oldDateTime);

            return result == oldHash;
        }
    }
}
