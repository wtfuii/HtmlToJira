using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

namespace HtmlToJira.Test
{
    public class JiraConverterTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly VerifySettings _verifySettings;

        public JiraConverterTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _verifySettings = new VerifySettings();
            _verifySettings.DisableRequireUniquePrefix();
        }
        
        static Task CheckConversion(string html, Config config = null)
        {
            config = config ?? new Config();
            var converter = new Converter(config);
            var result = converter.Convert(html);
            var settings = new VerifySettings();
            settings.DisableRequireUniquePrefix();
            return Verifier.Verify(result, settings: settings, extension: "md");
        }
        
        [Fact]
        public Task ConvertBold()
        {
            var html = "<b>Test</b>";
            return CheckConversion(html);
        }
        
        [Fact]
        public Task ConvertParagraph()
        {
            var html = "<p>Test 1</p><p>Test 2</p><p>Test 3</p>";
            return CheckConversion(html);
        }
        
        [Fact]
        public Task ConvertHeadings()
        {
            var html = "<h2>Test H2</h2><h4>Test H4</h4>";
            return CheckConversion(html);
        }
        
        [Fact]
        public Task ConvertTable()
        {
            var html = "<table><thead><tr><th>r1</th><th>r2</th><th>r 3</th></tr></thead><tbody><tr><td>v1</td><td>v2</td><td>v3</td></tr><tr><td>v4</td><td>v5</td><td>v6</td></tr></tbody></table>";
            return CheckConversion(html);
        }
        
        [Fact]
        public Task ConvertTableNoHeader()
        {
            var html = "<table><tbody><tr><td>v1</td><td>v2</td><td>v3</td></tr><tr><td>v4</td><td>v5</td><td>v6</td></tr></tbody></table>";
            return CheckConversion(html);
        }
        
        [Fact]
        public Task ConvertBlockquote()
        {
            var html = "<blockquote>This is the test</blockquote>";
            return CheckConversion(html);
        }
        
        [Fact]
        public Task ConvertUl()
        {
            var html = "<ul><li>Line1</li><li>Line2</li><li>Line3</li></ul>";
            return CheckConversion(html);
        }
    }
}
