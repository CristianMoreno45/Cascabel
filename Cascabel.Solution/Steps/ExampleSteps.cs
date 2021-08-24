using Boa.Constrictor.WebDriver;
using Cascabel.BaseFramework.Steps;
using static Cascabel.BaseFramework.ReportExtension.StepReport;
using System;
using TechTalk.SpecFlow;
using Cascabel.Solution.Pages;
using FluentAssertions;

namespace Cascabel.Solution.Steps
{
    /// <summary>
    /// All classes of type Sptep, must inherit from BaseStep and using 
    ///     static Cascabel.BaseFramework.ReportExtension.StepReport
    ///     FluentAssertions
    ///     Boa.Constrictor.WebDriver - Reference: https://q2ebanking.github.io/boa-constrictor/getting-started/quickstart/
    ///     TechTalk.SpecFlow
    /// </summary>
    [Binding]
    public class ExampleSteps: BaseStep
    {

        /// <summary>
        /// Mandatory
        /// </summary>
        /// <param name="scenarioContext"></param>
        public ExampleSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {

        }

        [Given(@"The user is in the web form located at (.*)")]
        public void GivenTheUserIsInTheWebFormLocatedAt(string url)
        {
            try
            {
                _actor.AttemptsTo(Navigate.ToUrl(url));
                InsterStepIntoReport(Driver, _scenarioContext);
            }
            catch (Exception ex)
            {
                InsterStepIntoReport(Driver, _scenarioContext, ex: ex);
                throw ex;
            }
        }

        [Given(@"enter first name '(.*)', middle name '(.*)' and last name '(.*)'")]
        public void GivenEnterFirstNameMiddleNameAndLastName(string firstName, string middleName, string lastName)
        {
            try
            {
                _actor.AttemptsTo(SendKeys.To(ExamplePage.FirstNameInput, firstName));
                _actor.AttemptsTo(SendKeys.To(ExamplePage.MiddleNameInput, middleName));
                _actor.AttemptsTo(SendKeys.To(ExamplePage.LastNameInput, lastName));
                InsterStepIntoReport(Driver, _scenarioContext);
            }
            catch (Exception ex)
            {
                InsterStepIntoReport(Driver, _scenarioContext, ex: ex);
                throw ex;
            }
        }

        [When(@"he click on the send option")]
        public void WhenHeClickOnTheSendOption()
        {
            try
            {
                //_actor.AttemptsTo(Click.On(ExamplePage.SubmitButton));
                _actor.AttemptsTo(JavaScriptClick.On(ExamplePage.SubmitButton));
                InsterStepIntoReport(Driver, _scenarioContext);
            }
            catch (Exception ex)
            {
                InsterStepIntoReport(Driver, _scenarioContext, ex: ex);
                throw ex;
            }
        }

        [Then(@"the user will see the welcome message '(.*)'")]
        public void ThenTheUserWillSeeTheWelcomeMessage(string message)
        {
            try
            {
                _actor.AsksFor(Text.Of(ExamplePage.ConfirmationMessage)).Should().Contain(message);
                InsterStepIntoReport(Driver, _scenarioContext);
            }
            catch (Exception ex)
            {
                InsterStepIntoReport(Driver, _scenarioContext, ex: ex);
                throw ex;
            }
        }

        [Then(@"the web browser will be closed")]
        public void ThenTheWebBrowserWillBeClosed()
        {
            try
            {
                _actor.AttemptsTo(QuitWebDriver.ForBrowser());
                InsterStepIntoReport(Driver, _scenarioContext, takePhoto: false);
            }
            catch (Exception ex)
            {
                InsterStepIntoReport(Driver, _scenarioContext, takePhoto: false, ex: ex);
                throw ex;
            }
        }
    }
}
