using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

using System.Collections;

namespace DataLayer
{
    public class FacetGroup : Data
    {
        //TODO: design decision, we may have three groups for male, female and both. this will depend on the device.
        private static Dictionary<string, List<Facet>> groups = new Dictionary<string, List<Facet>>();
        private static string gender = "nothing";  //no gender is defined call initGroups

        //gets a set of facets that starts with a value.
        public static async Task<List<Facet>> getFacets(string value, string gender)
        {
            //TODO: later on we need to sort this based on items number and order them ascending.            
            //TODO: problem with multithreading.            
            if (!gender.Equals(FacetGroup.gender))
            {
                await initGroups(gender);
            }

            //TODO: this should be async
            List<Facet> list = new List<Facet>();
            List<Facet> mainList = new List<Facet>();
            foreach (string groupName in groups.Keys)
            {
                addToList(mainList, list, groupName, value);
            }
            mainList.AddRange(list);
            return mainList;

        }

        private static void addToList(List<Facet> mainList, List<Facet> list, string groupName, string value)
        {
            string lowerValue = value.Trim().ToLower();
            List<Facet> facets = groups[groupName];
            foreach(Facet facet in facets)
            {
                string lower = facet.DisplayName.ToLower();
                if (lower.StartsWith(lowerValue))
                {
                    mainList.Add(facet);
                }
                else if (lower.Contains(lowerValue))
                {
                    list.Add(facet);
                }
            }
        }

        private static Filter getFilter(string gender)
        {
            Filter filter = new Filter().addGender(gender);
            return filter;
        }

        private static async Task initGroups(string gender)
        {
            Task<string> task = RestAPI.callAsync(Facet.facetUrl, getFilter(gender));
            string json = await task;

            try
            {
                JsonArray root = JsonValue.Parse(json).GetArray();

                //TODO: this code should be async.
                groups = new Dictionary<string, List<Facet>>();
                for (uint i = 0; i < root.Count; i++)
                {
                    JsonObject obj = root.GetObjectAt(i);
                    FacetGroup data = new FacetGroup();
                    data = data.read(obj) as FacetGroup;
                    if (data == null)
                        continue;

                    groups.Add(data.GroupName, data.Items);
                }

            }
            catch (Exception e)
            {
                //TODO: add logging
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

        }

        public FacetGroup()
        {
            arrayName = "facets";
            apiUrl = Facet.facetUrl;
        }

        public string GroupName;
        public List<Facet> Items = new List<Facet>();
        public override Data read(JsonObject obj)
        {
            FacetGroup facetGroup = new FacetGroup();
            Facet facet = new Facet();
            try
            {
                facetGroup.GroupName = obj.GetNamedString("filter");
                JsonArray root = obj.GetNamedArray(arrayName);

                facet.readList<Facet>(root, facetGroup.Items);
            }
            catch (Exception e)
            {
                //TODO: log exception
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return facetGroup;
        }
    }
}
