using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sqlapp.Models;
using sqlapp.Services;

namespace sqlapp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Course> Courses; 
        public void OnGet()
        {
            CourseService productService = new CourseService();
            Courses = productService.GetCourses();

        }
    }
}