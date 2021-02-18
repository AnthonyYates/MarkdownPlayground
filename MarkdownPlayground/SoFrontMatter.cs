using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace MarkdownPlayground
{
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
