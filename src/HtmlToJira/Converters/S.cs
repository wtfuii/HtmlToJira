﻿using System.Linq;
using HtmlAgilityPack;

namespace HtmlToJira.Converters
{
    public class S : ConverterBase
    {
        public S(Converter converter) : base(converter)
        {
            Converter.Register("s", this);
            Converter.Register("del", this);
            Converter.Register("strike", this);
        }

        public override string Convert(HtmlNode node)
        {
            var content = TreatChildren(node);
            if (string.IsNullOrEmpty(content) || AlreadyStrikethrough(node))
            {
                return content;
            }

            return content.EmphasizeContentWhitespaceGuard("-");
        }

        private static bool AlreadyStrikethrough(HtmlNode node)
        {
            return node.Ancestors("s").Any() || node.Ancestors("del").Any() || node.Ancestors("strike").Any();
        }
    }
}
