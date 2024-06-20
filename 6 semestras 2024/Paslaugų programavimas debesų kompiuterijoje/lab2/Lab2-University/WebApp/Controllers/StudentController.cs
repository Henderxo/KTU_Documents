using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models; // Assuming you have a Student class in your MVC app

namespace WebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient _httpClient;

        public StudentController(IHttpClientFactory httpClientFactory)
        {
            Console.WriteLine("access3");
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://studentsapi:8080"); // Docker container name
        }

        public async Task<IActionResult> Students()
        {
            Console.WriteLine("access2");
            var response = await _httpClient.GetAsync("/api/students");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var students = JsonConvert.DeserializeObject<List<Student>>(content);
                return View(students);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }

    }


}
