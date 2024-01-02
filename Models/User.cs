namespace SimpleJWT
{
    public class User
    {

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Gender{ get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        // public User(string username, string password)
        // {
        //     UserName = username;
        //     Password = password;
        // }
    }
}
