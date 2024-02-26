using LoginRegistrationApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace LoginRegistrationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("registration")]
        public string registration(Registration registration)
        {
            SqlConnection _connection = new SqlConnection(_configuration.GetConnectionString("ToysCon").ToString());
            SqlCommand _command = new SqlCommand("INSERT INTO Registration(UserName,Password,Email,IsActive) VALUES ('"+registration.UserName+ "','"+registration.Password+ "','"+registration.Email+ "','"+registration.IsActive+"')", _connection);
            _connection.Open();
            int i = _command.ExecuteNonQuery();
            _connection.Close();
            if(i > 0)
            {
                return "Data Inserted";
            }
            else
            {
                return "Error";
            }
            return "";
        }

        [HttpPost]
        [Route("login")]
        public string login(Registration registration)
        {
            SqlConnection _connection = new SqlConnection(_configuration.GetConnectionString("ToysCon").ToString());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Registration WHERE Email = '" + registration.Email + "' AND Password = '" + registration.Password + "' AND IsActive = 1", _connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                return "Data Found";
            }
            else
            {
                return "Invalid User";
            }
        }

    }
}
