using AventStack.ExtentReports.Gherkin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using static Cascabel.BaseFramework.ReportExtension.StepReport;
using static Cascabel.BaseFramework.ReportExtension.Logger;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Logging;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium.Chrome;
using System.Configuration;

namespace Cascabel.BaseFramework.ReportExtension
{
    /// <summary>
    /// Base class from where all the automation steps are inherited, mainly the actions that are required to be done at certain moments of the test life cycle are configured, then they are listed according to their hierarchy
    ///     BeforeTestRun=>
    ///         BeforeFeature=>
    ///             BeforeScenario=>
    ///                 BeforeScenarioBlock=>
    ///                     BeforeStep=>
    ///                     AfterStep=>
    ///                 AfterScenarioBlock=>
    ///             AfterScenario=>
    ///         AfterFeature=>
    ///     AfterTestRun=>
    /// </summary>
    public class SpecFlowSteps : WebDriverManager
    {
        /// <summary>
        /// Setting context
        /// </summary>
        protected readonly ScenarioContext _scenarioContext;
        protected static ScenarioContext _currentScenarioContext;
        protected static FeatureContext _currentFeatureContext;

        public SpecFlowSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Event that happens before the test starts
        /// </summary>
        [BeforeTestRun]
        public static void InitializeTest()
        {
            Log("InitializeTest");
        }

        /// <summary>
        /// Event that happens before the feature starts to evaluate
        /// </summary>
        /// <param name="featureContext">Current feature</param>
        [BeforeFeature]
        public static void InitializeFeature(FeatureContext featureContext)
        {
            string featureTitle = featureContext.FeatureInfo.Title;
            Log("InitializeFeature: {0}", featureTitle);

            // Create a feature within the test scenario
            if (featureContext != _currentFeatureContext)
            {
                CreateFeature(featureTitle);
                UpdateCurrentFeatureContext(featureContext);
            }

        }

        /// <summary>
        /// Event that happens before you start evaluating the Scenario
        /// </summary>
        [BeforeScenario]
        public void InitializeScenario()
        {
            if (!HaveCurrentReport()) InitializeReport(BaseDirectory);
            // Create test scenario
            if (_scenarioContext != _currentScenarioContext)
            {
                string scenarioTitle = _scenarioContext.ScenarioInfo.Title;
                Log("InitializeScenario: {0}", scenarioTitle);
                CreateScenario(scenarioTitle);
                UpdateCurrentScenarioContext(_scenarioContext);
            }
        }



        /// <summary>
        /// Event that happens before you start evaluating a Given, When, or Then block
        /// </summary>
        [BeforeScenarioBlock]
        public void InitializeBlock()
        {
            Log("InitializeBlock (Given, When or Then)");
        }

        /// <summary>
        /// Event that happens before you start evaluating a step
        /// </summary>
        [BeforeStep]
        public void InitializeStep()
        {
            Log("InitializeStep: {0}", _scenarioContext.StepContext.StepInfo.Text);
        }

        /// <summary>
        /// Event that happens after you finish evaluating the step
        /// </summary>
        [AfterStep]
        public void FinalizeStep()
        {
            // Log("Después del paso: {0}", _scenarioContext.StepContext.StepInfo.Text);
        }

        /// <summary>
        /// Event that happens after you finish evaluating a Given, When, or Then block
        /// </summary>
        [AfterScenarioBlock]
        public void FinalizeBlock()
        {
            // Log("FinalizeBlock (Given, When or Then)");
        }

        /// <summary>
        /// Event that happens after you finish evaluating a scenario
        /// </summary>
        [AfterScenario]
        public void FinalizeScenario()
        {
            // Log("FinalizeScenario: {0}", _scenarioContext.ScenarioInfo.Title);
            if (HaveCurrentReport()) CloseReport();
        }

        /// <summary>
        /// Event that happens after you finish evaluating a feature
        /// </summary>
        /// <param name="featureContext">Current feature</param>
        [AfterFeature]
        public static void FinalizeFeature(FeatureContext featureContext)
        {
            // Log("FinalizeFeature: {0}", featureContext.FeatureInfo.Title);
        }
        /// <summary>
        /// Event that happens after you finish evaluating the test
        /// </summary>
        [AfterTestRun]
        public static void FinalizeTest()
        {
            // Log("FinalizeTest");
        }

        /// <summary>
        /// Update the current scenario context
        /// </summary>
        /// <param name="_scenarioContext">Current scenario</param>
        private static void UpdateCurrentScenarioContext(ScenarioContext _scenarioContext)
        {
            _currentScenarioContext = _scenarioContext;
        }
        /// <summary>
        /// Update the current feature context
        /// </summary>
        /// <param name="featureContext">Current feature</param>
        private static void UpdateCurrentFeatureContext(FeatureContext featureContext)
        {
            _currentFeatureContext = featureContext;
        }
    }
}
