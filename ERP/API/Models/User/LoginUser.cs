using API.Models.Token;


namespace API.Models.User
{
    public class LoginUser
    {
        

        public string user_uuid { get; set; }
      
        public TokenModel token { get; set; }
    }
}
