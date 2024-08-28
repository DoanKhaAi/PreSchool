using System.ComponentModel;

namespace QLPreschool.ModelViews
{
    public class GiangDayGVMV
    {
        [DisplayName("Tên giáo viên")]
        public string tenGV { get; set; }
        public string AvatarGV { get; set; }
        public string chucVu {  get; set; }
        public string SDT_GV { get; set; }

        public string tenLop { get; set; }
        public int si_so { get; set; }
        public int hocKi { get; set; }
        public int namHoc {  get; set; }
    }
}
