using System.Text.Json.Serialization;

namespace BlackEnd.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoPessoa
    {
        Fisica = 1,
        Juridica = 2
    }
}
