using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WatchBook.Domain.Enums.Discovery;

/// <summary>
/// Represents the media type returned by the TMDb trending endpoint.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TrendingMediaType
{
    [EnumMember(Value = "movie")]
    Movie,

    [EnumMember(Value = "tv")]
    Tv,

    [EnumMember(Value = "person")]
    Person
}