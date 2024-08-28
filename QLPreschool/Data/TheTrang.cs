using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class TheTrang
{
    public string MaTe { get; set; } = null!;

    public string Thang { get; set; } = null!;

    public string Nam { get; set; } = null!;

    public double ChieuCao { get; set; }

    public double CanNang { get; set; }

    public virtual TreEm MaTeNavigation { get; set; } = null!;

    public virtual ThoiGian ThoiGian { get; set; } = null!;
}
