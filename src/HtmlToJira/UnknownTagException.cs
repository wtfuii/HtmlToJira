using System;

namespace HtmlToJira
{
    public class UnknownTagException : Exception
    {
        public UnknownTagException(string tagName): base($"Unknown tag: {tagName}")
        {
        }
    }
}
