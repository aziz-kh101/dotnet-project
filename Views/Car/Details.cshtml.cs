using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project;

namespace MyApp.Namespace
{
    public class CarDetailsModel
    {
        public Car Car { get; set; } = new();
    }
}
