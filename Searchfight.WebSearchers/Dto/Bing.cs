using System;

namespace Searchfight.WebSearchers.Dto
{
    public class Bing
    {
        public class CroppedRoot
        {
            public WebPages WebPages { get; set; }
        }

        public class Root
        {
            public string Type { get; set; }
            public QueryContext QueryContext { get; set; }
            public WebPages WebPages { get; set; }
            public Entities Entities { get; set; }
            public RelatedSearches RelatedSearches { get; set; }
            public RankingResponse RankingResponse { get; set; }
        }

        public class Entities
        {
            public EntitiesValue[] Value { get; set; }
        }

        public class EntitiesValue
        {
            public Uri Id { get; set; }
            public PurpleContractualRule[] ContractualRules { get; set; }
            public Uri WebSearchUrl { get; set; }
            public string Name { get; set; }
            public Image Image { get; set; }
            public string Description { get; set; }
            public EntityPresentationInfo EntityPresentationInfo { get; set; }
            public Guid BingId { get; set; }
            public Uri Url { get; set; }
        }

        public class PurpleContractualRule
        {
            public string Type { get; set; }
            public string TargetPropertyName { get; set; }
            public bool MustBeCloseToContent { get; set; }
            public License License { get; set; }
            public string LicenseNotice { get; set; }
            public string Text { get; set; }
            public Uri Url { get; set; }
        }

        public class License
        {
            public string Name { get; set; }
            public Uri Url { get; set; }
        }

        public class EntityPresentationInfo
        {
            public string EntityScenario { get; set; }
            public string[] EntityTypeHints { get; set; }
        }

        public class Image
        {
            public string Name { get; set; }
            public Uri ThumbnailUrl { get; set; }
            public Provider[] Provider { get; set; }
            public Uri HostPageUrl { get; set; }
            public long Width { get; set; }
            public long Height { get; set; }
            public long SourceWidth { get; set; }
            public long SourceHeight { get; set; }
        }

        public class Provider
        {
            public string Type { get; set; }
            public Uri Url { get; set; }
        }

        public class QueryContext
        {
            public string OriginalQuery { get; set; }
        }

        public class RankingResponse
        {
            public Mainline Mainline { get; set; }
            public Mainline Sidebar { get; set; }
        }

        public class Mainline
        {
            public Item[] Items { get; set; }
        }

        public class Item
        {
            public string AnswerType { get; set; }
            public long? ResultIndex { get; set; }
            public ItemValue Value { get; set; }
        }

        public class ItemValue
        {
            public Uri Id { get; set; }
        }

        public class RelatedSearches
        {
            public Uri Id { get; set; }
            public RelatedSearchesValue[] Value { get; set; }
        }

        public class RelatedSearchesValue
        {
            public string Text { get; set; }
            public string DisplayText { get; set; }
            public Uri WebSearchUrl { get; set; }
        }

        public class WebPages
        {
            public Uri WebSearchUrl { get; set; }
            public long TotalEstimatedMatches { get; set; }
            public WebPagesValue[] Value { get; set; }
        }

        public class WebPagesValue
        {
            public Uri Id { get; set; }
            public FluffyContractualRule[] ContractualRules { get; set; }
            public string Name { get; set; }
            public Uri Url { get; set; }
            public About[] About { get; set; }
            public bool IsFamilyFriendly { get; set; }
            public Uri DisplayUrl { get; set; }
            public string Snippet { get; set; }
            public DateTimeOffset DateLastCrawled { get; set; }
            public string Language { get; set; }
            public bool IsNavigational { get; set; }
        }

        public class About
        {
            public string Name { get; set; }
        }

        public class FluffyContractualRule
        {
            public string Type { get; set; }
            public string TargetPropertyName { get; set; }
            public long TargetPropertyIndex { get; set; }
            public bool MustBeCloseToContent { get; set; }
            public License License { get; set; }
            public string LicenseNotice { get; set; }
        }
    }
}