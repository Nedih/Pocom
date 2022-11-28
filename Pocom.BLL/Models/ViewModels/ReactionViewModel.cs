using Newtonsoft.Json.Converters;
using Pocom.DAL.Enums;
using System.Text.Json.Serialization;

namespace Pocom.BLL.Models.ViewModels
{
    public class ReactionViewModel
    {
        public Guid PostId { get; set; }
        //[JsonConverter(typeof(StringEnumConverter))]
        public ReactionType ReactionType { get; set; }
    }
}
