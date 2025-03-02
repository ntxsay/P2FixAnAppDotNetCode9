using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode9.Models.Services;

namespace P2FixAnAppDotNetCode9.Components
{
    public class LanguageSelectorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ILanguageService languageService)
        {
            return View(languageService);
        }
    }
}
