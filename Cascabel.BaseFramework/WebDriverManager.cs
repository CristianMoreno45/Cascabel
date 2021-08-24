using Cascabel.BaseFramework.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cascabel.BaseFramework
{
    /// <summary>
    /// Clase encargada de controlar las acciones del explorador web
    /// </summary>
    public class WebDriverManager
    {
        /// <summary>
        /// Objeto que representa al explorador
        /// </summary>
        public static IWebDriver Driver;
        /// <summary>
        /// Url donde estan los archivos fisicos de la solución
        /// </summary>
        public static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Inicializar el controlador web
        /// </summary>
        /// <param name="driver"></param>
        public static void InitializeDriver(DriverAvailable driver = DriverAvailable.Chrome)
        {
            try
            {
                // Estrategia para determinar el navegador
                // TODO: Este item deberia ser seteado desde el Feature
                switch (driver)
                {
                    case DriverAvailable.Chrome:
                        SetChromeDriver();
                        break;
                    case DriverAvailable.Firefox:
                        SetFirefoxDriver();
                        break;
                    case DriverAvailable.Edge:
                        SetEdgeDriver();
                        break;
                    default:
                        SetChromeDriver();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0} - {1}", ex.Message, ex.InnerException);
                ClosePage();
                throw ex;
            }
        }

        /// <summary>
        /// Abrir una pagina Web en el explorador web en el navegador determinado
        /// </summary>
        /// <param name="url">Url que se desea abrir</param>
        /// <param name="driver">Navegador</param>
        public static void OpenPage(string url, DriverAvailable driver = DriverAvailable.Chrome)
        {
            try
            {
                if (Driver != null)
                {
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    Driver.Manage().Window.Maximize();
                    Driver.Navigate().GoToUrl(url);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0} - {1}", ex.Message, ex.InnerException);
                ClosePage();
            }
        }

        /// <summary>
        /// Setear comportameintos del navegador Google Chrome
        /// </summary>
        private static void SetChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("--whitelisted-ips=''");
            Driver = new ChromeDriver(BaseDirectory, options);
        }

        /// <summary>
        /// Setear comportameintos del navegador Fire Fox
        /// </summary>
        private static void SetFirefoxDriver()
        {
            //TODO: Actualmente no se cuenta con el Nuget Selenium.WebDriver.Firefox dado a que no hay la necesidad aun, pero se deja la posibilidad
            var options = new FirefoxOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("--whitelisted-ips=''");
            Driver = new FirefoxDriver(BaseDirectory, options);
        }

        /// <summary>
        /// Setear comportameintos del navegador Edge
        /// </summary>
        private static void SetEdgeDriver()
        {
            //TODO: Actualmente no se cuenta con el Nuget Selenium.WebDriver.Edge dado a que no hay la necesidad aun, pero se deja la posibilidad
            var options = new EdgeOptions();
            Driver = new EdgeDriver(BaseDirectory, options);
        }

        /// <summary>
        /// Cerrar el explorador web
        /// </summary>
        public static void ClosePage()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver = null;
            }
        }
    }
}
