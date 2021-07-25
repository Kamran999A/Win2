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

        public static string FileName { get; set; }
        static FileHelper()
        {
            Serializer = new JsonSerializer();
        }

        public static void WriteToJson(IList<Applier> workers)
        {
            using (var fs = new FileStream(FileName, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    using (var jw = new JsonTextWriter(sw))
                    {
                        Serializer.Formatting = Formatting.Indented;

                        Serializer.Serialize(jw, workers);
                    }
                }
            }
        }

        public static IList<Applier> ReadFromJson()
        {
            var workers = new List<Applier>();

            using (var fs = new FileStream(FileName, FileMode.Open))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    using (var jr = new JsonTextReader(sr))
                    {
                        workers = Serializer.Deserialize<List<Applier>>(jr) ?? workers;
                    }
                }
            }

            return workers;
        }
    }
}