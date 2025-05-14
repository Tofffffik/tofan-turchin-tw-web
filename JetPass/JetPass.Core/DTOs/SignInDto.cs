namespace JetPass.Core.DTOs
{
    public class SignInDto
    {
        public string EmailOrPhoneNumber { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}