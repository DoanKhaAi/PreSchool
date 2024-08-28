using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class TreEm
{
    public string MaTe { get; set; } = null!;

    public string MaPh { get; set; } = null!;

    public string TenTe { get; set; } = null!;

    public int NamSinhTe { get; set; }

    public bool? GioiTinh { get; set; }

    public virtual PhuHuynh MaPhNavigation { get; set; } = null!;

    public virtual ICollection<PhieuLienLac> PhieuLienLacs { get; set; } = new List<PhieuLienLac>();

    public virtual ICollection<TheTrang> TheTrangs { get; set; } = new List<TheTrang>();

    public virtual ICollection<Lop> MaLops { get; set; } = new List<Lop>();
}
