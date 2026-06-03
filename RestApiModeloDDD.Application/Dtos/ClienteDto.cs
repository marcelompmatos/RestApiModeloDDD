using System.Text.Json.Serialization;

namespace RestApiModeloDDD.Application.Dtos
{
    public class ClienteDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
    }
}