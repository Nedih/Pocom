using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Pocom.DAL.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReactionType
    {
        None,
        Like,  
        Fire,
        Dislike
    }
}
