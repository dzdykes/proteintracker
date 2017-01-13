using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ProteinTrackerWebService
{
    [WebService(Namespace = "http://dylandykes.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ProteinTrackingService : WebService
    {
        private UserRepository repository = new UserRepository();

        [WebMethod(Description = "Adds an amount to the total", EnableSession = true)]
        public int AddProtien(int amount, int userId)
        {
            var user = repository.GetById(userId);
            if (user == null) return -1;
            user.Total += amount;
            repository.Save(user);
            return user.Total;
        }

        [WebMethod(EnableSession = true)]
        public int AddUser(string name, int goal)
        {
            var user = new User()
            {
                Goal = goal,
                Name = name,
                Total = 0
            };
            repository.Add(user);

            return user.UserId;
        }

        [WebMethod(EnableSession = true)]
        public List<User> ListUsers()
        {
            return new List<User>(repository.GetAll());
        }
    }
}
