using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Searchfight.Common;
using Searchfight.Domain;
using Xunit;

namespace Searchfight.Tests
{
    public class SearchfightJudgeTests
    {
        private readonly ISearchfightJudge _searchfightJudge;
        private readonly IResultSearcher _searcher1;
        private readonly IResultSearcher _searcher2;

        public SearchfightJudgeTests()
        {
            Substitute.For<ISearchfightJudge>();
            _searcher1 = Substitute.For<IResultSearcher>();
            _searcher2 = Substitute.For<IResultSearcher>();
            _searchfightJudge = new SearchfightJudge(new[] { _searcher1, _searcher2 });
        }

        [Fact]
        public async Task TestResults_GeneralCase()
        {
            string[] topics = { ".net", "java" };
            var returnedFrom1 = new Dictionary<string, Result<long>>
            {
                { ".net", new Result<long>(5) },
                { "java", new Result<long>(3) },
            };

            _searcher1.Name.Returns("Searcher 1");
            _searcher1.GetNumberOfResults(topics).Returns(returnedFrom1);

            var returnedFrom2 = new Dictionary<string, Result<long>>
            {
                { ".net", new Result<long>(1) },
                { "java", new Result<long>(2) },
            };

            _searcher2.Name.Returns("Searcher 2");
            _searcher2.GetNumberOfResults(topics).Returns(returnedFrom2);

            var result = await _searchfightJudge.GetResults(topics);

            Assert.Equal(2, result.GeneralResults.Count());

            Assert.Equal(1, result.TotalWinners.Count());

            Assert.Equal(".net", result.TotalWinners.First().Topic);
            Assert.Equal(6, result.TotalWinners.First().NumberOfResults);

            Assert.Equal(5, result.SearcherWinners.First(w => w.Name == "Searcher 1").NumberOfResults);
            Assert.Equal(".net", result.SearcherWinners.First(w => w.Name == "Searcher 1").Topic);

            Assert.Equal(2, result.SearcherWinners.First(w => w.Name == "Searcher 2").NumberOfResults);
            Assert.Equal("java", result.SearcherWinners.First(w => w.Name == "Searcher 2").Topic);
        }

        [Fact]
        public async Task TestResults_FailedSearches()
        {
            string[] topics = { ".net", "java" };
            var returnedFrom1 = new Dictionary<string, Result<long>>
            {
                { ".net", new Result<long>("Unable to retrieve data") },
                { "java", new Result<long>(3) },
            };

            _searcher1.Name.Returns("Searcher 1");
            _searcher1.GetNumberOfResults(topics).Returns(returnedFrom1);

            var returnedFrom2 = new Dictionary<string, Result<long>>
            {
                { ".net", new Result<long>(1) },
                { "java", new Result<long>(2) },
            };

            _searcher2.Name.Returns("Searcher 2");
            _searcher2.GetNumberOfResults(topics).Returns(returnedFrom2);

            var result = await _searchfightJudge.GetResults(topics);

            Assert.Equal(2, result.GeneralResults.Count());

            Assert.Equal(1, result.TotalWinners.Count());

            Assert.Equal("java", result.TotalWinners.First().Topic);
            Assert.Equal(5, result.TotalWinners.First().NumberOfResults);

            Assert.Equal(3, result.SearcherWinners.First(w => w.Name == "Searcher 1").NumberOfResults);
            Assert.Equal("java", result.SearcherWinners.First(w => w.Name == "Searcher 1").Topic);

            Assert.Equal(2, result.SearcherWinners.First(w => w.Name == "Searcher 2").NumberOfResults);
            Assert.Equal("java", result.SearcherWinners.First(w => w.Name == "Searcher 2").Topic);
        }

        [Fact]
        public async Task TestResults_TwoTotalWinners()
        {
            string[] topics = { ".net", "java" };
            var returnedFrom1 = new Dictionary<string, Result<long>>
            {
                { ".net", new Result<long>(2) },
                { "java", new Result<long>(1) },
            };

            _searcher1.Name.Returns("Searcher 1");
            _searcher1.GetNumberOfResults(topics).Returns(returnedFrom1);

            var returnedFrom2 = new Dictionary<string, Result<long>>
            {
                { ".net", new Result<long>(1) },
                { "java", new Result<long>(2) },
            };

            _searcher2.Name.Returns("Searcher 2");
            _searcher2.GetNumberOfResults(topics).Returns(returnedFrom2);

            var result = await _searchfightJudge.GetResults(topics);

            Assert.Equal(2, result.TotalWinners.Count());
        }

        [Fact]
        public async Task TestResults_TwoSearcherWinners()
        {
            string[] topics = { ".net", "java" };
            var returnedFrom1 = new Dictionary<string, Result<long>>
            {
                { ".net", new Result<long>(4) },
                { "java", new Result<long>(4) },
            };

            _searcher1.Name.Returns("Searcher 1");
            _searcher1.GetNumberOfResults(topics).Returns(returnedFrom1);

            var returnedFrom2 = new Dictionary<string, Result<long>>
            {
                { ".net", new Result<long>(1) },
                { "java", new Result<long>(2) },
            };

            _searcher2.Name.Returns("Searcher 2");
            _searcher2.GetNumberOfResults(topics).Returns(returnedFrom2);

            var result = await _searchfightJudge.GetResults(topics);

            Assert.Equal(1, result.TotalWinners.Count());

            Assert.Equal(2, result.SearcherWinners.Count(w => w.Name == "Searcher 1"));
        }
    }
}