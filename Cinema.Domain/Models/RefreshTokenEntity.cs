namespace Cinema.Domain.Models
{
    public class RefreshTokenEntity
    {
        public int Id { get; set; }
        public string Token { get; set; }   
        public int UserId { get; set; }
        public string Jti { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiryAt { get; set; }
    }
}
