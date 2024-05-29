using BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ThanhVienController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _env;
		public ThanhVienController(IConfiguration configuration, IWebHostEnvironment env)
		{
			_configuration = configuration;
			_env = env;	
		}
		[HttpGet]
		public JsonResult Get()
		{
			string query = "select * from ThanhVien";
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
		public JsonResult Post(ThanhVien thanhVien)
		{
			string query = @"Insert into ThanhVien values
				(N'"+thanhVien.TenMaThanhVien+ "' "+
				", N'"+thanhVien.TenThanhVien+ "'" +
				", "+thanhVien.NamSinh  +
				", N'"+thanhVien.GioiTinh +"'" +
				", '"+ thanhVien.TenMaBan + "'"+
				", N'" +thanhVien.MaBan + "')";
				
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
		public JsonResult Put(ThanhVien  thanhVien)
		{
			string query = @"Update ThanhVien set
			TenMaThanhVien = N'"+thanhVien.TenMaThanhVien+"' ,"+
			"TenThanhVien = N'" + thanhVien.TenThanhVien + "' ," +
			"NamSinh = " + thanhVien.NamSinh + ", " +
			"GioiTinh = N'" +thanhVien.GioiTinh +"', "+
			"TenMaBan = N'" + thanhVien.TenMaBan +"', "+
			"MaBan =" +thanhVien.MaBan + 
			"where MaThanhVien = "+thanhVien.MaThanhVien;
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
			string query = @"Delete from ThanhVien " +
			"where MaThanhVien = " + id;
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
				using (var stream = new FileStream(physicalPath, FileMode.Create))
				{
					postedFile.CopyTo(stream);
				}
				return new JsonResult(filename);
			}
			catch (Exception)
			{
				return new JsonResult("com.jpg");
			}
		}
		[Route("GellAllTenBan")]
		[HttpGet]
		public JsonResult GellAllTenBan()
		{
			string query = "select * from Ban";
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
