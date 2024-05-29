using BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
namespace BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NguoiDungController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		public NguoiDungController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		[Route("DangNhap")]
		[HttpPost]
		public JsonResult DangNhap(NguoiDung nguoiDung)
		{
			string query = "select count(*) from NguoiDung where TenDangNhap = '" + 
				nguoiDung.TenDangNhap + "' and MatKhau = '"+ nguoiDung.MatKhau+ "'";
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
		[Route("GoiTK")]
		[HttpPost]
		public JsonResult GoiTK(NguoiDung nguoiDung)
		{
			string query = "select * from NguoiDung where TenDangNhap = '" +
				nguoiDung.TenDangNhap + "' and MatKhau = '" + nguoiDung.MatKhau + "'";
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
		[HttpGet]
		public JsonResult Get()
		{
			string query = "select * from NguoiDung";
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

		private string GetDebuggerDisplay()
		{
			return ToString();
		}

		[HttpPost]
		public JsonResult Post(NguoiDung nguoiDung)
		{
			string query = @"Insert into NguoiDung values

				(N'" + nguoiDung.TenDangNhap + "'," +
				"N'" + nguoiDung.MatKhau + "',"+
				"N'"+nguoiDung.TenNguoiDung+"',"+
				nguoiDung.VaiTro+")";
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
		public JsonResult Put(NguoiDung nguoiDung)
		{
			string query = @"Update NguoiDung set
			TenDangnhap = N'" + nguoiDung.TenDangNhap + "', " +
			"MatKhau = N'" + nguoiDung.MatKhau + "'," +
			"TenNguoiDung = N'"+nguoiDung.TenNguoiDung+"',"+
			"VaiTro = "+nguoiDung.VaiTro+ 
			"where MaTK = " + nguoiDung.MaTK;
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
			string query = @"Delete from NguoiDung " +
			"where MaTK = " + id;
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
