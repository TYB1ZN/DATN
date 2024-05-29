namespace BE.Model
{
	public class SuKien
	//	MaSuKien int identity(1,1) not null primary key,
	//TenSuKien nvarchar(100),
	//ThoiGian date,
	//MoTa nvarchar(500),
	{
		public int MaSuKien { get; set; }
		public string TenSuKien { get; set; }
		public string ThoiGian { get; set; }
		public string DiaDiem { get; set; }
		public string MoTa {  get; set; }

	}
}
