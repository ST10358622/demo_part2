using demo_part2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace demo_part2.Controllers
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
            //check the connection
            try
            {
                //get connection string from connection class
                connection conn = new connection();

                //then check
                using (SqlConnection connect = new SqlConnection(conn.connecting()))
                {
                    //open connection
                    connect.Open();
                    Console.WriteLine("Connected");
                    connect.Close();

                }

            }
            catch (IOException error)
            {
                //error 
                Console.WriteLine("Error : " + error.Message);

            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //http post for the register

        //from the register form
        [HttpPost]
        public IActionResult Register_user(register add_user)
        {
            //collect user's value
            string name = add_user.username;
            string email = add_user.email;
            string password = add_user.password;
            string role = add_user.role;


            ///check if all are collected 

            //Console.WriteLine("Name: " + name + "\nEmail: " + email + "Role: " + role);

            //passs all the va\lues to insert method
            string message = add_user.insert_user(name, email, role, password);

            //then check if the user is inserted
            if (message == "done")
            {
                //track error output
                Console.Write(message);
                //reirect
                return RedirectToAction("Login", "Home");

            }
            else
            {

                //track error output
                Console.Write(message);

                //redirect
                return RedirectToAction("Index", "Home");

            }
        }
        //for login page 
        public IActionResult Login()
        {
            return View();
        }

        //open dashboard
        public IActionResult Dashboard()
        {
            return View() ;
        }
        //login page
        [HttpPost]
        public IActionResult login_user(check_login user)

        {
            string email = user.email;
            string role = user.role;
            string password = user.password;

            string message = user.login_user(email, role, password);
            if (message == "found")
            {
                Console.WriteLine(message);
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                Console.WriteLine(message);
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]

        public IActionResult claim_sub(IFormFile file, claim insert)
        {
            //assign
            string module_name = insert.user_email;
            string hour_work = insert.hours_worked;
            string hour_rate = insert.hour_rate;
            string description = insert.description;



            //file info
            string filename = "no file";
            if (file != null && file.Length > 0)
            {
                // Get the file name
                 filename = Path.GetFileName(file.FileName);

                // Define the folder path (pdf folder)
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/pdf");

                // Ensure the pdf folder exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Define the full path where the file will be saved
                string filePath = Path.Combine(folderPath, filename);

                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                   
               
 }
            }


            string message = insert.insert_claim(module_name, hour_work, hour_rate, description,filename);


            if(message == "done")
            {
                Console.WriteLine(message);
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                Console.WriteLine(message);
                return RedirectToAction("Dashboard", "Home");
            }
            
        }
       
        public IActionResult view_claims()
        {
            //constructor for it to refresh automatically
            get_claims collect = new get_claims();



            return View(collect);
        }

    }
}

