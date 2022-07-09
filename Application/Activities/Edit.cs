using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
            private readonly IMapper _mapper;

            public Handler(DataContext conetxt, IMapper mapper)
            {
                _conetxt = conetxt;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _conetxt.Activities.FindAsync(request.Activity.Id);
                _mapper.Map(request.Activity, activity);
                await _conetxt.SaveChangesAsync();
                return Unit.Value;

            }
        }
    }
}