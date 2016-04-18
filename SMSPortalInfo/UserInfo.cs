using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
	
    public class UserInfo
    {
        public UserInfo()
        {
            Entities = new List<Entity>();
            Entity = new Entity();
        }
        public int User_Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Contact_No_1 { get; set; }

        public string Contact_No_2 { get; set; }

        public string Email_Id { get; set; }

        public int Gender { get; set; }

        public string User_Name { get; set; }

        public string Password { get; set; }

        public int Entity_Id { get; set; }

        public int Role_Id { get; set; }

        public bool Is_Active { get; set; }

        public string Status { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }
        public List<Entity> Entities { get; set; }
        public Entity Entity { get; set; }
    }

    public class Entity
    {
        public int Entity_Id { get; set; }
        public string Entity_Name { get; set; }
    }
}
