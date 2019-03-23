using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Network {
    public class JsonHandler
    {
        public static string StringToHash(string JsonToHashValue)
        {
            byte[] HashValue;

            //Create a new instance of the UnicodeEncoding class to 
            //convert the string into an array of Unicode bytes.
            UnicodeEncoding UE = new UnicodeEncoding();

            //Convert the string into an array of bytes.
            byte[] MessageBytes = UE.GetBytes(JsonToHashValue);

            //Create a new instance of the SHA1Managed class to create 
            //the hash value.
            SHA1Managed SHhash = new SHA1Managed();

            //Create the hash value from the array of bytes.
            HashValue = SHhash.ComputeHash(MessageBytes);

            return HashValue.ToString();
        }
    
    
        public static string JsonFromList<T>(List<T> listTarget)
        {
            return JsonConvert.SerializeObject(listTarget);
        }
        
        public static string ToJson<T>(T JsonTarget)
        {
            return JsonConvert.SerializeObject(JsonTarget);
        }


    }
}


