using Xunit;

namespace Chess.Persistance.Tests
{
    public class UserFixture
    {
        [Fact]
        public void Should_Get_TestUser()
        {
            // act
           var user = User.GetUser("user1");

            // assert
            Assert.Equal("123", user.Password);
        }
    }
}
