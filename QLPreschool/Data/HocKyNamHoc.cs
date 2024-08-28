using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class HocKyNamHoc
{
    public int HocKy { get; set; }

    public int NamHoc { get; set; }

    public virtual ICollection<GiangDay> GiangDays { get; set; } = new List<GiangDay>();
}
