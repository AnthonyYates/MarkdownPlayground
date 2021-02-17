using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace MarkdownPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var markdownString =
@"---
title: Article title
so.date: 12.12.2020
so.envir: [onsite, online]
so.product: api-core, api-services, win, web, netserver, mobile
so.area: [configuration, administration, end-user, security, osql, entities, rows, connectors, mirroring, SOAP, REST]
so.category: [contact, person, sale, selection, ticket, oauth, openid connect]
so.topic: concept, how-to, reference, tutorial
---

Here is a brief introduction.

## Sub-heading

Here is some content

### Sub-sub-heading

Here is some sub sub content.
";
            var fm = FrontMatterParser.Parser.Parse<SoFrontMatter>(markdownString);

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }


    public class SoFrontMatter
    {
        [YamlMember(Alias = "so.area")]
        public string[] Area { get; set; }

        [YamlMember(Alias = "so.category")]
        public string[] Category { get; set; }
        
        [YamlMember(Alias = "so.envir")]
        public string[] Environment { get; set; }
        
        [YamlMember(Alias = "so.product")]
        public string Product { get; set; }

        [YamlMember(Alias = "tags")]
        public string Tags { get; set; }

        [YamlMember(Alias = "title")]
        public string Title { get; set; }

        [YamlMember(Alias = "so.date")]
        public DateTime WrittenDate { get; set; }

        [YamlIgnore]
        public IList<string> GetTags => Tags?
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray();
    }
}
