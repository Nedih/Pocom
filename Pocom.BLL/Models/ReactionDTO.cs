using Newtonsoft.Json.Converters;
using Pocom.DAL.Enums;
using System.Text.Json.Serialization;

namespace Pocom.BLL.Models
{
    public class ReactionDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PostId { get; set; }
        public string? AuthorId { get; set; }
        //[JsonConverter(typeof(StringEnumConverter))]
        public ReactionType ReactionType { get; set; }
        //public UserAccount Author { get; set; }
        //public Post Post { get; set; }
    }
}
