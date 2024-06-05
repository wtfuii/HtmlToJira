using HtmlAgilityPack;

namespace ReverseMarkdown.ConvertersMarkdown
{
    public class PassThrough : ConverterBase
    {
        public PassThrough(Converter converter)
            : base(converter)
        {
        }

        public override string Convert(HtmlNode node)
        {
            return node.OuterHtml;
        }
    }
}
