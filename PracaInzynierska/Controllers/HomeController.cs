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
            try
            {
                foreach(var file in files)
                {
                    if (file != null && file.Length != 0)
                    {
                        var filePath = Path.GetTempFileName();
                        using (var image = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(image);
                        }


                        byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                        string base64Data = Convert.ToBase64String(imageArray);
                        var imageSource = String.Format("data:image/png;base64,{0}", base64Data);


                        modelInput.ImageSource = filePath;
                        var imagePrediction = consumeModel.Predict(modelInput);


                        string label="";
                        if((imagePrediction.Score.Max()*100)>=85)
                        {
                            label = "I have recognized the bird. It is:";
                        }
                        else if(((imagePrediction.Score.Max() * 100) < 85) && ((imagePrediction.Score.Max()*100)>50))
                        {
                            label = "I have recognized the bird. I think it is:";
                        }
                        PredictionList.Add(new Result {Image = imageSource, 
                                                       PredictionResult = imagePrediction.Prediction, 
                                                       Score=imagePrediction.Score.Max()*100,
                                                       Label = label });
                    }
                }
            jsonFile = JsonConvert.SerializeObject(PredictionList);
            }
            catch (Exception)
            {
                return Json("Error, try again");
            }

            return Json(jsonFile);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
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