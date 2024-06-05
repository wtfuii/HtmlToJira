using System.Linq;
using HtmlAgilityPack;

namespace ReverseMarkdown.ConvertersMarkdown
{
    public class Cite : ConverterBase
    {
        public Cite(Converter converter) : base(converter)
        {
            Converter.Register("cite", this);
        }

        public override string Convert(HtmlNode node)
        {
            var content = TreatChildren(node);
            if (string.IsNullOrEmpty(content) || AlreadyCite(node))
            {
                return content;
            }
            
            var spaceSuffix = (node.NextSibling?.Name == "cite")
                ? " "
                : "";

            return content.EmphasizeContentWhitespaceGuard("??", spaceSuffix);
        }

        private static bool AlreadyCite(HtmlNode node)
        {
            return node.Ancestors("cite").Any();
        }
    }
}
