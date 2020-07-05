using ZipProject.Model;

namespace ZipProject.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(zip_dbContext dbContext)
        {
            dbContext.UserModel.Add(new UserModel() { EmailAddress = "testOne@email.com", Expenses = 2500, Name = "Test One", Salary = 4500 });
            dbContext.UserModel.Add(new UserModel() { EmailAddress = "testTwo@email.com", Expenses = 3012, Name = "Test Two", Salary = 2869 });
            dbContext.UserModel.Add(new UserModel() { EmailAddress = "testThree@email.com", Expenses = 1256, Name = "Test Three", Salary = 3600 });
            dbContext.UserModel.Add(new UserModel() { EmailAddress = "testFour@email.com", Expenses = 896, Name = "Test Four", Salary = 987 });
            dbContext.UserModel.Add(new UserModel() { EmailAddress = "testFive@email.com", Expenses = 1768, Name = "Test Five", Salary = 6879 });
            dbContext.AccountModel.Add(new AccountModel { AccountOwner = "testOne@email.com", Amount = 1000 });
            //dbContext.AccountModel.Add(new AccountModel { EmailAddress = "testThree@email.com", Amount = 1000 });
            dbContext.SaveChanges();
        }
    }
}
