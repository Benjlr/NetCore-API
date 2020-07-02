using System;
using System.Collections.Generic;

namespace ZipProject.Model
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }

        public virtual User User { get; set; }
    }
}
