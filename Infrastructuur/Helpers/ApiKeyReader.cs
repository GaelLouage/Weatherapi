using Infrastructuur.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;    

namespace Infrastructuur.Extensions
{
    public static class ApiKeyReader
    {
        public static ApiKey KeyReader(string fileName)
        {
            // read file into a string and deserialize JSON to a type
            var api = JsonConvert.DeserializeObject<ApiKey>(File.ReadAllText(fileName));

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (ApiKey)serializer.Deserialize(file, typeof(ApiKey));
            }
        }
    }
}
