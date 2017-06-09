namespace Framework.Core.Net.Http
{
    public class TokenInfo
    {
        public string Token_Type { get; set; }
        public string Access_Token { get; set; }
        public override string ToString()
        {
            return Token_Type + " " + Access_Token;
        }
    }  
   
}
