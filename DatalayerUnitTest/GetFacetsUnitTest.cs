using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;

namespace DatalayerUnitTest
{
    [TestClass]
    public class GetFacetsUnitTest
    {
        [TestMethod]
        public async Task TestGetFacets()
        {
            List<Facet> list = await FacetGroup.getFacets("sho", Gender.female);
            Assert.AreNotEqual(list.Count, 0);
        }
    }
}
