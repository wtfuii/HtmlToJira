using HtmlAgilityPack;

namespace ReverseMarkdown.ConvertersMarkdown
{
    public interface IConverter
    {
        string Convert(HtmlNode node);
    }
}
