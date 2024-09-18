using RL.Data.DataModels.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Data.DataModels;

public class PlanProcedure : IChangeTrackable
{
    public PlanProcedure() => AssignedUsers = new List<AssignedUser>();
   
    [ForeignKey("Procedure")]
    public int ProcedureId { get; set; }

    [ForeignKey("Plan")]
    public int PlanId { get; set; }
    public virtual Procedure Procedure { get; set; }
    public virtual Plan Plan { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public virtual ICollection<AssignedUser> AssignedUsers { get; set; }
}
