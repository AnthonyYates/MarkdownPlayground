using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using System;
using System.Linq;
using YamlDotNet.Serialization;

namespace FrontMatterParser
{
    /// <summary>
    /// Parses markdown file and returns front matter as strongly typed class.
    /// </summary>
    /// <remarks>Idea from https://khalidabuhakmeh.com/parse-markdown-front-matter-with-csharp </remarks>
    public static class Parser
    {
        private static readonly IDeserializer YamlDeserializer;
        private static readonly MarkdownPipeline Pipeline;

        static Parser()
        {
            YamlDeserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .Build();

            Pipeline = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .Build();
        }

        public static T Parse<T>(this string markdown)
        {
            var document = Markdown.Parse(markdown, Pipeline);
            var block = document
                .Descendants<YamlFrontMatterBlock>()
                .FirstOrDefault();

            if (block == null)
                return default;

            var yaml = block.Lines.ToString();

            return YamlDeserializer.Deserialize<T>(yaml);
        }
    }
}
