using System;
using System.Collections.Generic;
using System.Text;
using ZipProject.Model;

namespace ZipProject.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(zip_dbContext dbContext)
        {
            dbContext.User.Add(new User() { Id =1, EmailAddress = "testOne@email.com" , Expenses = 2500, Name = "Test One", Salary = 4500});
            dbContext.User.Add(new User() { Id = 2, EmailAddress = "testTwo@email.com" , Expenses = 3012, Name = "Test Two", Salary = 2869});
            dbContext.User.Add(new User() { Id = 3, EmailAddress = "testThree@email.com" , Expenses = 1256, Name = "Test Three", Salary = 3600});
            dbContext.User.Add(new User() { Id = 4, EmailAddress = "testFour@email.com" , Expenses = 896, Name = "Test Four", Salary = 987});
            dbContext.User.Add(new User() { Id = 5, EmailAddress = "testFive@email.com" , Expenses = 1768, Name = "Test Five", Salary = 6879 });
            dbContext.Account.Add(new Account{ AccountId =1, UserId =1, Amount = 1000 });
            dbContext.Account.Add(new Account{ AccountId =2, UserId =5, Amount = 1000 });
            dbContext.SaveChanges();
        }
    }
}
