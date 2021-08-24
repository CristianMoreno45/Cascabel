using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Cascabel.BaseFramework.ReportExtension;
using System;
using TechTalk.SpecFlow;
using static Cascabel.BaseFramework.ReportExtension.StepReport;

namespace Cascabel.BaseFramework.Steps
{
    public class BaseStep: SpecFlowSteps
    {
        protected static Actor _actor;
        protected static bool _enabledS3;

        public BaseStep(ScenarioContext scenarioContext) : base(scenarioContext)
        {

        }

        [BeforeScenario(Order = 0)]
        public void RegisterDI()
        {
            //_scenarioContext.ScenarioContainer.RegisterTypeAs<DatabaseContext, IDatabaseContext>();
            if (Driver == null)
            {
                InitializeDriver();
                _actor = new Actor("Anonymous", new ConsoleLogger());
                _actor.Can(BrowseTheWeb.With(Driver));
                //_scenarioContext.ScenarioContainer.RegisterInstanceAs(actor);
            }
        }

        [AfterScenario]
        public void FinalizeThisScenario()
        {
            try
            {
                _actor.AttemptsTo(QuitWebDriver.ForBrowser());
                ClosePage();
            }
            catch (Exception ex)
            {
                InsterStepIntoReport(Driver, _scenarioContext, ex: ex);
                throw ex;
            }
        }
    }
}
