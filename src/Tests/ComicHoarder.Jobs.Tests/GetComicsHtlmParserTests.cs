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
        private string _GetComicsGodzillaIinfityRoar5 = "";
        private string _GetComicsTheSentry3 = "";

        [TestInitialize]
        public void Init()
        {

            _GetComicsPackPage1 = TestFileLoader.Load(@"2026.05.27 Weekly Pack.html");
            _GetComicsPackPage2 = TestFileLoader.Load(@"2026.06.03 Weekly Pack.html");
            _GetComicsGodzillaIinfityRoar5 = TestFileLoader.Load(@"Godzilla - Infinity Roar 5 (2026).html");
            _GetComicsTheSentry3 = TestFileLoader.Load(@"The Sentry 3 (2026).html");
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

        [TestMethod]
        public void CanParseGodzillaLinkFromPage()
        {
            var link = GetComicsHtmlParser.GetDownloadLinkFromHtml(_GetComicsGodzillaIinfityRoar5);
            Assert.AreEqual(@"https://getcomics.org/dls/+wrPjKFXy1zrl8IsP2X47u9KFWL8SA7NNdZuAenew1JugkapRX+cv004AaUkUGdMFLwtwLSmyz0t99yLN10gKNfwK2Gl7Zmctrqy1uL0w5KohteP+FOFINmYWaGxVmUKLKMdxzXscJzmLqgQ7da4EICWwoeUSFC58lc0HBKy1en7r1CEIokD95L+E2ItdORYVJVqKLSBdRg6ZQl10Jd2KA==:D7kx8RGv5kJg/ztCgoy2pg==", link.Link);
        }

        [TestMethod]
        public void CanParseSentryLinkFromPage()
        {
            var link = GetComicsHtmlParser.GetDownloadLinkFromHtml(_GetComicsTheSentry3);
            Assert.AreEqual(@"https://getcomics.org/dls/7DmBS/Dle/4Ig2TsbcwIblpOR9LZ8N3du/UM0EFX6KI4x+vGBZgWhN1Ywhz6sjeQyUvZF5z7WKg4G1ZSxAPautNx0h2AOgGJJhHTsMqG12ZcwG6+H/0nBI2ZhNJbg4VaqTZPw3XgrBy2YIysE1d5WQ==:oTtcZBL/q9q3dLyj0FNBQw==", link.Link);
        }
    }
}
