using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Interfaces;

namespace WebAPI.Models
{
    public class AspNetUsers : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        public string Discriminator { get; set; }
    }
}
