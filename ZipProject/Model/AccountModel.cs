using System;
using System.Collections.Generic;

namespace ZipProject.Model
{
    public partial class AccountModel
    {
        public string AccountOwner { get; set; }
        public int Amount { get; set; }

        public virtual UserModel UserModel { get; set; }
    }
}
