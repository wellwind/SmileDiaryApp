using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmileDiaryApp
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        public static string serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
