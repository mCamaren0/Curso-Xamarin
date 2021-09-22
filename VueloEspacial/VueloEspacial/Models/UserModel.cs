using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace VueloEspacial.Models
{
    public class UserModel : RealmObject
    {
        
        [PrimaryKey]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
        public string Photo { get; set; }
        public bool Remember { get; set; }

        public UserModel()
        {

        }
    }
}
