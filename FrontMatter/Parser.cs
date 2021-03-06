﻿using Markdig;
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

        /// <summary>
        /// Parse markdown file with front matter.
        /// </summary>
        /// <typeparam name="T">Class with Yaml decorated properties that represent the front matter</typeparam>
        /// <param name="markdown"></param>
        /// <returns>Strongly typed class with front matter contents.</returns>
        /// <example>
        /// Markdown content with front matter:
        /// <code lang="markdown">
        /// ---
        /// title: My title here
        /// desc: My description here
        /// link: http://www.somewhere.com
        /// --
        /// 
        /// ## My header here
        /// 
        /// My paragraph content here...
        /// </code>
        /// 
        /// Yaml front matter class representation:
        /// <code lang="charp">
        /// // represents class decorated with yaml properties
        /// public class FrontMatterClass
        /// {
        ///     [YamlMember(Alias = "title")]
        ///     public string Title { get; set; }
        /// 
        ///     [YamlMember(Alias = "desc")]
        ///     public string Description { get; set; }
        ///     
        ///     [YamlMember(Alias = "link")]
        ///     public string Link { get; set; }
        /// }
        /// </code>
        /// 
        /// Use:
        /// <code lang="csharp">
        /// var markdownString = {Load from example above...}
        /// var fmInstance = FrontMatterParser.Parser.Parse&lt;FrontMatterClass&gt;(markdownString);
        ///
        /// var title = yamlAsClassInstance.Title;
        /// 
        /// Assert.Equals(title, "My title here");
        /// </code>
        /// </example>
        public static T Parse<T>(string markdown)
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
