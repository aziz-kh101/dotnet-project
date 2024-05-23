
namespace MyApp.Namespace
{
    public class LoginModel
    {

        public LoginModel()
        {
        }

        public class InputModel
        {
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
            public bool RememberMe { get; set; } = false;
        }

        public InputModel Input { get; set; } = new();
    }
}
