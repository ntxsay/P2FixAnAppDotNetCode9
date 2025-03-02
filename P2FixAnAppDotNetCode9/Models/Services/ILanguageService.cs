namespace P2FixAnAppDotNetCode9.Models.Services
{
    public interface ILanguageService
    {
        void ChangeUiLanguage(HttpContext context, string language);
        string SetCulture(string language);
        void UpdateCultureCookie(HttpContext context, string culture);
    }
}
