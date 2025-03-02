using P2FixAnAppDotNetCode9.Models.Services;

namespace P2FixAnAppDotNetCode9.Tests
{
    /// <summary>
    /// The LanguageService class
    /// </summary>
    public class LanguageServiceTests
    {
        [Fact]
        public void SetCulture()
        {
            // Arrange
            ILanguageService languageService = new LanguageService();
            string language = "French";

            // Act
            string culture = languageService.SetCulture(language);

            // Assert
            Assert.Same("fr", culture);
        }
    }
}
