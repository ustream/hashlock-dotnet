using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace Ustream
{
    public class Hash : Dictionary<string, string>
    {
        public string getHash(string secret, int ttlSeconds)
        {
            string hashExpire = getExpireTimestamp(ttlSeconds);
            this.Add("hashExpire", hashExpire);

            string signString = concatSignData(secret);
            string signature = md5Sign(signString);

            this.Add("hash", signature);
            return toJson();
        }

        private string concatSignData(string secret)
        {
            string signData = "";
            foreach (KeyValuePair<string, string> data in this)
            {
                signData += data.Value + "|";
            }
            signData += secret;

            return signData;
        }

        private string md5Sign(string signString)
        {
            byte[] md5 = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(signString));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < md5.Length; i++)
            {
                sBuilder.Append(md5[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private string toJson()
        {
            var hashDataList = this.Select(p => new Dictionary<string, string>() { { p.Key, p.Value } });
            return JsonConvert.SerializeObject(hashDataList);
        }

        private string getExpireTimestamp(int ttl)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long hashExpireTimestamp = (long)(DateTime.Now - sTime).TotalSeconds + ttl;
            return hashExpireTimestamp.ToString();
        }
    }
}