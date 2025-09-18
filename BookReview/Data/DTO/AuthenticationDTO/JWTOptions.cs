namespace BookReview.Data.DTO.AuthenticationDTO
{
    public class JWTOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationDays { get; set; }
    }
}
