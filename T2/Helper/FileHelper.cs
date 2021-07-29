using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using T2;

namespace T2
{
    public static class FileHelper
    {
        private static readonly JsonSerializer Serializer;

        static FileHelper()
        {
            Serializer = new JsonSerializer();
        }

        public static void WriteUserToJson(string fileName, User user)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    using (var jw = new JsonTextWriter(sw))
                    {
                        jw.Formatting = Formatting.Indented;

                        Serializer.Serialize(jw, user);
                    }
                }
            }
        }

        public static User ReadUserFromJson(string fileName)
        {
            User user = null;
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    using (var jr = new JsonTextReader(sr))
                    {
                        user = Serializer.Deserialize<User>(jr);
                    }
                }
            }

            return user;
        }

        public static string CreateFileName(User user) => $"{user.FullName}.json";

     
    }
}