using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetMQ.SendMessage.Models;
using NetMQ.Sockets;

namespace NetMQ.SendMessage.Controllers
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

        public IActionResult Privacy()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Privacy(string message)
        {
            using var client = new RequestSocket("tcp://localhost:8050");

            System.Console.WriteLine($"Sending message '{message}'...");

            client.SendFrame(message);
            //System.Console.WriteLine("Message has been sent.");

            List<string> messages = client.ReceiveMultipartStrings(2);
            //foreach (string message in messages)
            //{
            //    System.Console.WriteLine($"Server response: {message}");
            //}
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
