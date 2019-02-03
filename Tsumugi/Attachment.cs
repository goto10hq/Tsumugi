using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tsumugi
{
    public class Attachment
    {
        [JsonProperty("author_icon")]
        public string AuthorIcon;

        [JsonProperty("author_link")]
        public string AuthorLink;

        [JsonProperty("author_name")]
        public string AuthorName;

        [JsonProperty("color")]
        public string Color;

        [JsonProperty("fields")]
        public IEnumerable<Field> Fields;

        [JsonProperty("image_url")]
        public string ImageUrl;

        [JsonProperty("thumb_url")]
        public string ThumbUrl;

        [JsonProperty("pretext")]
        public string Pretext;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("title_link")]
        public string TitleLink;        
    }
}
