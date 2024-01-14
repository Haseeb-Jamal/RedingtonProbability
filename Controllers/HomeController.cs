using Microsoft.AspNetCore.Mvc;
using Redington.Models;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace Redington.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string probabilityA, string probabilityB, string operation)
        {
            // Parse probabilities from strings to doubles
            if (!double.TryParse(probabilityA, out double pA) || !IsValidProbability(pA) ||
                !double.TryParse(probabilityB, out double pB) || !IsValidProbability(pB))
            {
                ViewBag.ErrorMessage = "Please input valid value between 0 and 1.";
                return View();
            }

            double result = 0;

            // Perform the selected operation
            switch (operation)
            {
                case "CombinedWith":
                    result = CombinedWith(pA, pB);
                    //LogData(pA, pB, operation);
                    break;

                case "Either":
                    result = Either(pA, pB);
                    //LogData(pA, pB, operation);
                    break;

                default:
                    ViewBag.ErrorMessage = "Invalid operation selected.";
                    return View();
            }

            ViewBag.Result = Math.Round(result, 2, MidpointRounding.ToEven); ;
            return View();
        }
        
        private double CombinedWith(double pA, double pB)
        {
            return pA * pB;
        }

        private double Either(double pA, double pB)
        {
            return pA + pB - (pA * pB);
        }

        private bool IsValidProbability(double probability)
        {
            return probability >= 0 && probability <= 1;
        }
        private void LogData(double probabilityA, double probabilityB, string operation)
        {
            // Folder, where a file is created.
            // Make sure to change this folder to your own folder
            string folder = @"C:\Temp\";
            // Filename
            string fileName = "LogCalculation.txt";
            // Fullpath. You can direct hardcode it if you like.
            string fullPath = folder + fileName;
            // Write file using StreamWriter
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("Date" + DateTime.Now);
                writer.WriteLine(operation);
                writer.WriteLine(probabilityA);
                writer.WriteLine(probabilityB);
            }
        }
    }

}

