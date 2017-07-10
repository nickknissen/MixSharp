using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace MixSharp 
{
    [TestClass]
    public class MixSharpTest
    {
        [TestMethod]
        [ExpectedException(typeof(ConfigurationMissingException), "A userId of null was inappropriately allowed.")]
        public void TestPublicPathMissingConfig()
        {
            ConfigurationManager.AppSettings["publicPath"] = null;
            var path = MixSharp.PublicPath("");
        }

        [TestMethod]
        public void TestPublicPath1()
        {
            ConfigurationManager.AppSettings["publicPath"] = "/public";
            var path = MixSharp.PublicPath("/mix-manifest.json");

            Assert.AreEqual("/public/mix-manifest.json", path);
        }

        [TestMethod]
        public void TestPublicPath2()
        {
            ConfigurationManager.AppSettings["publicPath"] = "public";
            var path = MixSharp.PublicPath("");
            Assert.AreEqual(AppDomain.CurrentDomain.BaseDirectory + "public", path);
        }

        [TestMethod]
        public void TestMix()
        {
            ConfigurationManager.AppSettings["publicPath"] = @"\stubs\public-mix-dev";

            var path = MixSharp.Mix("css/app.css");
            Assert.AreEqual("/css/app.css", path);
        }
    }
}
