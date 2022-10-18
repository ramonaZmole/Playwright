using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;

namespace PlaywrightMsTest.Pages
{
    public abstract class WebPage<TPage> where TPage : new()
    {
        private static readonly Lazy<TPage> _lazyPage = new(() => new TPage());

        protected readonly IPage _page;

        protected WebPage()
        {
            _page = Browser.GetPage() ?? throw new ArgumentNullException("The wrapped IPages instance is not initialized.");
        }

        public static TPage GetInstance()
        {
            return _lazyPage.Value;
        }



        private ILocator ErrorMessages => _page.Locator(".alert.alert-danger p");

        // private readonly IPage _page;

        // public BasePage(IPage page) => _page = page;


        public async Task<List<string?>> GetErrorMessages() => await ErrorMessages.GetLocatorsText();

        public async Task<bool> IsErrorMessageDisplayed()
        {
            await ErrorMessages.WaitForLocator();
            return await ErrorMessages.First.IsVisibleAsync();
        }
    }
}
