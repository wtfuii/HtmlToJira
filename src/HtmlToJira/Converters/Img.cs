using HtmlAgilityPack;

namespace HtmlToJira.Converters
{
    public class Img : ConverterBase
    {
        public Img(Converter converter) : base(converter)
        {
            Converter.Register("img", this);
        }

        public override string Convert(HtmlNode node)
        {
            var src = node.GetAttributeValue("src", string.Empty);

            if (!Converter.Config.IsSchemeWhitelisted(StringUtils.GetScheme(src)))
            {
                return "";
            }

            return $"!{src}!";
        }
    }
}
