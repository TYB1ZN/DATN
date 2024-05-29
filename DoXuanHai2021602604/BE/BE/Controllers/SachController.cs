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
	public class SachController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _env;
		public SachController(IConfiguration configuration, IWebHostEnvironment env)
		{
			_configuration = configuration;
			_env = env;
		}
		[HttpGet]
		public JsonResult Get()
		{
			string query = "select * from Sach";
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
		public JsonResult Post(Sach sach)
		{
			string query = @"Insert into Sach values
				(N'"+sach.TenMaSach +"',"+
				"N'" + sach.TenSach + "'" +
				", "+	sach.TrangThai +
				", N'" + sach.TacGia + "'" +
				", N'" + sach.AnhSach + "'" +
				", " + sach.GiaTien +
				", N'" + sach.MoTa + "'" +
				", " + sach.MaTheLoai +
				", N'" + sach.TenMaTheLoai+ "')";
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
		[Route("GetById")]
		[HttpPost]
		public JsonResult GetById(Sach sach)
		{
			string query = @"select * from Sach Where
				TenSach = N'"+sach.TenSach+"' or TenMaSach = '"+sach.TenMaSach+"'";
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
		[HttpPut]
		public JsonResult Put(Sach sach)
		{
			string query = @"Update Sach set
			TenSach = N'" + sach.TenSach + "', " +
			"TenMaSach = N'"+ sach.TenMaSach + "'," +
			"TacGia = N'" + sach.TacGia + "' , " +
			"TrangThai = " + sach.TrangThai + ", " +
			"AnhSach = N'" + sach.AnhSach + "', " +
			"GiaTien = " + sach.GiaTien + ", " +
			"MoTa = N'" + sach.MoTa + "', " +
			"MaTheLoai = " + sach.MaTheLoai +", "+
			"TenMaTheLoai = N'"+sach.TenMaTheLoai +"'"+
			"where MaSach = " + sach.MaSach ;
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
			string query = @"Delete from Sach " +
			"where MaSach = " +id;
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
		[Route("SaveFile")]
		[HttpPost]
		public JsonResult SaveFile()
		{
			try
			{
				var httpRequest = Request.Form;
				var postedFile = httpRequest.Files[0];
				string filename = postedFile.FileName;
				var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
				using(var stream = new FileStream(physicalPath, FileMode.Create))
				{
					postedFile.CopyTo(stream);	
				}
				return new JsonResult(filename);
			}
			catch(Exception)
			{
				return new JsonResult("com.jpg");
			}
		}
		[Route("GellAllTenTheLoai")]
		[HttpGet]
		public JsonResult GellAllTenTheLoai()
		{
			string query = "select * from TheLoai";
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
	}
}
