using System;
using System.Collections.Generic;

namespace QLPreschool.Data;

public partial class PhuHuynh
{
    public string MaPh { get; set; } = null!;

    public string TenPh { get; set; } = null!;

    public string SdtPhuhuynh { get; set; } = null!;

    public string DiaChiPh { get; set; } = null!;

    public virtual ICollection<TreEm> TreEms { get; set; } = new List<TreEm>();

    public virtual User? User { get; set; }
}
