namespace MenuMaster.Dtos
{
    public class ResetPasswordDto
    {
        public string Code { get; set; } // 6-digit code
        public string NewPassword { get; set; }
        public string Email { get; set; }
    }
}
