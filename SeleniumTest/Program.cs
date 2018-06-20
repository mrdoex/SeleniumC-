using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace SeleniumTest
{
    
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new FirefoxDriver();
            // Ingresar a URL donde se ejecutarán las pruebas
            driver.Navigate().GoToUrl("http://10.10.90.169:6969/Cav/Home");
            driver.Manage().Window.Maximize();
            //ACÁ PARTE LA PRUEBA//
            //Seleccionamos un remate mediante el buscador para la subida de archivos.
            IWebElement cmbRemates = driver.FindElement(By.Id("select2-ddlRemate-container"));
            cmbRemates.Click();
            IWebElement buscadorRemate = driver.FindElement(By.CssSelector(".select2-search__field"));
            buscadorRemate.SendKeys("578");
            buscadorRemate.SendKeys(Keys.Enter);
            //Examinamos los archivos CAV
            string[] files = Directory.GetFiles(@"C:\Users\Bastian Mella\Desktop\CAV_Pruebas\", "*.pdf");
            //Por cada recorrido del array cargo un documento al examinar archivos, hasta examinar todos los que se encuentran en la carpeta
            foreach (string file in files)
            {
                driver.FindElement(By.Id("fileCargaMasiva")).SendKeys(@file.ToString());
            }
            //Presiono el botón subir archivos
            IWebElement btnSubir = driver.FindElement(By.CssSelector("#principalContent > div:nth-child(4) > div > div.file-input.theme-fa > div.input-group.file-caption-main > div.input-group-btn > a > span"));
            btnSubir.Click();
            //Damos un tiempo a la aplicación para que procese los documentos.
            Thread.Sleep(30000);
            //Confirmar pop up de éxito 
            IWebElement btnConfirmar = driver.FindElement(By.CssSelector("body > div.swal2-container.swal2-center.swal2-fade.swal2-shown > div > div.swal2-actions > button.swal2-confirm.swal2-styled"));
            btnConfirmar.Click();
            //Expandir desplegable disponibles para actualizar 
            IWebElement despActualizar = driver.FindElement(By.CssSelector("div.panel:nth-child(2) > div:nth-child(1) > a:nth-child(1)"));
            despActualizar.Click();
            //Marcamos todos los check de desplegable actualizar
            IWebElement chkActualizar = driver.FindElement(By.CssSelector("#dtReplace > thead > tr > th.text-center.select-checkbox.sorting_disabled > input[type='checkbox']"));
            chkActualizar.Click();
            //Eliminamos los archivos subidos recientemente
            IWebElement btnEliminar = driver.FindElement(By.Id("btnDelete"));
            btnEliminar.Click();
            Thread.Sleep(5000);
            //Confirmamos pop up de éxito en la eliminación de los elementos en la memoria.
            btnConfirmar = driver.FindElement(By.CssSelector("body > div.swal2-container.swal2-center.swal2-fade.swal2-shown > div > div.swal2-actions > button.swal2-confirm.swal2-styled"));
            btnConfirmar.Click();
            Console.ReadKey();

        }
    }
}
