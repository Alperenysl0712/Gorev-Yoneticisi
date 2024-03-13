namespace Görev_Yöneticisi.Models
{
    public class UserTokenInfo
    {
        public int userId { get; set; }

        public string? Username { get; set; }

        public string? Token { get; set; }

        public DateTime? loginDate { get; set; }
    }
}
