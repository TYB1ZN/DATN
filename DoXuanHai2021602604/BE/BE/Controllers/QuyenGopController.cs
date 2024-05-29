using BE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
namespace BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuyenGopController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		public QuyenGopController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		[HttpGet]
		public JsonResult Get()
		{
			string query = "select * from QuyenGop";
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
		public JsonResult Post(QuyenGop quyenGop)
		{
			string query = @"Insert into QuyenGop values
				(N'" + quyenGop.TenNguoiQuyenGop + "' ," +
				"N'" + quyenGop.GioiTinh + "', " +
				"N'" + quyenGop.SDT + "'," +
				quyenGop.SoLuong + ", " +
				"N'" + quyenGop.TenSach + "'," +
				"N'" + quyenGop.NgayTao + "', " +
				"N'" + quyenGop.LoiNhan + "')";
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
		public JsonResult Put(QuyenGop quyenGop)
		{
			string query = @"Update QuyenGop set
			TenNguoiQuyenGop = N'" + quyenGop.TenNguoiQuyenGop + "', " +
			"GioiTinh = N'" +quyenGop.GioiTinh+ "', "+
			"SDT = N'"+quyenGop.SDT +"', "+
			"SoLuong = N'"+quyenGop.SoLuong +"', "+
			"TenSach = N'"+quyenGop.TenSach +"', "+
			"LoiNhan = N'"+quyenGop.LoiNhan +"'"+
			"where MaQuyenGop = " + quyenGop.MaQuyenGop;
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
			string query = @"Delete from QuyenGop " +
			"where MaQuyenGop = " + id;
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
