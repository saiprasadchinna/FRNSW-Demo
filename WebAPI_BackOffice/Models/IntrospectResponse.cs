namespace WebAPI_BackOffice.Models
{
    //{"active":true,"scope":"openid profile offline_access","username":"triambhakeshwar1994@gmail.com","exp":1686064913,"iat":1686061313,"sub":"triambhakeshwar1994@gmail.com","aud":"https://dev-17683470.okta.com","iss":"https://dev-17683470.okta.com","jti":"AT.ZVX6Orc6fp-V3t_U7JfyQt5KwZFfnoRfO6Uqb2IALao.oar13u43g7r8GRil85d7","token_type":"Bearer","client_id":"0oa912ox83mA6vxCh5d7","uid":"00u9ujga53476CyDB5d7"}
    public class IntrospectResponse
    {
        public bool active { get; set; }
        public string scope { get; set; }
        public string username { get; set; }
        public int exp { get; set; }
        public int iat { get; set; }
        public string sub { get; set; }
        public string aud { get; set; }
        public string iss { get; set; }
        public string jti { get; set; }
        public string token_type { get; set; }
        public string client_id { get; set; }
        public string uid { get; set; }
    }
}
