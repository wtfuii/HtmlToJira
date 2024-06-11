using System;
using System.Linq;
using HtmlAgilityPack;

namespace HtmlToJira.Converters
{
    public class Li : ConverterBase
    {
        public Li(Converter converter) : base(converter)
        {
            Converter.Register("li", this);
        }

        public override string Convert(HtmlNode node)
        {
            // Standardize whitespace before inner lists so that the following are equivalent
            //   <li>Foo<ul><li>...
            //   <li>Foo\n    <ul><li>...
            foreach (var innerList in node.SelectNodes("//ul|//ol") ?? Enumerable.Empty<HtmlNode>())
            {
                if (innerList.PreviousSibling?.NodeType == HtmlNodeType.Text)
                {
                    innerList.PreviousSibling.InnerHtml = innerList.PreviousSibling.InnerHtml.Chomp();
                }
            }

            var content = TreatChildren(node);
            var indentationLevel = IndentationLevelFor(node, false);
            var prefix = PrefixFor(node);

            return $"{new string(prefix, indentationLevel)} {content.Chomp()}{Environment.NewLine}";
        }

        private char PrefixFor(HtmlNode node)
        {
            if (node.ParentNode != null && node.ParentNode.Name.ToLower() == "ol")
            {
                // index are zero based hence add one
                return '#';
            }
            else
            {
                return '*';
            }
        }
    }
}
