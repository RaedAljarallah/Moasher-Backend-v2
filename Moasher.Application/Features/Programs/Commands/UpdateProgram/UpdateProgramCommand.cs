using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Exceptions;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
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
        var entity = programs.FirstOrDefault(e => e.Id == request.Id);
        if (entity is null)
        {
            throw new NotFoundException();
        }
        
        request.ValidateAndThrow(new ProgramDomainValidator(programs.Where(e => e.Id != request.Id).ToList(), request.Name, request.Code));

        _mapper.Map(request, entity);
        _context.Programs.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProgramDto>(entity);
    }
}