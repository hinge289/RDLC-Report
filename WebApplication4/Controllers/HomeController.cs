using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
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
            //pass report path  and filename 
            MailSenderr(result.MainStream, "AllEmpoyee");
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
      
        public IActionResult List()
        {
            return View();
        }
     
        private void MailSenderr(byte[] pdfBytes, string filename)
        {

            // Sender email credentials
            string senderEmail = "tejashinge754@gmail.com";
            string senderPassword = "oezfvaerysltwdjg";

            // Receiver email
            string receiverEmail ="hingetejas9@gmail.com";

          

            // Email subject and body
            string subject = "All Empolyee Details";
            string body = $"All Empolyee Details with Salary Details";

            try
            {
                // Create the MailMessage object
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(receiverEmail);
                mail.Subject = subject;
                mail.Body = body;
                
                using (MemoryStream ms = new MemoryStream(pdfBytes))
                {
                    //pass MemoryStream , filename and type 
                    mail.Attachments.Add(new Attachment(ms,filename,"application/pdf"));
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                // Send the email
              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send OTP: {ex.Message}");
            }
           
        }
    }
}
