using Microsoft.Playwright;

namespace PlaywrightMsTest.Helpers
{
    internal static class PageHelpers
    {
        public static async Task<List<ILocator>> GetElements(this ILocator locator)
        {
            var elements = new List<ILocator>();
            for (var i = 0; i < await locator.CountAsync(); i++)
            {
                elements.Add(locator.Nth(i));
            }

            return elements;
        }

        public static async Task Click(this ILocator locator)
        {
            await locator.WaitForAsync();
            await locator.ClickAsync();
        }

        public static async Task<bool> IsVisible(this ILocator locator)
        {
            await locator.WaitForLocator();
            return await locator.IsVisibleAsync();
        }

        public static async Task<List<string?>> GetLocatorsText(this ILocator locator)
        {
            await locator.WaitForLocator();
            var list = new List<string?>();

            for (var i = 0; i < await locator.CountAsync(); i++)
            {
                list.Add(await locator.Nth(i).TextContentAsync());
            }

            return list;
        }

        public static async Task WaitForLocator(this ILocator locator, WaitForSelectorState state = WaitForSelectorState.Attached)
        {
            await locator.Last.WaitForAsync(new LocatorWaitForOptions
            {
                Timeout = 200,
                State = state
            });
        }
    }
}
