using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class MonHoc
{
    public string MaMon { get; set; } = null!;

    public string? TenMon { get; set; }

    public virtual ICollection<LoaiLop> MaLoais { get; set; } = new List<LoaiLop>();
}
