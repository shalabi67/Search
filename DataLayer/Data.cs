using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace DataLayer
{
    public  class Data
    {
        protected string apiUrl = "";
        protected string arrayName = "content";
        public async Task<List<T>> readAPIAsync<T>(Filter filter) where T : Data
        {
            Task<string> task = RestAPI.callAsync(apiUrl, filter);
            string json = await task;
            return readAll<T>(json);
        }
        public void readList<T>(JsonArray root, List<T> list) where T : Data
        {
            
            for (uint i = 0; i < root.Count; i++)
            {
                JsonObject obj = root.GetObjectAt(i);
                T data = read(obj) as T;
                if (data == null)
                    continue;
                list.Add(data);
            }
        }
        public List<T> readAll<T>(string json) where T:Data
        {
            //TODO: this should be async
            List<T> list = new List<T>();
            try
            {
                JsonObject jobj = JsonValue.Parse(json).GetObject();
                JsonArray root = jobj.GetNamedArray(arrayName);
                readList(root, list);
            }
            catch(Exception e)
            {
                //TODO: add logging
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return list;
        }

        public virtual Data read(JsonObject obj)
        {
            return null;
        }
       
    }
}
