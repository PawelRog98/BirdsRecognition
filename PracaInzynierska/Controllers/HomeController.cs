using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Data;
using Newtonsoft.Json;
using PracaInzynierska.Models;
using PracaInzynierskaML.Model;
using Tensorflow.Keras.Engine;

namespace PracaInzynierska.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, ModelInput modelInput)
        {
            var PredictionList = new List<Result>();
            ConsumeModel consumeModel = new ConsumeModel();
            var jsonFile="";
            //try
            //{
                foreach(var file in files)
                {
                    if (file != null && file.Length != 0)
                    {
                        var filePath = Path.GetTempFileName();
                        byte[] imageArray = null;
                        using (var image = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(image);
                        }
                        imageArray = System.IO.File.ReadAllBytes(filePath);
                        modelInput.ImageSource = filePath;
                        string imreBase64Data = Convert.ToBase64String(imageArray);
                        var imagePrediction = consumeModel.Predict(modelInput);

                        var imgSrc = String.Format("data:image/png;base64,{0}", imreBase64Data);
                        ViewBag.Result = imagePrediction;
                    
                        PredictionList.Add(new Result {Image=imgSrc, PredictionResult = imagePrediction.Prediction, Score=imagePrediction.Score.Max()*100 });


                    }
                }
            jsonFile = JsonConvert.SerializeObject(PredictionList);
            //HttpContext.Session.SetString(jsonFile,);
            //}
            //catch (Exception)
            //{
            //    return Json(jsonFile);
            //}

            return Json(jsonFile);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }   
    }
}
