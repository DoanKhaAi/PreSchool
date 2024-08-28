using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class GiaoVien
{
    public string MaGv { get; set; } = null!;

    public string TenGv { get; set; } = null!;

    public string ChucVu { get; set; } = null!;

    public string SdtGv { get; set; } = null!;

    public string? AvatarGv { get; set; }

    public virtual ICollection<GiangDay> GiangDays { get; set; } = new List<GiangDay>();

    public virtual User? User { get; set; }
}
