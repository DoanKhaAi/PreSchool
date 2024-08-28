using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class ThoiGian
{
    public string Thang { get; set; } = null!;

    public string Nam { get; set; } = null!;

    public virtual ICollection<PhieuLienLac> PhieuLienLacs { get; set; } = new List<PhieuLienLac>();

    public virtual ICollection<TheTrang> TheTrangs { get; set; } = new List<TheTrang>();
}
