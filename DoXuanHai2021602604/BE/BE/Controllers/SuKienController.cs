using BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SuKienController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _env;
		public SuKienController(IConfiguration configuration, IWebHostEnvironment env)
		{
			_configuration = configuration;
			_env = env;
		}
		[HttpGet]
		public JsonResult Get()
		{
			string query = "select * from SuKien";
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
			return new JsonResult(table);
		}
		/*MaSach nvarchar(50) not null primary key,
		TenSach nvarchar(500) not null ,
		TacGia nvarchar(50) not null,
		AnhSach nvarchar(500),
		GiaTien money,
		MoTa nvarchar(500),
		MaTheLoai nvarchar(50)*/

		[HttpPost]
		public JsonResult Post(SuKien suKien)
		{
			string query = @"Insert into SuKien values
				( N'" + suKien.TenSuKien + "'" +
				", '" + suKien.ThoiGian + "'" +
				", N'" + suKien.DiaDiem + "'" +
				", N'" + suKien.MoTa + "')";
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
		public JsonResult Put(SuKien suKien)
		{
			string query = @"Update SuKien set
			TenSuKien = N'" + suKien.TenSuKien + "', " +
			"ThoiGian = '" + suKien.ThoiGian + "' , " +
			"DiaDiem = N'" + suKien.DiaDiem + "', " +
			"MoTa = N'" +suKien.MoTa+"'"+
			"where MaSukien = " + suKien.MaSuKien ;
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
			string query = @"Delete from SuKien " +
			"where MaSuKien = " +id;
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
