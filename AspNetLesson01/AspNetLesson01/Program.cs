using AspNetLesson01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Common.Internal;
using Newtonsoft.Json;
using System.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
