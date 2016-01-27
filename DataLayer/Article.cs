using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace DataLayer
{
    public class Article : Data
    {
        public Article()
        {
            arrayName = "content";
            apiUrl = "/articles";
        }
        public string Name;
        public string Brand;
        public string ThumbImage;
        public string Price;

        public override Data read(JsonObject obj)
        {
            Article article = new Article();
            try
            {
                article.Name = obj.GetNamedString("name");
                article.Brand = obj.GetNamedObject("brand").GetNamedString("name");
                JsonArray units = obj.GetNamedArray("units");
                article.Price = units.GetObjectAt(0).GetNamedObject("price").GetNamedString("formatted");
                JsonArray images = obj.GetNamedObject("media").GetNamedArray("images");
                article.ThumbImage = images.GetObjectAt(0).GetNamedString("smallUrl");
            }
            catch(Exception e)
            {
                //TODO: log exception
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return article;
        }
    }
}
