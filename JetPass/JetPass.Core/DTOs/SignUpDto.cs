namespace JetPass.Core.DTOs
{
    public class SignUpDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public bool AgreeToTermsOfService { get; set; }
    }
}