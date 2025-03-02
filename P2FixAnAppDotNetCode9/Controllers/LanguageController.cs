﻿using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode9.Models.Services;
using P2FixAnAppDotNetCode9.Models.ViewModels;

namespace P2FixAnAppDotNetCode9.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeUiLanguage(LanguageViewModel model, string returnUrl)
        {
            if (!string.IsNullOrEmpty(model.Language) && !string.IsNullOrWhiteSpace(model.Language))
            {
                _languageService.ChangeUiLanguage(HttpContext, model.Language);
            }

            return Redirect(returnUrl);
        }
    }
}
