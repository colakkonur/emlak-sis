﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emlakkkk.Models.Giris
{
    public class LoginState
    {
        public LoginState()
        {
            emlksisEntities db = new emlksisEntities();
        }
        public bool IsLoginSucces(string user, string pass)
        {
            emlksisEntities db = new emlksisEntities();
            users resultUser = db.users.Where(x => x.mail.Trim().ToString().Equals(user) && x.password.Equals(pass)).FirstOrDefault();
            if (resultUser != null)
            {
                HttpContext.Current.Session.Add("userid", resultUser.userId.ToString());
                HttpContext.Current.Session.Add("kulEposta", resultUser.mail.ToString());
                if (resultUser.avatarSource != null)
                {
                    HttpContext.Current.Session.Add("avatarim", resultUser.avatarSource.ToString());
                }
                HttpContext.Current.Session.Add("kulAdSoyad", resultUser.name.ToString()+" "+resultUser.surname.ToString());
                return true;
            }
            return new bool();

        }
    }
}