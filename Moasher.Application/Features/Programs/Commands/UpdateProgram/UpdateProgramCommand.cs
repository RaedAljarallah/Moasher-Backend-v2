using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Programs.BackgroundJobs;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Programs.Commands.UpdateProgram;

public record UpdateProgramCommand : ProgramCommandBase, IRequest<ProgramDto>
{
    public Guid Id { get; set; }
}

public class UpdateProgramCommandHandler : IRequestHandler<UpdateProgramCommand, ProgramDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;
    private readonly IBackgroundQueue _queue;
    private readonly IProgramUpdatedJob _programUpdatedJob;

    public UpdateProgramCommandHandler(IMoasherDbContext context, IMapper mapper, IBackgroundQueue queue, IProgramUpdatedJob programUpdatedJob)
    {
        _context = context;
        _mapper = mapper;
        _queue = queue;
        _programUpdatedJob = programUpdatedJob;
    }
    
    public async Task<ProgramDto> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
    {
        var programs = await _context.Programs.AsNoTracking().ToListAsync(cancellationToken);
        var program = programs.FirstOrDefault(e => e.Id == request.Id);
        if (program is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new ProgramDomainValidator(programs.Where(e => e.Id != request.Id).ToList(), request.Name, request.Code));
        
        var runBackgroundJob = request.Name != program.Name;
        
        _mapper.Map(request, program);
        _context.Programs.Update(program);
        await _context.SaveChangesAsync(cancellationToken);

        if (runBackgroundJob)
        {
            await _queue.QueueTask(ct => _programUpdatedJob.ExecuteAsync(program.Id, ct));
        }
        return _mapper.Map<ProgramDto>(program);
    }
}