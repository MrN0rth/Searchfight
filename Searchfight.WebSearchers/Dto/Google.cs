using System;
using System.Collections.Generic;

namespace Searchfight.WebSearchers.Dto
{
    public class Google
    {
        public class CroppedRoot
        {
            public SearchInformation SearchInformation { get; set; }
        }

        public class Root
        {
            public string Kind { get; set; }
            public Url Url { get; set; }
            public Queries Queries { get; set; }
            public Context Context { get; set; }
            public SearchInformation SearchInformation { get; set; }
            public IList<Item> Items { get; set; }
        }

        public class Url
        {
            public string Type { get; set; }
            public string Template { get; set; }
        }

        public class Request
        {
            public string Title { get; set; }
            public string TotalResults { get; set; }
            public string SearchTerms { get; set; }
            public int Count { get; set; }
            public int StartIndex { get; set; }
            public string InputEncoding { get; set; }
            public string OutputEncoding { get; set; }
            public string Safe { get; set; }
            public string Cx { get; set; }
        }

        public class NextPage
        {
            public string Title { get; set; }
            public string TotalResults { get; set; }
            public string SearchTerms { get; set; }
            public int Count { get; set; }
            public int StartIndex { get; set; }
            public string InputEncoding { get; set; }
            public string OutputEncoding { get; set; }
            public string Safe { get; set; }
            public string Cx { get; set; }
        }

        public class Queries
        {
            public IList<Request> Request { get; set; }
            public IList<NextPage> NextPage { get; set; }
        }

        public class Context
        {
            public string Title { get; set; }
        }

        public class SearchInformation
        {
            public double SearchTime { get; set; }
            public string FormattedSearchTime { get; set; }
            public string TotalResults { get; set; }
            public string FormattedTotalResults { get; set; }
        }

        public class Hcard
        {
            public string Role { get; set; }
            public string Bday { get; set; }
            public string Fn { get; set; }
            public string Nickname { get; set; }
            public string Category { get; set; }
        }

        public class CseThumbnail
        {
            public string Src { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
        }

        public class Person
        {
            public string Role { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
        }

        public class Metatag
        {
            public string Referrer { get; set; }
            public string OgImage { get; set; }
            public string Title { get; set; }
            public string TwitterTitle { get; set; }
            public string OgType { get; set; }
            public DateTime? ArticlePublishedTime { get; set; }
            public string OgImageWidth { get; set; }
            public string OgImageAlt { get; set; }
            public string TwitterCard { get; set; }
            public string OgSiteName { get; set; }
            public string OgTitle { get; set; }
            public string OgImageHeight { get; set; }
            public string ContextlyPage { get; set; }
            public string TwitterCreator { get; set; }
            public string OgDescription { get; set; }
            public string TwitterImage { get; set; }
            public string TwitterTextTitle { get; set; }
            public string TwitterImageAlt { get; set; }
            public string TwitterSite { get; set; }
            public DateTime? ArticleModifiedTime { get; set; }
            public string Viewport { get; set; }
            public string OgLocale { get; set; }
            public string FbAdmins { get; set; }
            public string OgUrl { get; set; }
            public string ThemeColor { get; set; }
            public string AlIosAppName { get; set; }
            public string AlAndroidPackage { get; set; }
            public string AlIosUrl { get; set; }
            public string AlWebUrl { get; set; }
            public string OgVideoSecureUrl { get; set; }
            public string OgVideoTag { get; set; }
            public string OgVideoWidth { get; set; }
            public string AlIosAppStoreId { get; set; }
            public string AlAndroidUrl { get; set; }
            public string OgVideoType { get; set; }
            public string OgVideoHeight { get; set; }
            public string OgVideoUrl { get; set; }
            public string AlAndroidAppName { get; set; }
            public string SkypeToolbar { get; set; }
            public string FormatDetection { get; set; }
            public string ApplicationName { get; set; }
            public string MsapplicationTilecolor { get; set; }
            public string MsapplicationConfig { get; set; }
            public string TwitterUrl { get; set; }
            public string Author { get; set; }
            public string AppleMobileWebAppTitle { get; set; }
            public string FbAppId { get; set; }
            public string TwitterDescription { get; set; }
            public string AppstoreDeveloperUrl { get; set; }
            public string AppstoreBundleId { get; set; }
            public string AppstoreStoreId { get; set; }
            public string AppleMobileWebAppCapable { get; set; }
            public string MobileWebAppCapable { get; set; }
        }

        public class CseImage
        {
            public string Src { get; set; }
        }

        public class Imageobject
        {
            public string Url { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
        }

        public class Organization
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }

        public class Article
        {
            public string Image { get; set; }
            public string Articlebody { get; set; }
            public string Headline { get; set; }
            public string Mainentityofpage { get; set; }
        }

        public class Videoobject
        {
            public string Embedurl { get; set; }
            public string Playertype { get; set; }
            public string Isfamilyfriendly { get; set; }
            public string Uploaddate { get; set; }
            public string Description { get; set; }
            public string Videoid { get; set; }
            public string Url { get; set; }
            public string Duration { get; set; }
            public string Unlisted { get; set; }
            public string Name { get; set; }
            public string Paid { get; set; }
            public string Width { get; set; }
            public string Regionsallowed { get; set; }
            public string Genre { get; set; }
            public string Interactioncount { get; set; }
            public string Channelid { get; set; }
            public string Datepublished { get; set; }
            public string Thumbnailurl { get; set; }
            public string Height { get; set; }
        }

        public class Sitenavigationelement
        {
            public string Url { get; set; }
        }

        public class Offer
        {
            public string Price { get; set; }
            public string Url { get; set; }
        }

        public class Pagemap
        {
            public IList<Hcard> Hcard { get; set; }
            public IList<CseThumbnail> CseThumbnail { get; set; }
            public IList<Person> Person { get; set; }
            public IList<Metatag> Metatags { get; set; }
            public IList<CseImage> CseImage { get; set; }
            public IList<Imageobject> Imageobject { get; set; }
            public IList<Organization> Organization { get; set; }
            public IList<Article> Article { get; set; }
            public IList<Videoobject> Videoobject { get; set; }
            public IList<Sitenavigationelement> Sitenavigationelement { get; set; }
            public IList<Offer> Offer { get; set; }
        }

        public class Item
        {
            public string Kind { get; set; }
            public string Title { get; set; }
            public string HtmlTitle { get; set; }
            public string Link { get; set; }
            public string DisplayLink { get; set; }
            public string Snippet { get; set; }
            public string HtmlSnippet { get; set; }
            public string CacheId { get; set; }
            public string FormattedUrl { get; set; }
            public string HtmlFormattedUrl { get; set; }
            public Pagemap Pagemap { get; set; }
        }

    }
}