using BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
namespace BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BanController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		public BanController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		[HttpGet]
		public JsonResult Get()
		{
			string query = "select MaBan, TenMaBan, TenBan from Ban";
			DataTable table = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("QLCLB");
			SqlDataReader myReader;
			using(SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myReader = myCommand.ExecuteReader();
					table.Load(myReader);
					myReader.Close();
					myCon.Close();
				}
			}
			return new JsonResult(table);
		}

		[HttpPost]
		public JsonResult Post(Ban ban)
		{
			string query = @"Insert into Ban values

				(N'"+ban.TenMaBan+ "',"+
				"N'"+ ban.TenBan+"')";
			DataTable table = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("QLCLB");
			SqlDataReader myReader;
			using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myReader = myCommand.ExecuteReader();
					table.Load(myReader);
					myReader.Close();
					myCon.Close();
				}
			}
			return new JsonResult("Them moi thanh cong");

		}
		[HttpPut]
		public JsonResult Put(Ban ban)
		{
			string query = @"Update Ban set
			TenMaBan = N'" + ban.TenMaBan +"', "+
			"TenBan = N'"+ ban.TenBan +"'"+
			"where MaBan = " + ban.MaBan;
			DataTable table = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("QLCLB");
			SqlDataReader myReader;
			using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myReader = myCommand.ExecuteReader();
					table.Load(myReader);
					myReader.Close();
					myCon.Close();
				}
			}
			return new JsonResult("Cap nhap thanh cong");
		}
		[HttpDelete("{id}")]
		public JsonResult Delete(int id)
		{
			string query = @"Delete from Ban " +
			"where MaBan = " + id;
			DataTable table = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("QLCLB");
			SqlDataReader myReader;
			using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myReader = myCommand.ExecuteReader();
					table.Load(myReader);
					myReader.Close();
					myCon.Close();
				}
			}
			return new JsonResult("Đã xoá thành công");
		}
	}
}
