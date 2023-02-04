namespace AuthenticationDuda.Models
{
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    }
}
