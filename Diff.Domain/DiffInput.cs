using Newtonsoft.Json;

namespace Diff.Domain;

public class DiffInput
{
    [JsonProperty("input")]
    public string Input { get; set; }
}
