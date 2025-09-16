
using System.Text.Json.Serialization;

namespace API.Entities;

public class Photo
{
    public int Id { get; set; }

    public required string Url { get; set; }

    public string? PublicId { get; set; }

    //Navigation back property to the Members
    [JsonIgnore]
    public Member Member { get; set; } = null!;

    public string MemberId { get; set; } = null!; // give us a navigation to the memberId 

}
