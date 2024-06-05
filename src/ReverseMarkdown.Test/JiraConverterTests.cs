using System;
using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

namespace ReverseMarkdown.Test
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
    }
}
