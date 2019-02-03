using Newtonsoft.Json;

namespace Tsumugi
{
    public class Field
    {        
        [JsonProperty("title")]
        public string Title;

        [JsonProperty("value")]
        public string Value;

        [JsonProperty("short")]
        public bool IsShort;

        public Field()
        {            
        }

        public Field(string title, string value, bool isShort = false)
        {
            IsShort = isShort;
            Title = title;
            Value = value;
        }
    }
}
