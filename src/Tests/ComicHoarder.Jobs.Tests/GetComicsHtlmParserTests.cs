using GetComicsDownloader.Services;
using GetComicsDownloader.Models;
using ComicHoarder.Jobs.Tests.TestUtilities;

namespace ComicHoarder.Jobs.Tests
{
    [TestClass]
    public sealed class GetComicsHtlmParserTests
    {
        private string _GetComicsPackPage1 = "";
        private string _GetComicsPackPage2 = "";

        [TestInitialize]
        public void Init()
        {

            _GetComicsPackPage1 = TestFileLoader.Load("2026.05.27 Weekly Pack.html");
            _GetComicsPackPage2 = TestFileLoader.Load("2026.06.03 Weekly Pack.html");
        }

        [TestMethod]
        public void CanParseCorrectNumberOfLinksFromGetComics_1()
        {
            var links = GetComicsHtmlParser.GetPageUrlsFromHtml(_GetComicsPackPage1);
            Assert.AreEqual(12, links.Count());
        }

        [TestMethod]
        public void CanParseCorrectLinksFromGetComics_1()
        {
            var links = GetComicsHtmlParser.GetPageUrlsFromHtml(_GetComicsPackPage1);
            Assert.IsTrue(
                links.Contains("https://getcomics.org/marvel/the-sentry-3-2026/"),
                "Expected link was not found in parsed results.");
        }

        [TestMethod]
        public void CanParseCorrectNumberOfLinksFromGetComics_2()
        {
            var links = GetComicsHtmlParser.GetPageUrlsFromHtml(_GetComicsPackPage2);
            Assert.AreEqual(16, links.Count());
        }

        [TestMethod]
        public void CanParseCorrectLinksFromGetComics_2()
        {
            var links = GetComicsHtmlParser.GetPageUrlsFromHtml(_GetComicsPackPage2);
            Assert.IsTrue(
                links.Contains("https://getcomics.org/marvel/godzilla-infinity-roar-5-2026/"),
                "Expected link was not found in parsed results.");
        }
    }
}
