
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

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
            //var response = await _client.PostAsync("")


        }


        /// <summary>
        /// Ensures that user emails that are not unique are rejected
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldNotAddUserWithNonUniqueEmail()
        {

        }


        /// <summary>
        /// Ensures that a user can create an account
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldCreateAccount()
        {

        }

        /// <summary>
        /// Ensures that Users who do not have enough cash cannot create an account
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldNotCreateAccount()
        {

        }

        [Fact]
        public async Task Test5()
        {

        }
    }
}
