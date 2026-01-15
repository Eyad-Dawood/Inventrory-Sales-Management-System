using DataAccessLayer.Entities;
using LogicLayer.DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Global.Users
{
    public class UserSession
    {
        public UserReadDto? CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;

        public void Login(UserReadDto user)
        {
            CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
