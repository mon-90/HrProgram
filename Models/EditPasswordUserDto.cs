namespace HrProgram.Models
{
    public class EditPasswordUserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
