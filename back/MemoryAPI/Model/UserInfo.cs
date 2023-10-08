namespace Model
{
    public class UserInfo
    {
        public int id { get; set; }
        public string? username { get; set; }
        public string? password { get; set;}
        public string? phone { get; set; }
        public string? email { get; set; }
    }

    public class UserInfoplus: UserInfo
    {
        public string? newpassword { get; set; }
    }
}