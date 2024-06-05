using System.Linq;
using HtmlAgilityPack;

namespace ReverseMarkdown.ConvertersMarkdown
{
    public class U : ConverterBase
    {
        public U(Converter converter) : base(converter)
        {
            var elements = new [] { "u", "ins" };

            foreach (var element in elements)
            {
                Converter.Register(element, this);
            }
            
        }

        public override string Convert(HtmlNode node)
        {
            var content = TreatChildren(node);
            if (string.IsNullOrEmpty(content) || AlreadyCite(node))
            {
                return content;
            }
            
            var spaceSuffix = (node.NextSibling?.Name == "u" || node.NextSibling?.Name == "ins")
                ? " "
                : "";

            return content.EmphasizeContentWhitespaceGuard("??", spaceSuffix);
        }

        private static bool AlreadyCite(HtmlNode node)
        {
            return node.Ancestors("u").Any() || node.Ancestors("ins").Any();
        }
    }
}
