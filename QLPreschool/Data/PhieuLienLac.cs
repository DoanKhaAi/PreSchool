using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class PhieuLienLac
{
    public string MaTe { get; set; } = null!;

    public string Thang { get; set; } = null!;

    public string Nam { get; set; } = null!;

    public string LoiNhanXet { get; set; } = null!;

    public virtual TreEm MaTeNavigation { get; set; } = null!;

    public virtual ThoiGian ThoiGian { get; set; } = null!;
}
