using Core.StorageSevice.PeakVentures.Interface;
using Moq;

namespace Test.StorageService.PeakVentures
{
    public class UserDataStorageServiceTest
    {
        [Theory]
        [InlineData("2022-12-19T14:16:49.9605280Z|https://google.com|SomeUserAgent 1.2.3|192.168.1.1")]
        [InlineData("2022-12-19T14:17:49.9605280Z|https://bing.com|AnotherUserAgent 4.5.6|10.0.0.1")]
        [InlineData("2022-12-19T14:16:49.9605280Z|null|null|8.8.8.8")]
        public void GivenStorageService_WhenUserDataIsStoredSucessfully_ThenReturnsTrue(string userData)
        {
            //Arrange
            Moq.Mock<IUserDataStorageService> mockStorageService = new Moq.Mock<IUserDataStorageService>();
            mockStorageService.Setup(x => x.StoreUserData(It.IsNotNull<string>())).Returns(true);

            IUserDataStorageService userDataStorageService = mockStorageService.Object;

            //Act
            var isStored = userDataStorageService.StoreUserData(userData);

            //Assert
            Assert.True(isStored);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenStorageService_WhenUserDataIsNullOrEmpty_ThenReturnsFalse(string userData)
        {
            //Arrange
            Moq.Mock<IUserDataStorageService> mockStorageService = new Moq.Mock<IUserDataStorageService>();
            mockStorageService.Setup(x => x.StoreUserData("")).Returns(false);
            mockStorageService.Setup(x => x.StoreUserData(null)).Returns(false);

            IUserDataStorageService userDataStorageService = mockStorageService.Object;

            //Act
            var isStored = userDataStorageService.StoreUserData(userData);

            //Assert
            Assert.False(isStored);
        }
    }
}