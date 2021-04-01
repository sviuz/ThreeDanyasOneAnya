using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace MessengerServer
{
    public class Salt
    {
        public Salt(){ }
        //thread-safe
        public byte[] GetSalt()
        {
            byte[] Salt;
            string filename = @".\..\..\..\Storage\salt";
            Salt = File.ReadAllBytes(filename);
            return Salt;
        }
        public static byte[] GetHash(byte[] data)
        {
            byte[] Salt;
            string filename = @".\..\..\..\Storage\salt";
            Salt = File.ReadAllBytes(filename);
            byte[] result;
            using (var hmac = new HMACSHA256(Salt))
            {
                result = hmac.ComputeHash(data);
            }
            return result;
        }
    }
}
