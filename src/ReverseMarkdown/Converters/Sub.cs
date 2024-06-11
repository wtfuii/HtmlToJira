using System.Linq;
using HtmlAgilityPack;

namespace ReverseMarkdown.ConvertersMarkdown
{
    public class Sub : ConverterBase
    {
        public Sub(Converter converter) : base(converter)
        {
            Converter.Register("sub", this);   
        }

        public override string Convert(HtmlNode node)
        {
            var content = TreatChildren(node);
            if (string.IsNullOrEmpty(content) || AlreadySup(node))
            {
                return content;
            }

            return $"~{content.Chomp(all:true)}~";
        }

        private static bool AlreadySup(HtmlNode node)
        {
            return node.Ancestors("sub").Any();
        }
    }
}
