using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace DataLayer
{
    public class Facet : Data
    {
        public const string facetUrl = "/facets";
        public Facet()
        {
            arrayName = "facets";
            apiUrl = facetUrl;
        }

        public string Key;
        public string DisplayName;
        public uint Count;

        public override Data read(JsonObject obj)
        {
            Facet facet = new Facet();
            try
            {
                facet.Key = obj.GetNamedString("key");
                facet.DisplayName = obj.GetNamedString("displayName");
                facet.Count = (uint)obj.GetNamedNumber("count");
            }
            catch (Exception e)
            {
                //TODO: log exception
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return facet;
        }

        public override string ToString()
        {
            return DisplayName;
        }

    }
}
