using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    //using builder pattern
    public class Filter
    {
        private const string gender = "gender";
        private const string fullText = "fullText";
        public string UrlFilter
        {
            get;
            private set;
        }  
        public Filter()
        {
            UrlFilter = "?";
        }

        

        public Filter addFullText(string fullText)
        {
            return addFilter(Filter.fullText, fullText);
        }
        public Filter addGender(string gender)
        {
            return addFilter(Filter.gender, gender);
        }
        public Filter addPaging(uint pageNumber)
        {
            addAnd();
            UrlFilter += Paging.getPaging(pageNumber);
            return this;
        }

        private Filter addFilter(string tag, string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return this;
            string encoded = System.Net.WebUtility.UrlEncode(filter);
            addAnd();
            UrlFilter += tag + "=" + encoded;
            return this;
        }

        private void addAnd()
        {
            if (UrlFilter.Length > 1)
                UrlFilter += "&";
        }

        //TODO: this is allow setting just for testing, later on will be removed to readonly.
        public void setFilter(string filter)
        {
            UrlFilter += filter;
        }
    }
}
