using System;

namespace RestApiModeloDDD.Application.Dtos
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime Expiration { get; set; }
    }
}
