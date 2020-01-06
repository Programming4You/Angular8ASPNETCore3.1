using System.Collections.Generic;

namespace WebAPI.ViewModel
{
    public class ApplicationUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public List<string> Roles { get; set; }
    }
}
