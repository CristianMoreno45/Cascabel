using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace Cascabel.BaseFramework.ReportExtension
{
    public class SpectFlowReport
    {

        private static ExtentTest _scenario;
        private static ScenarioContext _scenarioContext;
        private string _title;
        private ScenarioExecutionStatus _scenarioExecutionStatus;
        private static string _baseDirectory;
        private static string _reportDirectory;
        public SpectFlowReport(ExtentTest scenario, ScenarioContext scenarioContext, string baseDirectory, string reportDirectory)
        {
            _scenario = scenario;
            _scenarioContext = scenarioContext;
            _title = _scenarioContext.ScenarioInfo.Title;
            _scenarioExecutionStatus = _scenarioContext.ScenarioExecutionStatus;
            _baseDirectory = baseDirectory;
            _reportDirectory = reportDirectory;

        }
        /// <summary>
        /// Create a step in the SpecFlow report
        /// </summary>
        /// <param name="Driver">Web browser driver</param>
        /// <param name="stepInfo">Step Name</param>
        /// <param name="takePhoto">Take a photo (screen shot)?</param>
        /// <param name="ex">Exception detail</param>
        public void CreateStepStepReport(IWebDriver Driver, string stepInfo, bool takePhoto = true, Exception ex = null)
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType;
            switch (stepType)
            {
                case StepDefinitionType.Given:
                    CreateNode<Given>(Driver, stepInfo, takePhoto, ex);
                    break;
                case StepDefinitionType.When:
                    CreateNode<When>(Driver, stepInfo, takePhoto, ex);
                    break;
                case StepDefinitionType.Then:
                    CreateNode<Then>(Driver, stepInfo, takePhoto, ex);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Create a step in the test case
        /// </summary>
        /// <typeparam name="T"> Identify in step within the test case in Gherkin format</typeparam>
        public void CreateNode<T>(IWebDriver Driver, string stepInfo, bool takePhoto = true, Exception ex = null) where T : IGherkinFormatterModel
        {
            MediaEntityModelProvider mediaModel = null;
            string stepInformation = stepInfo;
            if (takePhoto)
            {
                string imageUrl = CaptureScreenshotAndReturnModel(Driver);
                if (imageUrl != "")
                {
                    mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(imageUrl).Build();
                    stepInformation += "<br/><a href='" + imageUrl + "'><img src='" + imageUrl + "' width='500px'></a>";
                }
                else
                    mediaModel = null;
            }

            // TODO: The way by exception => not working
            // If the step is unsuccessful by exception
            if (ex != null)
            {
                string detail = string.Format("<br/><span style='font-size:8px'><b>Error:</b> {0}<br/><b>Detail:</b><br/>{1}</span>", ex.Message, Newtonsoft.Json.JsonConvert.SerializeObject(ex.InnerException));
                _scenario.CreateNode<T>(stepInformation + detail, _title).Log(Status.Fail, "DONE", mediaModel);
            }
            else
            {
                switch (_scenarioExecutionStatus)
                {
                    case ScenarioExecutionStatus.OK:
                        _scenario.CreateNode<T>(stepInformation, _title).Log(Status.Pass, "DONE", mediaModel);
                        break;
                    case ScenarioExecutionStatus.BindingError:
                        _scenario.CreateNode<T>(stepInformation, _title).Log(Status.Error, "ERROR", mediaModel);
                        break;
                    case ScenarioExecutionStatus.StepDefinitionPending:// Si el paso esta pendiente
                        _scenario.CreateNode<T>(stepInformation).Skip("PENDING");
                        break;
                }
            }
        }

        /// <summary>
        /// Take a screenshot
        /// </summary>
        /// <param name="Driver">Web browser driver</param>
        /// <returns></returns>
        private string CaptureScreenshotAndReturnModel(IWebDriver Driver)
        {
            try
            {
                string finalPath = "";
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                int i = 0;
                string imagePath = "";
                string localpath = "";
                do
                {
                    imagePath = "\\" + i.ToString() + ".png";
                    localpath = _baseDirectory + _reportDirectory + imagePath;
                    i++;
                } while (File.Exists(localpath));

                screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
                finalPath = localpath;
                return finalPath;

            }
            catch (Exception ex)
            {
                Console.WriteLine("--> Error CaptureScreenshotAndReturnModel -  Message:{0}, Detail: {1} ", ex.Message, Newtonsoft.Json.JsonConvert.SerializeObject(ex.InnerException));
                return string.Empty;
            }
        }
    }
}
