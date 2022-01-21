using APIConsume.Helpers;
using APIConsume.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIConsume.Controllers
{
    public class CarController : Controller
    {
        private readonly IHttpClientFactory _client;

        public CarController(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var client = _client.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, SD.APIPathModel);
            var response = await client.SendAsync(request);

            List<VmModel> model = new List<VmModel>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string modelString = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<VmModel>>(modelString);

                return View(model);
            }

            ModelState.AddModelError("", "There is the problem on API");
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var client = _client.CreateClient();


            var requestBrand = new HttpRequestMessage(HttpMethod.Get, SD.APIPathBrand);
            var responseBrand = await client.SendAsync(requestBrand);

            if (responseBrand.StatusCode == HttpStatusCode.OK)
            {
                string modelBrandString = await responseBrand.Content.ReadAsStringAsync();
                ViewBag.Brand = JsonConvert.DeserializeObject<List<VmBrand>>(modelBrandString);
            }

            var requestColor = new HttpRequestMessage(HttpMethod.Get, SD.APIPathColor);
            var responseColor = await client.SendAsync(requestColor);

            if (responseColor.StatusCode == HttpStatusCode.OK)
            {
                string modelColorString = await responseColor.Content.ReadAsStringAsync();
                ViewBag.Color = JsonConvert.DeserializeObject<List<VmColor>>(modelColorString);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VmModelCreate model)
        {
            var client = _client.CreateClient();

            if (ModelState.IsValid)
            {
                model.Image = model.ImageFile.FileName;

                using (var steam = new MemoryStream())
                {
                    model.ImageFile.CopyTo(steam);
                    byte[] fileBytes = steam.ToArray();
                    model.ImageBase64 = Convert.ToBase64String(fileBytes);
                }

                var request = new HttpRequestMessage(HttpMethod.Post, SD.APIPathModel);
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("index");
                }


                ModelState.AddModelError("", "Problem on API");
                ModelState.AddModelError("", response.StatusCode.ToString());
            }

            var requestBrand = new HttpRequestMessage(HttpMethod.Get, SD.APIPathBrand);
            var responseBrand = await client.SendAsync(requestBrand);

            if (responseBrand.StatusCode == HttpStatusCode.OK)
            {
                string modelBrandString = await responseBrand.Content.ReadAsStringAsync();
                ViewBag.Brand = JsonConvert.DeserializeObject<List<VmBrand>>(modelBrandString);
            }

            var requestColor = new HttpRequestMessage(HttpMethod.Get, SD.APIPathColor);
            var responseColor = await client.SendAsync(requestColor);

            if (responseColor.StatusCode == HttpStatusCode.OK)
            {
                string modelColorString = await responseColor.Content.ReadAsStringAsync();
                ViewBag.Color = JsonConvert.DeserializeObject<List<VmColor>>(modelColorString);
            }

            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var client = _client.CreateClient();

            var requestBrand = new HttpRequestMessage(HttpMethod.Get, SD.APIPathBrand);
            var responseBrand = await client.SendAsync(requestBrand);

            if (responseBrand.StatusCode == HttpStatusCode.OK)
            {
                string modelBrandString = await responseBrand.Content.ReadAsStringAsync();
                ViewBag.Brand = JsonConvert.DeserializeObject<List<VmBrand>>(modelBrandString);
            }

            var requestColor = new HttpRequestMessage(HttpMethod.Get, SD.APIPathColor);
            var responseColor = await client.SendAsync(requestColor);

            if (responseColor.StatusCode == HttpStatusCode.OK)
            {
                string modelColorString = await responseColor.Content.ReadAsStringAsync();
                ViewBag.Color = JsonConvert.DeserializeObject<List<VmColor>>(modelColorString);
            }

            var requestModel = new HttpRequestMessage(HttpMethod.Get, SD.APIPathModel + id);
            var responseModel = await client.SendAsync(requestModel);
            VmModelCreate model = new VmModelCreate();
            if (responseModel.StatusCode == HttpStatusCode.OK)
            {
                string modelString = await responseModel.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<VmModelCreate>(modelString);
            }
            model.EngineString = model.Engine.ToString();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VmModelCreate model)
        {
            var client = _client.CreateClient();

            if (ModelState.IsValid)
            {
                model.Engine = Convert.ToDecimal(model.EngineString.Replace(".", ","));
                var request = new HttpRequestMessage(HttpMethod.Patch, SD.APIPathModel);
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                request.Headers.Add("id", model.Id.ToString());

                var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("index");
                }


                ModelState.AddModelError("", "Problem on API");
                ModelState.AddModelError("", response.StatusCode.ToString());
            }

            var requestBrand = new HttpRequestMessage(HttpMethod.Get, SD.APIPathBrand);
            var responseBrand = await client.SendAsync(requestBrand);

            if (responseBrand.StatusCode == HttpStatusCode.OK)
            {
                string modelBrandString = await responseBrand.Content.ReadAsStringAsync();
                ViewBag.Brand = JsonConvert.DeserializeObject<List<VmBrand>>(modelBrandString);
            }

            var requestColor = new HttpRequestMessage(HttpMethod.Get, SD.APIPathColor);
            var responseColor = await client.SendAsync(requestColor);

            if (responseColor.StatusCode == HttpStatusCode.OK)
            {
                string modelColorString = await responseColor.Content.ReadAsStringAsync();
                ViewBag.Color = JsonConvert.DeserializeObject<List<VmColor>>(modelColorString);
            }

            return View(model);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var client = _client.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Delete, SD.APIPathModel + id);
            var response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("Error", "Problem on API");
            return RedirectToAction("index");
        }

    }

}
