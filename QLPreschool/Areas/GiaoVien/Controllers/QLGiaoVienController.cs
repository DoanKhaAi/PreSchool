using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLPreschool.Data;
using QLPreschool.ModelViews;
using QLPreschool.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using QLPreschool.Models;

namespace QLPreschool.Areas.GiaoVien.Controllers
{
    [Area("GiaoVien")]
    public class QLGiaoVienController : Controller
    {
        private QlTMnContext _context { get; set; }
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        public QLGiaoVienController(QlTMnContext context,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore)
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var dsGV = (from gv in _context.GiaoViens select gv).ToList();
            return View(dsGV);
        }

        public IActionResult DetailGV(string? idGv)
        {
            if (String.IsNullOrEmpty(idGv))
            {
                return View("NotFound");
            }
            var gvDetail = _context.GiaoViens.Where(gv => gv.MaGv == idGv).FirstOrDefault();
            if (gvDetail == null)
            {
                return View("NotFound");
            }
            ViewBag.idGv = gvDetail.MaGv;
            ViewBag.GvDetail = gvDetail;
            var dsGiangDay = _context.GiangDays.Where(gd => gd.MaGv == gvDetail.MaGv).ToList();
            var detailGiangDayGVList = new List<GiangDayGVMV>();
            if (dsGiangDay.Count > 0)
            {
                foreach (var gd in dsGiangDay)
                {
                    var LopGD = _context.Lops.Where(l => l.MaLop == gd.MaLop).FirstOrDefault();
                    detailGiangDayGVList.Add(new GiangDayGVMV()
                    {
                        tenGV = gvDetail.TenGv,
                        AvatarGV = gvDetail.AvatarGv,
                        chucVu = gvDetail.ChucVu,
                        SDT_GV = gvDetail.SdtGv,
                        tenLop = LopGD.TenLop,
                        si_so = LopGD.SiSo,
                        hocKi = gd.HocKy,
                        namHoc = gd.NamHoc
                    });
                }
            }

            return View(detailGiangDayGVList);
        }

        [HttpGet]
        public IActionResult AddClassGV(string? idGv)
        {
            if (String.IsNullOrEmpty(idGv))
            {
                return View("NotFound");
            }
            ViewBag.idGV = idGv;
            LoadDataInAddClass();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> AddClassGV(string? idGv, [Bind] LopPTMV lopPT)
        {
            if (String.IsNullOrEmpty(idGv))
            {
                return View("NotFound");
            }
            var gvPT = _context.GiaoViens.Where(gv => gv.MaGv == idGv).FirstOrDefault();
            if (gvPT == null)
            {
                return View("NotFound");
            }
            var sucChuaPhong = _context.PhongHocs.Where(p => p.MaPhong == lopPT.MaPhong).FirstOrDefault().SucChua;
            if (lopPT.SiSo > sucChuaPhong)
            {
                ModelState.AddModelError("SiSo", $"Sỉ số vượt quá sức chứa phòng({sucChuaPhong})");
                ViewBag.idGV = idGv;
                LoadDataInAddClass();
                return View("AddClassGV");
            }
            if (String.IsNullOrEmpty(lopPT.MaLoai))
            {
                ModelState.AddModelError("MaLoai", $"Vui lòng chọn loại lớp");
                ViewBag.idGV = idGv;
                LoadDataInAddClass();
                return View("AddClassGV");
            }
            var lopCuoiCung = _context.Lops.Select(s => s).OrderByDescending(s => s.MaLop).Take(1);
            var idLopCuoi = lopCuoiCung.FirstOrDefault() != null ? lopCuoiCung.FirstOrDefault().MaLop : "L00";
            var idNewLop = GenerateKey.GenerateKeyPrimary(idLopCuoi, 2);
            try
            {
                await _context.Lops.AddAsync(new Lop()
                {
                    MaLoai = lopPT.MaLoai,
                    MaPhong = lopPT.MaPhong,
                    MaLop = idNewLop,
                    TenLop = lopPT.TenLop,
                    SiSo = lopPT.SiSo,
                });
                await _context.GiangDays.AddAsync(new GiangDay()
                {
                    MaGv = idGv,
                    MaLop = idNewLop,
                    HocKy = lopPT.HocKi,
                    NamHoc = lopPT.NamHoc
                });
                _context.SaveChanges();
                return RedirectToAction("DetailGV", new { idGv = idGv });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.idGV = idGv;
                LoadDataInAddClass();
                return View("AddClassGV");
            }
        }

        [HttpGet]
        public IActionResult AddGV()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGV([Bind] GiaoVienMV gvMV)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = await _userManager.FindByEmailAsync(gvMV.Email);
                if (checkEmail != null)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại");
                    return View("AddGV");
                }
                var fileUpload = gvMV.AvatarGV;
                if (fileUpload != null)
                {
                    var filename = GetUniqueFileName(fileUpload.FileName);
                    var pathFolder = Path.Combine(_hostingEnvironment.WebRootPath, "AdminDashboard\\img");
                    var filePath = Path.Combine(pathFolder, filename);
                    string? avatarStr = null;
                    if (!System.IO.File.Exists(filePath))
                    {
                        avatarStr = filename;
                        gvMV.AvatarGV.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    var giaoVienCuoi = _context.GiaoViens.Select(gv => gv).OrderByDescending(gv => gv.MaGv).Take(1);
                    var idGVCuoi = giaoVienCuoi.FirstOrDefault() != null ? giaoVienCuoi.FirstOrDefault().MaGv : "GV00";
                    var maGV = GenerateKey.GenerateKeyPrimary(idGVCuoi, 3);
                    await _context.GiaoViens.AddAsync(new Data.GiaoVien()
                    {
                        MaGv = maGV,
                        TenGv = gvMV.TenGV,
                        ChucVu = gvMV.ChucVu,
                        SdtGv = gvMV.SDT,
                        AvatarGv = avatarStr
                    });

                    _context.SaveChanges();
                    //Tạo user GV
                    //Kiểm tra unique Email


                    var user = CreateUser();
                    await _userStore.SetUserNameAsync(user, gvMV.Email, CancellationToken.None);
                    await _userManager.SetEmailAsync(user, gvMV.Email);
                    user.maGV = maGV;
                    var result = await _userManager.CreateAsync(user, gvMV.MatKhau);
                    if (result.Succeeded)
                    {
                        var userId = await _userManager.GetUserIdAsync(user);
                        //tạo ra token email confirm
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        await _userManager.ConfirmEmailAsync(user, code);
                    }
                    return RedirectToAction("Index");
                }
            }
            return View("AddGV");
        }
        private void LoadDataInAddClass()
        {
            var loaiLop = from ll in _context.LoaiLops select ll;
            ViewBag.loaiLop = new SelectList(loaiLop, "MaLoai", "TenLoai");
            var phongHoc = from p in _context.PhongHocs select p;
            ViewBag.phongHoc = new SelectList(phongHoc, "MaPhong", "TenPhong");
            var nmHk = from item in _context.HocKyNamHocs select item;
            ViewBag.nmHk = new SelectList(nmHk, "NamHoc", "NamHoc");
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
