using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class GiangDay
{
    public int HocKy { get; set; }

    public int NamHoc { get; set; }

    public string MaGv { get; set; } = null!;

    public string MaLop { get; set; } = null!;

    public virtual HocKyNamHoc HocKyNamHoc { get; set; } = null!;

    public virtual GiaoVien MaGvNavigation { get; set; } = null!;

    public virtual Lop MaLopNavigation { get; set; } = null!;
}
