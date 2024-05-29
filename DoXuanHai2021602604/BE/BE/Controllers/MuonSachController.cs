using BE.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
namespace BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MuonSachController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		public MuonSachController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpGet]
		public JsonResult Get()
		{
			string query = "select * from MuonSach";
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
		public JsonResult Post(MuonSach muonSach)
		{
			string query = @"insert into MuonSach values 
				(N'"+muonSach.TenNguoiMuon+"'," + 
				"N'"+muonSach.MaSVNguoiMuon+"',"+
				"N'"+muonSach.TenLop+"'," +
					muonSach.MaSach+","+
				"'" +muonSach.TenMaSach+ "', "+
				"N'"+muonSach.TenThanhVien+"',"+
				"'"+muonSach.NgayMuon+"',"+
				"'"+muonSach.NgayDuKienTra+"' ,"+
				"N'"+muonSach.GhiChu+"',"+
				muonSach.GiaCocSach+")";
			//insert into MuonSach values(
			//N'Đỗ Ánh',N'2021602604',N'KHMT01',1,N'VH',N'Đỗ Hải','2024-02-01', '2024-05-12',N'ngày trả',12000)
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
		public JsonResult Put(MuonSach muonSach)
		{
			string query = @"Update MuonSach set
				TenNguoiMuon = N'" + muonSach.TenNguoiMuon + "'," +
				"MaSVNguoiMuon = N'" + muonSach.MaSVNguoiMuon + "'," +
				"TenLop = N'" + muonSach.TenLop + "'," +
				"MaSach = "+muonSach.MaSach +","+
				"TenMaSach = N'"+muonSach.TenMaSach +"', "+
				"TenThanhVien = N'" + muonSach.TenThanhVien + "'," +
				"NgayMuon = '" + muonSach.NgayMuon + "'," +
				"NgayDuKienTra = '" + muonSach.NgayDuKienTra + "' ," +
				"GhiChu = N'" + muonSach.GhiChu +"' "+
				"GiaCocSach ="+ muonSach.GiaCocSach +
				"where MaMuonSach = " + muonSach.MaMuonSach ;
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
			string query = @"Delete from MuonSach " +
			"where MaMuonSach = " +id;
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
		[Route("GellAllSach")]
		[HttpGet]
		public JsonResult GellAllSach()
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
	}

}
