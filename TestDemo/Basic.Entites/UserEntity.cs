using Basic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basic.Entites
{
    [SoftDelete]
    public class UserEntity:BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdCar { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        
    }
}
