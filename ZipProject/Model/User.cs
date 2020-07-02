using System;
using System.Collections.Generic;

namespace ZipProject.Model
{
    public partial class User
    {
        public User()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int Expenses { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
