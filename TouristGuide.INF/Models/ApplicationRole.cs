using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Application Role Model
    /// </summary>
    [NotMapped]
    public class ApplicationRole : IdentityRole
    {
    }
}
