﻿using MediatR;
using RL.Backend.Models;
namespace RL.Backend.Commands
{
    public class RemoveAssignedUserCommand: IRequest<ApiResponse<Unit>>
    {
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public int ProcedureId { get; set; }
    }
}
