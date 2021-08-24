using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Cascabel.Solution.Pages
{
    /// <summary>
    /// This class has all the html elements, you can use xpath, Id, Css Selectors, etc.
    /// </summary>
    public static class ExamplePage
    {
        public static IWebLocator  FirstNameInput =>L("Input field for first name", By.Id("first_11"));
        public static IWebLocator  MiddleNameInput =>L("Input field for middle name", By.Id("middle_11"));
        public static IWebLocator  LastNameInput =>L("Input field for middle name", By.Id("last_11"));
        public static IWebLocator  SubmitButton => L("Form submit button", By.XPath(".//*[@id='input_9']"));
        public static IWebLocator  ConfirmationMessage => L("Successful delivery message", By.CssSelector(".thankyou-main-text"));

        
    }
}
