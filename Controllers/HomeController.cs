﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HashlockDemo.Models;
using Ustream;

namespace HashlockDemo.Controllers
{
    public class HomeController : Controller
    {
        //Static configuration for demo
        private const int videoId = 1;
        private const string hashSecret = "TOPSECRET";

        public IActionResult Index()
        {
            ViewData["videoId"] = HomeController.videoId;
            return View();
        }

        public IActionResult Authorization(string ustreamContentType, string ustreamContentId)
        {
            //You get type of content in ustreamContentType query parameter and ID of content in ustreamContentId query parameter
            //You can check in your catalog the user is logged in and has permission to wacth this content
            var isAuthorized = true;

            if (isAuthorized)
            { //If user is authorized redirect to hashlock pass
                int oneDayTTL = 24 * 3600;
                // Collect data for hash
                Hash hashData = new Hash();
                hashData.Add("user", "youruser@example.com");
                var json = hashData.getHash(HomeController.hashSecret, oneDayTTL); //Create signed JSON hash data

                // Redirect the hashlock pass url
                return Redirect("https://www.ustream.tv/embed/hashlock/pass?hash=" + json);
            }

            //If not authorized you display a message wathever you want
            ViewData["contentId"] = ustreamContentId;
            ViewData["contentType"] = ustreamContentType;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}