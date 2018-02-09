using Septa.PardakhtVaset.Client.Internals;
using System;
using Xunit;

namespace PardakhtVasetClientTests
{
    public class ObjectParserTests
    {
        [Fact]
        public void ParseArgs_WithNullArgs_ReturnsEmptyDictionary()
        {
            var parser = new ObjectParser();
            var result = parser.ParseArgs(null);

            Assert.Empty(result);
        }

        [Fact]
        public void ParseArgs_WithEmptyArgs_ReturnsEmptyDictionary()
        {
            var parser = new ObjectParser();
            var result = parser.ParseArgs(new { });

            Assert.Empty(result);
        }

        [Fact]
        public void ParseArgs_WithAnonymousArg_ReturnsExpectedDictionary()
        {
            var args = new { Number = 1, Text = "Hello", CreateDate = new DateTime(2018, 1, 1, 1, 1, 1), Mark = 2.0 };

            var parser = new ObjectParser();
            var result = parser.ParseArgs(args);

            Assert.Equal(4, result.Count);
            Assert.Equal(1, result[nameof(args.Number)]);
            Assert.Equal("Hello", result[nameof(args.Text)]);
            Assert.Equal(new DateTime(2018, 1, 1, 1, 1, 1), result[nameof(args.CreateDate)]);
            Assert.Equal(2.0, result[nameof(args.Mark)]);
        }
    }
}
