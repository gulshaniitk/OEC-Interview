using RL.Data.DataModels.Common;
using System.ComponentModel.DataAnnotations;

namespace RL.Data.DataModels;

public class Procedure : IChangeTrackable
{
    public Procedure()
    {
        AssignedUsers = new List<AssignedUser>();
        PlanProcedures = new List<PlanProcedure>();
    }
    [Key]
    public int ProcedureId { get; set; }
    public string ProcedureTitle { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public virtual ICollection<AssignedUser> AssignedUsers { get; set; }
    public virtual ICollection<PlanProcedure> PlanProcedures { get; set; }
}
