using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using Microsoft.Data.Sqlite;



public class LoginController : Controller {
    private string data_base_name = "db.db";


    [Route("/")]
    public IActionResult Index()
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");

        return View();
    }


    [Route("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }



    [Route("/login")]
    public IActionResult Login()
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");
        return View();
    }



    [HttpPost]
    [Route("/login")]
    public IActionResult Login(IFormCollection form)
    {
        if (form is null)
            return View();

        using (var connection = new SqliteConnection("Data Source=" + data_base_name))
        {
            string username = form["username"].ToString();
            string password = form["password"].ToString();

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password;";
            command.Parameters.AddWithValue("@Username", form["username"].ToString());
            command.Parameters.AddWithValue("@Password", MD5Hash(form["password"].ToString()));
            System.Console.WriteLine(MD5Hash(form["password"].ToString()));

            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                ViewData["Username"] = username;
                ViewData["Message"] = "Login successful";
                HttpContext.Session.SetString("Username", username);
                Console.WriteLine(ViewData["Username"].ToString() + ViewData["Message"].ToString());
            }
            else
            {
                ViewData["Message"] = "Invalid username or password";
            }

        }
        return View();
    }


    public static string MD5Hash(string input)
    {
        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }



    [Route("/register")]
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    [Route("/register")]
    public IActionResult Register(IFormCollection form)
    {
        if (form is null)
            return View();

        using (var connection = new SqliteConnection("Data Source=" + data_base_name))
        {
            string username = form["username"].ToString();
            string password = form["password"].ToString();

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password);";
            command.Parameters.AddWithValue("@Username", form["username"].ToString());
            command.Parameters.AddWithValue("@Password", MD5Hash(form["password"].ToString()));
            System.Console.WriteLine(MD5Hash(form["password"].ToString()));

            if(command.ExecuteNonQuery() == 1){
                ViewData["Message"] = "Registered succesfully";
            }
            else{
                ViewData["Message"] = "Something went wrong...";
            }


        }
        return View();
    }






    [Route("/data")]
    public IActionResult Data()
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");

        if (ViewData["Username"] is null)
        {
            return RedirectToAction("Login");
        }

        using (var connection = new SqliteConnection("Data Source=" + data_base_name))

        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Data WHERE UserId = (SELECT Id FROM Users WHERE Username = @Username);";
            command.Parameters.AddWithValue("@Username", ViewData["Username"].ToString());
            var reader = command.ExecuteReader();
            List<String> dataList = new List<String>();
            while (reader.Read())
            {
                dataList.Add(new String(reader.GetString(1)));
            }
            ViewData["DataList"] = dataList;

            foreach (var item in dataList)
            {
                System.Console.WriteLine(item);
            }
        }
        return View();
    }

    [HttpPost]
    [Route("/data")]
    public IActionResult Data(IFormCollection form)
    {
        ViewData["Username"] = HttpContext.Session.GetString("Username");

        if (ViewData["Username"] is null)
        {
            return RedirectToAction("Login");
        }

        using (var connection = new SqliteConnection("Data Source=" + data_base_name))

        {

            String username = HttpContext.Session.GetString("Username");
            String content = form["data"].ToString();

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Data (Content, UserId) VALUES (@Content, (SELECT Id FROM Users WHERE Username = @Username));";
            System.Console.WriteLine(username);
            System.Console.WriteLine(content);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Content", content);
            command.ExecuteNonQuery();

            ViewData["Message"] = "Data added successfully";

        }

        return Data();
    }


}