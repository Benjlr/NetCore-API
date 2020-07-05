using System;
using System.Collections.Generic;

namespace ZipProject.Model
{
    public partial class UserModel
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int Expenses { get; set; }

        public virtual AccountModel AccountModel { get; set; }
    }
}
