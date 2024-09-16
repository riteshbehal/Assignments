using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sqlapp.Models;
using sqlapp.Services;
using System.Collections.Generic;

namespace sqlapp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Course> Courses; 
        public void OnGet()
        {
            CourseService courseService = new CourseService();
            Courses= courseService.GetCourses();

        }
    }
}