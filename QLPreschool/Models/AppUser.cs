using Microsoft.AspNetCore.Identity;
using QLPreschool.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace QLPreschool.Models
{
    public class AppUser: IdentityUser
    {
        [StringLength(10,MinimumLength = 1)]
        [Column(TypeName = "varchar")]
        [AllowNull]
        public string? maPH {  get; set; }

        public PhuHuynh PhuHuynh { get; set; }

        [StringLength(10, MinimumLength = 1)]
        [Column(TypeName = "varchar")]
        [AllowNull]
        public string? maGV { get; set; }
        public GiaoVien GiaoVien { get; set; }
    }
}
