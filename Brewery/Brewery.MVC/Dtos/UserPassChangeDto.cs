namespace Brewery.MVC.Dtos
{
    public class UserPassChangeDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        
    }
}