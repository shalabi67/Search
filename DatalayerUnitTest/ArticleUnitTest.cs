using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;

namespace DatalayerUnitTest
{
    [TestClass]
    public class ArticleUnitTest
    {
        [TestMethod]
        public async Task TestGetArticles()
        {
            Article article = new Article();
            Filter filter = new Filter();
            filter.setFilter("brandFamily=nike&color=white&color=red&category=womens-shoes&page=1&pageSize=2");
            Task<List<Article>> task = article.readAPIAsync<Article>(filter);
            List<Article>  list = await task;
            Assert.AreNotEqual(list.Count, 0);
        }
    }
}
