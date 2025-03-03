using System.Data;
using System.Diagnostics;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;
using WebApplication4.Repo;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _Environment;
        private Genric _genric;
        private BLClass _bls;
        private DbConnection1 _context;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, Genric genric, DbConnection1 context, BLClass bls = null)
        {
            _logger = logger;
            _Environment = environment;
            _genric = genric;
            _bls = bls;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Print()
        {
            //Coures
            //var dt = new DataTable();
            //dt = GetAllCourse();

            //string mimietype = "";
            //int extention = 1;
            //var path= $"{_Environment.WebRootPath}\\Reportss\\Report1.rdlc";
            //LocalReport rpt = new LocalReport(path);
            //rpt.AddDataSource("Courses", dt);
            //var result = rpt.Execute(RenderType.Pdf, extention, null, mimietype);
            //return File(result.MainStream, "application/pdf");
            //Empolyee
            var dt = new DataTable();
         
                dt = GetAllEmpolyee();
            string mimietype = "";
            int extention = 1;
            var path = $"{_Environment.WebRootPath}\\Reportss\\Empolyee.rdlc";
            LocalReport rpt = new LocalReport(path);
            rpt.AddDataSource("DataSet1", dt);
            var result = rpt.Execute(RenderType.Pdf, extention, null, mimietype);
            return File(result.MainStream, "application/pdf");
        }
        private DataTable GetAllEmpolyee()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmployeeID", typeof(int));     
            dt.Columns.Add("FirstName", typeof(string));    
            dt.Columns.Add("LastName", typeof(string)); 
            dt.Columns.Add("Email", typeof(string));        
            dt.Columns.Add("Department", typeof(string));   
            dt.Columns.Add("Salary", typeof(decimal));     
            dt.Columns.Add("HireDate", typeof(DateTime));  


            var employees = _context.Employee.AsNoTracking().ToList();
            employees.ForEach(emp =>
      dt.Rows.Add(emp.EmployeeID, emp.FirstName, emp.LastName, emp.Email, emp.Department, emp.Salary, emp.HireDate));
            return dt;
        }
        //private DataTable GetAllCourse()
        //{
        //    var dt = new DataTable();
        //    dt.Columns.Add("COURSE_ID",typeof(int));
        //    dt.Columns.Add("COURSE_NAME",typeof(string));
        //    var course = _context.CourseNew.AsNoTracking().ToList();
        //    course.ForEach(x => dt.Rows.Add(x.COURSE_ID, x.COURSE_NAME));
        //    return dt;  
        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult List()
        {
            return View();
        }
        public FileContentResult DownloadReport()
        {
            //var SDList = _bls.GetScanDoucment();
            //var Datatable = _bls.ListToDataTable(SDList);
            var dt = _bls.getdata();
            string format = "PDF";
            string extension = "pdf";
            string contenttype = "application/pdf";
            var localPath = $"{_Environment.WebRootPath}\\Reports\\Report1.rdlc";
            var localreport = new LocalReport(localPath);
            localreport.AddDataSource("ds_Doucment", dt);

            var res = localreport.Execute(RenderType.Pdf, 1, null, contenttype);
            return File(res.MainStream, contenttype);
        }
    }
}
