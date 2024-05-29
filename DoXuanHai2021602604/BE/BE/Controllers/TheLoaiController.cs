using BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
namespace BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TheLoaiController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		public TheLoaiController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		[HttpGet]
		public JsonResult Get()
		{
			string query = "select * from TheLoai";
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
		public JsonResult Post(TheLoai theLoai)
		{
			string query = @"Insert into TheLoai values
				(N'"+theLoai.TenMaTheLoai+ "' ,"+
				"N'"+theLoai.TenTheLoai+"')";
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
		public JsonResult Put(TheLoai theLoai)
		{
			string query = @"Update TheLoai set
			TenMaTheLoai = N'" + theLoai.TenMaTheLoai + "', " +
			"TenTheLoai = N'" +theLoai.TenTheLoai+
			"where MaTheLoai = " + theLoai.MaTheLoai;
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
			string query = @"Delete from TheLoai " +
			"where MaTheLoai = " +id;
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
