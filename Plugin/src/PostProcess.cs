using System.Text;

namespace AEIOU_Company;

static class PostProcess
{
    private static string FilterOutTags(string input, char tagStart, char tagEnd)
    {
        bool inTag = false;
        var stringBuilder = new StringBuilder();
        foreach (var chr in input)
        {
            if (chr == tagStart) { inTag = true; }
            if (!inTag) { stringBuilder.Append(chr); }
            if (chr == tagEnd) { inTag = false; }
        }
        return stringBuilder.ToString();
    }

    public static string FilterOutXml(string input)
        => FilterOutTags(input, '<', '>');

    public static string FilterOutInlineCommands(string input)
        => FilterOutTags(input, '[', ']');
}