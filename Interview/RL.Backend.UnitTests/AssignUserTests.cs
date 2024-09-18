using FluentAssertions;
using MediatR;
using RL.Backend.Commands;
using RL.Backend.Commands.Handlers.Users;
using RL.Backend.Exceptions;

namespace RL.Backend.UnitTests
{
    [TestClass]
    public class AssignUserTests
    {

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task AssignUserToPlanProcedureTests_InvalidPlanId_ReturnsBadRequest(int planId)
        {
            //Given
            var context = DbContextHelper.CreateContext();
            var handler = new AssignUserToPlanProcedure(context);
            var request = new AssignUserCommand()
            {
                PlanId = planId,
                ProcedureId = 1,
                UserId = 1
            };
            //When
            var result = await handler.Handle(request, new CancellationToken());
            //Then
            result.Exception.Should().BeOfType(typeof(BadRequestException));
            result.Succeeded.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task AssignUserToPlanProcedureTests_InvalidUserId_ReturnsBadRequest(int userId)
        {
            //Given
            var context = DbContextHelper.CreateContext();
            var handler = new AssignUserToPlanProcedure(context);
            var request = new AssignUserCommand()
            {
                PlanId = 1,
                ProcedureId = 1,
                UserId = userId
            };
            //When
            var result = await handler.Handle(request, new CancellationToken());
            //Then
            result.Exception.Should().BeOfType(typeof(BadRequestException));
            result.Succeeded.Should().BeFalse();
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task AssignUserToPlanProcedureTests_InvalidProcedureId_ReturnsBadRequest(int procedureId)
        {
            //Given
            var context = DbContextHelper.CreateContext();
            var handler = new AssignUserToPlanProcedure(context);
            var request = new AssignUserCommand()
            {
                PlanId = 1,
                ProcedureId = procedureId,
                UserId = 1
            };
            //When
            var result = await handler.Handle(request, new CancellationToken());
            //Then
            result.Exception.Should().BeOfType(typeof(BadRequestException));
            result.Succeeded.Should().BeFalse();
        }


        [TestMethod]
        [DataRow(1, 1, 1)]
        [DataRow(2, 1, 2)]
        [DataRow(3, 2, 1)]
        public async Task AssignUserToPlanProcedureTests_UserNotFound_ReturnsNotFound(int userId, int planId, int procedureId)
        {
            //Given
            var context = DbContextHelper.CreateContext();
            context.Plans.Add(new Data.DataModels.Plan() { PlanId = 1 });
            context.Plans.Add(new Data.DataModels.Plan() { PlanId = 2 });
            context.Procedures.Add(new Data.DataModels.Procedure() { ProcedureId = 1 });
            context.Procedures.Add(new Data.DataModels.Procedure() { ProcedureId = 2 });

            await context.SaveChangesAsync();

            var handler = new AssignUserToPlanProcedure(context);
            var request = new AssignUserCommand()
            {
                PlanId = planId,
                ProcedureId = procedureId,
                UserId = userId
            };
            //When
            var result = await handler.Handle(request, new CancellationToken());
            //Then
            result.Exception.Should().BeOfType(typeof(NotFoundException));
            result.Succeeded.Should().BeFalse();
        }


        [TestMethod]
        [DataRow(1, 1, 1)]
        [DataRow(1, 1, 2)]
        [DataRow(1, 2, 1)]
        public async Task AssignUserToPlanProcedureTests_PlanProcedureNotFound_ReturnsNotFound(int userId, int planId, int procedureId)
        {
            //Given
            var context = DbContextHelper.CreateContext();
            context.Users.Add(new Data.DataModels.User() { UserId = 1 });
            context.Plans.Add(new Data.DataModels.Plan() { PlanId = 1 });
            context.Plans.Add(new Data.DataModels.Plan() { PlanId = 2 });
            context.Procedures.Add(new Data.DataModels.Procedure() { ProcedureId = 1 });
            context.Procedures.Add(new Data.DataModels.Procedure() { ProcedureId = 2 });

            await context.SaveChangesAsync();

            var handler = new AssignUserToPlanProcedure(context);
            var request = new AssignUserCommand()
            {
                PlanId = planId,
                ProcedureId = procedureId,
                UserId = userId
            };
            //When
            var result = await handler.Handle(request, new CancellationToken());
            //Then
            result.Exception.Should().BeOfType(typeof(NotFoundException));
            result.Succeeded.Should().BeFalse();
        }


        [TestMethod]
        [DataRow(1, 1, 1)]
        [DataRow(1, 1, 2)]
        [DataRow(1, 2, 1)]
        public async Task AssignUserToPlanProcedureTests_UserAlreadyAssigned_ReturnsSuccess(int userId, int planId, int procedureId)
        {
            //Given
            var context = DbContextHelper.CreateContext();
            context.Users.Add(new Data.DataModels.User() { UserId = 1 });
            context.Plans.Add(new Data.DataModels.Plan() { PlanId = 1 });
            context.Plans.Add(new Data.DataModels.Plan() { PlanId = 2 });
            context.Procedures.Add(new Data.DataModels.Procedure() { ProcedureId = 1 });
            context.Procedures.Add(new Data.DataModels.Procedure() { ProcedureId = 2 });
            context.PlanProcedures.Add(new Data.DataModels.PlanProcedure() { PlanId = 1, ProcedureId = 1 });
            context.PlanProcedures.Add(new Data.DataModels.PlanProcedure() { PlanId = 1, ProcedureId = 2 });
            context.PlanProcedures.Add(new Data.DataModels.PlanProcedure() { PlanId = 2, ProcedureId = 1 });
            context.AssignedUsers.Add(new Data.DataModels.AssignedUser() { UserId = 1, PlanId=1,ProcedureId=1 });
            context.AssignedUsers.Add(new Data.DataModels.AssignedUser() { UserId = 1, PlanId = 1, ProcedureId = 2 });
            context.AssignedUsers.Add(new Data.DataModels.AssignedUser() { UserId = 1, PlanId = 2, ProcedureId = 1 });
            await context.SaveChangesAsync();

            var handler = new AssignUserToPlanProcedure(context);
            var request = new AssignUserCommand()
            {
                PlanId = planId,
                ProcedureId = procedureId,
                UserId = userId
            };
            //When
            var result = await handler.Handle(request, new CancellationToken());
            //Then
            result.Value.Should().BeOfType(typeof(Unit));
            result.Succeeded.Should().BeTrue();
        }

        [TestMethod]
        [DataRow(1, 1, 1)]
        [DataRow(1, 1, 2)]
        [DataRow(1, 2, 1)]
        public async Task AssignUserToPlanProcedureTests_UserAssigned_ReturnsSuccess(int userId, int planId, int procedureId)
        {
            //Given
            var context = DbContextHelper.CreateContext();
            context.Users.Add(new Data.DataModels.User() { UserId = 1 });
            context.Plans.Add(new Data.DataModels.Plan() { PlanId = 1 });
            context.Plans.Add(new Data.DataModels.Plan() { PlanId = 2 });
            context.Procedures.Add(new Data.DataModels.Procedure() { ProcedureId = 1 });
            context.Procedures.Add(new Data.DataModels.Procedure() { ProcedureId = 2 });
            context.PlanProcedures.Add(new Data.DataModels.PlanProcedure() { PlanId = 1, ProcedureId = 1 });
            context.PlanProcedures.Add(new Data.DataModels.PlanProcedure() { PlanId = 1, ProcedureId = 2 });
            context.PlanProcedures.Add(new Data.DataModels.PlanProcedure() { PlanId = 2, ProcedureId = 1 });
            await context.SaveChangesAsync();

            var handler = new AssignUserToPlanProcedure(context);
            var request = new AssignUserCommand()
            {
                PlanId = planId,
                ProcedureId = procedureId,
                UserId = userId
            };
            //When
            var result = await handler.Handle(request, new CancellationToken());
            //Then
            result.Value.Should().BeOfType(typeof(Unit));
            result.Succeeded.Should().BeTrue();
        }



    }
}
