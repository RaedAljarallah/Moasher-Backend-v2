using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moasher.Application.Common.Extensions;
using Moasher.Application.Common.Interfaces;
using Moasher.Domain.Entities;
using Moasher.Domain.Validators;

namespace Moasher.Application.Features.Programs.Commands.CreateProgram;

public record CreateProgramCommand : ProgramCommandBase, IRequest<ProgramDto>
{
}

public class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, ProgramDto>
{
    private readonly IMoasherDbContext _context;
    private readonly IMapper _mapper;

    public CreateProgramCommandHandler(IMoasherDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ProgramDto> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
    {
        var programs = await _context.Programs.AsNoTracking().ToListAsync(cancellationToken);
        request.ValidateAndThrow(new ProgramDomainValidator(programs, request.Name, request.Code));
        var program = _mapper.Map<Program>(request);
        _context.Programs.Add(program);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProgramDto>(program);
    }
}