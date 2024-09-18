using System.ComponentModel.DataAnnotations;
using RL.Data.DataModels.Common;

namespace RL.Data.DataModels;

public class User : IChangeTrackable
{
    public User() => AssignedUsers = new List<AssignedUser>();
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public virtual ICollection<AssignedUser> AssignedUsers { get; set; }
}