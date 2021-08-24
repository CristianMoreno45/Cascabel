using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;

namespace Cascabel.BaseFramework.ReportExtension
{
    public static class StepReport
    {

        private static ExtentReports CurrentReport;
        private static ExtentTest feature;
        private static ExtentTest scenario;
        private static SpectFlowReport report;
        private static string _baseDirectory;
        private static string ReportDirectory;
        private static string toDay;

        /// <summary>
        /// Initialization of the spec flow report
        /// </summary>
        /// <param name="baseDirectory">Base Directory</param>
        /// <param name="scenarioReportConfig">Scenario configuration</param>
        public static void InitializeReport(string baseDirectory, string scenarioReportConfig = "")
        {
            DateTime dt = DateTime.Now;
            toDay = dt.ToString("yyyyMMdd_HHmm");

            _baseDirectory = baseDirectory;

            if (!HaveCurrentReport())
            {
                string reportPathRoot = baseDirectory + @"\ReportFiles";
                ReportDirectory = @"\ReportFiles";

                if (!Directory.Exists(reportPathRoot))
                {
                    Directory.CreateDirectory(reportPathRoot);
                }

                reportPathRoot += @"\" + toDay;
                ReportDirectory += @"\" + toDay;

                Directory.CreateDirectory(reportPathRoot);

                string pathReport = reportPathRoot + @"\index.html";
                ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(pathReport);
                if (scenarioReportConfig != "")
                {
                    htmlReporter.LoadConfig(baseDirectory +"\\" +scenarioReportConfig);
                }
                CurrentReport = new ExtentReports();
                CurrentReport.AttachReporter(htmlReporter);
        
            }
        }

        /// <summary>
        /// Determine if it have Current Report
        /// </summary>
        /// <returns>true or false</returns>
        public static bool HaveCurrentReport()
        {
            if (CurrentReport == null)
                return false;
            return true;
        }
        /// <summary>
        /// Create a report from a scenario
        /// </summary>
        /// <param name="scenarioInfo">Scenario to describe</param>
        public static void CreateScenario(string scenarioInfo)
        {
            if (HaveCurrentReport())
            {
                scenario = CurrentReport.CreateTest<Scenario>(scenarioInfo);
            }
        }
        /// <summary>
        /// Create a report from a feature
        /// </summary>
        /// <param name="featureInfo">Feature to describe</param>
        public static void CreateFeature(string featureInfo)
        {
            if (HaveCurrentReport())
            {
                feature = CurrentReport.CreateTest<Feature>(featureInfo);
            }
        }
        /// <summary>
        /// Close Report
        /// </summary>
        public static void CloseReport()
        {
            // Terminar reporte
            if (CurrentReport != null)
            {
                CurrentReport.Flush();
            }
        }
        /// <summary>
        /// Insert step into current Report
        /// </summary>
        /// <param name="Driver">Web browser driver</param>
        /// <param name="scenarioContext">Current Scenario</param>
        /// <param name="takePhoto">Take a photo (screen shot)?</param>
        /// <param name="ex">Exception detail</param>
        public static void InsterStepIntoReport(IWebDriver Driver, ScenarioContext scenarioContext, bool takePhoto = true, Exception ex = null)
        {
            if (HaveCurrentReport())
            {
                report = new SpectFlowReport(scenario, scenarioContext, _baseDirectory, ReportDirectory);
                report.CreateStepStepReport(Driver, scenarioContext.StepContext.StepInfo.Text, takePhoto, ex: ex);
            }
        }
    }
} 


