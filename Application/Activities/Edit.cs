using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _conetxt;

            public Handler(DataContext conetxt)
            {
                _conetxt = conetxt;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _conetxt.Activities.FindAsync(request.Activity.Id);
                activity.Title = request.Activity.Title ?? activity.Title;
                await _conetxt.SaveChangesAsync();
                return Unit.Value;

            }
        }
    }
}