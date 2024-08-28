using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class Lop
{
    public string MaLop { get; set; } = null!;

    public string MaLoai { get; set; } = null!;

    public string MaPhong { get; set; } = null!;

    public string TenLop { get; set; } = null!;

    public int SiSo { get; set; }

    public virtual ICollection<GiangDay> GiangDays { get; set; } = new List<GiangDay>();

    public virtual LoaiLop MaLoaiNavigation { get; set; } = null!;

    public virtual PhongHoc MaPhongNavigation { get; set; } = null!;

    public virtual ICollection<TreEm> MaTes { get; set; } = new List<TreEm>();
}
