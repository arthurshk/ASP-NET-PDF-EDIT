using AspNetLesson01.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Reflection.Metadata;

namespace AspNetLesson01.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly AboutMe aboutMe;

        public HomeController()
        {
           
            aboutMe = new AboutMe()
            {
                LastName = "Arthur",
                FirstName = "Shkryabiy",
                Age = 22,
                Description = "Senior web developer",
                Skills = new() {
                        new Skill
                        {
                            Title = "C",
                            Experience = 50,
						},
						new Skill{
							Title = "C++",
							Experience = 70,
						},
						new Skill{
							Title = "C#",
							Experience = 80,
						},
						new Skill{
							Title = "ADO.Net",
							Experience = 90,
						},
						new Skill{
							Title = "ASP.NET Core",
							Experience = 100,
						},


						//"C", "C++", "C#", "ADO.Net", "ASP.NET Core",
      //                  "SQL", "MSSQL", "MySQL", "PostgreSQL", "MongoDB",
      //                  "JavaScript", "NodeJS", "Vue 2,3", "HTML", "CSS", "SCSS",
      //                  "Python", "PHP", "Yii2 framework", "Symfony", "Laravel",
}
            };
        }

        public IActionResult Index()
        {
            //ViewBag.AboutMe = aboutMe;
            //ViewBag.SomeText = "Hello world";
            ViewData["Title"] = "About me page";
            return View(aboutMe);
        }

        [HttpGet("/about-me")]
        public IActionResult AboutMe()
        {
            return Json(aboutMe);
        }

		[HttpPost("/about-me")]
		public IActionResult AboutMePost([FromForm] AboutMe data)
		{
			var aboutMe = this.aboutMe;
			aboutMe.FirstName = data.FirstName;
			aboutMe.LastName = data.LastName;
			aboutMe.Description = data.Description;
			ViewData["Title"] = "About me page (after post)";
			return View("Index", aboutMe);
		}
		[HttpGet("/about-me/pdf")]
		public IActionResult AboutMePDF()
		{
			var aboutMe = this.aboutMe;
			using (var stream = new MemoryStream())
			{
				var pdfDoc = new iTextSharp.text.Document();
				var writer = PdfWriter.GetInstance(pdfDoc, stream);
				pdfDoc.Open();
				pdfDoc.Add(new Paragraph("About Me PDF"));
				pdfDoc.Add(new Paragraph($"Full Name: {aboutMe.FirstName} {aboutMe.LastName}"));
				pdfDoc.Add(new Paragraph($"Description: {aboutMe.Description}"));
				pdfDoc.Close();
				stream.Seek(0, SeekOrigin.Begin);
				return File(stream, "application/pdf", "about_me.pdf");
			}
		}
		[HttpPost("/about-me/skills")]
        public IActionResult Skills([FromBody] SearchQuery query)
        {
            return Json(aboutMe.Skills.Where(s => s.Title.ToLower().Contains(query.Query.ToLower())));
        }
    }
}
