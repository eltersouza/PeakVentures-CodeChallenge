namespace Test.PixelService.PeakVenture
{
    public class ImageGeneratorServiceTest
    {
        [Fact]
        public void GivenImageGenerator_WhenReceivesRequest_ThenGenerateGif()
        {
            //Arrange
            var imageGeneratorService = new ImageGeneratorService();

            //Act
            var result = imageGeneratorService.GenerateGif();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }
    }
}