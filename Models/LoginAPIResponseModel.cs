using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Models
{
    public class LoginResponse
    {
        public LoginData Data { get; set; }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
    }

    public class LoginData
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public UserInfo User { get; set; }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public RoleInfo Role { get; set; }
        public List<UserRight> Userrights { get; set; }
    }

    public class RoleInfo
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }

    public class UserRight
    {
        public string Right_Id_Pro { get; set; }
        public string Right_Name_Pro { get; set; }
        public string Right_Description { get; set; }
    }

}
