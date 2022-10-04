using System.Text.Json.Serialization;

namespace Loja.Src.Utilidades
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Portadores {USER,ADMIN}
}
