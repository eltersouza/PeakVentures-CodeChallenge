using Core.PixelService.PeakVentures.Interfaces;
using Microsoft.AspNetCore.Http;
using Core.PixelService.PeakVentures.Models;
using Moq;

namespace Test.PixelService.PeakVenture
{
    public class UserDataPublisherServiceTest
    {
        [Fact]
        public void GivenRequest_WhenUserDataIsComplete_ThenPublishReturnsTrue()
        {
            //Arrange
            Moq.Mock<IUserDataPublisherService> mock = new Moq.Mock<IUserDataPublisherService>();
            mock.Setup(x => x.PublishUserData(It.Is<UserData>(x => !string.IsNullOrEmpty(x.IpAddress)) , It.IsAny<KafkaConfiguration>())).Returns(true);

            IUserDataPublisherService userDataPublisherService = mock.Object;
            var userData = new UserData("127.0.0.1", null, null);

            //Act
            bool isPublished = userDataPublisherService.PublishUserData(userData, new KafkaConfiguration());

            //Assert
            Assert.True(isPublished);
        }

        [Fact]
        public void GivenRequest_WhenUserDataIsIncomplete_ThenPublishReturnsFalse()
        {
            //Arrange
            Moq.Mock<IUserDataPublisherService> mock = new Moq.Mock<IUserDataPublisherService>();
            mock.Setup(x => x.PublishUserData(It.Is<UserData>(x => string.IsNullOrEmpty(x.IpAddress)), It.IsAny<KafkaConfiguration>())).Returns(false);

            IUserDataPublisherService userDataPublisherService = mock.Object;
            var userData = new UserData("", null, null);

            //Act
            bool isPublished = userDataPublisherService.PublishUserData(userData, new KafkaConfiguration());

            //Assert
            Assert.False(isPublished);
        }
    }
}
