using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace HtmlToJira.Converters
{
    public class Table : ConverterBase
    {
        public Table(Converter converter) : base(converter)
        {
            Converter.Register("table", this);
        }

        public override string Convert(HtmlNode node)
        {

            return $"{Environment.NewLine}{Environment.NewLine}{TreatChildren(node)}{Environment.NewLine}";
        }

        private static bool HasNoTableHeaderRow(HtmlNode node)
        {
            var thNode = node.SelectNodes("//th")?.FirstOrDefault();
            return thNode == null;
        }

        private static string EmptyHeader(HtmlNode node)
        {
            var firstRow = node.SelectNodes("//tr")?.FirstOrDefault();

            if (firstRow == null)
            {
                return string.Empty;
            }

            var colCount = firstRow.ChildNodes.Count(n => n.Name.Contains("td"));

            var headerRowItems = new List<string>();
            var underlineRowItems = new List<string>();

            for (var i = 0; i < colCount; i++ )
            {
                headerRowItems.Add("<!---->");
                underlineRowItems.Add("---");
            }

            var headerRow = $"| {string.Join(" | ", headerRowItems)} |{Environment.NewLine}";
            var underlineRow = $"| {string.Join(" | ", underlineRowItems)} |{Environment.NewLine}";

            return headerRow + underlineRow;
        }
    }
}
