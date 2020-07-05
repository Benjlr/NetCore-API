
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit;
using ZipProject.Model;

namespace ZipProject.Tests
{
    public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        /// <summary>
        /// Ensures that a user is created properly
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldAddUser()
        {

            UserModel testUser = new UserModel()
            {
                EmailAddress = "addeduser@test.com",
                Salary = 15000,
                Expenses = 2500,
                Name = "Added User",
                
            };

            var convertedUser = JsonConvert.SerializeObject(testUser);
            var stringContent= new StringContent(convertedUser.ToString(), 
                Encoding.UTF8, 
                "application/json");


            var response = await _client.PostAsync("users/createuser", stringContent);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<UserModel>(stringResponse);
            Assert.Equal("addeduser@test.com", users.EmailAddress);
            Assert.Equal("Added User", users.Name);
            Assert.Equal(15000, users.Salary);
            Assert.Equal(2500, users.Expenses);
        }


        /// <summary>
        /// Ensures that a user can create an account
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldCreateAccount()
        {

            var emailAddress = "accountHolder@test.com";

            UserModel accountHolder = new UserModel()
            {
                EmailAddress = emailAddress,
                Salary = 15000,
                Expenses = 2500,
                Name = "Account Holder",

            };

            var convertedUser = JsonConvert.SerializeObject(accountHolder);
            var stringContent = new StringContent(convertedUser.ToString(),
                Encoding.UTF8,
                "application/json");


            var response = await _client.PostAsync("users/createuser", stringContent);
            response.EnsureSuccessStatusCode();

            response = await _client.PostAsync($"accounts/createaccount", stringContent);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<AccountModel>(stringResponse);
            Assert.Equal(emailAddress, account.AccountOwner);
            Assert.Equal(1000, account.Amount);

        }

        /// <summary>
        /// Ensures that user emails that are not unique are rejected
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldNotAddUserWithNonUniqueEmail()
        {

            UserModel dupeNameOne = new UserModel()
            {
                EmailAddress = "duplicate@test.com",
                Salary = 15000,
                Expenses = 2500,
                Name = "duplicate User",

            };

            var convertedUser = JsonConvert.SerializeObject(dupeNameOne);
            var stringContent = new StringContent(convertedUser.ToString(),
                Encoding.UTF8,
                "application/json");


            var response = await _client.PostAsync("users/createuser", stringContent);
            response.EnsureSuccessStatusCode();

            UserModel dupeNameTwo = new UserModel()
            {
                EmailAddress = "duplicate@test.com",
                Salary = 15000,
                Expenses = 2500,
                Name = "duplicate User",

            };

            convertedUser = JsonConvert.SerializeObject(dupeNameTwo);
            stringContent = new StringContent(convertedUser.ToString(),
                Encoding.UTF8,
                "application/json");


            response = await _client.PostAsync("users/createuser", stringContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);


        }


        /// <summary>
        /// Ensures that Users who do not have enough cash cannot create an account
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldNotCreateAccount()
        {
            var emailAddress = "badAcccountHolder@test.com";

            UserModel accountHolder = new UserModel()
            {
                EmailAddress = emailAddress,
                Salary = 2000,
                Expenses = 2500,
                Name = "Nota Holder",

            };

            var convertedUser = JsonConvert.SerializeObject(accountHolder);
            var stringContent = new StringContent(convertedUser.ToString(),
                Encoding.UTF8,
                "application/json");


            var response = await _client.PostAsync("users/createuser", stringContent);
            response.EnsureSuccessStatusCode();

            AccountModel myAccount = new AccountModel()
            {
                AccountOwner = emailAddress,
                Amount = 1000,
            };
            response = await _client.PostAsync("accounts/createaccount", stringContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
