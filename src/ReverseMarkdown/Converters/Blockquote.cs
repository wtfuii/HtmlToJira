using System;
using HtmlAgilityPack;

namespace ReverseMarkdown.Converters
{
    public class Blockquote : ConverterBase
    {
        public Blockquote(Converter converter) : base(converter)
        {
            Converter.Register("blockquote", this);
        }

        public override string Convert(HtmlNode node)
        {
            var content = TreatChildren(node);

            return $"{Environment.NewLine}{{quote}}{Environment.NewLine}{content}{Environment.NewLine}{{quote}}{Environment.NewLine}";
        }
    }
}
