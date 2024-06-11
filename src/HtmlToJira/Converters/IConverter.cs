using HtmlAgilityPack;

namespace HtmlToJira.Converters
{
    public interface IConverter
    {
        string Convert(HtmlNode node);
    }
}
