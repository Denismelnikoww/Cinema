namespace Pet.Contracts
{
    public record LoginRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}


