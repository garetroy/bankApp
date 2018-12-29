namespace bankLedger.Web.Dtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string DecryptedPassword { get; set; }
    }
}
