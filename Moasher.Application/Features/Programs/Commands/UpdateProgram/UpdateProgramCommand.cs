using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Events.Programs;
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
    public UpdateProgramCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        var hasEvent = request.Name != program.Name;
        
        
        _mapper.Map(request, program);
        if (hasEvent)
        {
            program.AddDomainEvent(new ProgramUpdatedEvent(program));
        }
        _context.Programs.Update(program);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<ProgramDto>(program);
    }
}