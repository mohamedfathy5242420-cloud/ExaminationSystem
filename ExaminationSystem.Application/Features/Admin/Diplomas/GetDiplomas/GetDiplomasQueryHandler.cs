using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.GetDiplomas.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Domain.Entities.Learning;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.GetDiplomas;

public sealed class GetDiplomasQueryHandler
    : IRequestHandler<GetDiplomasQuery, Result<IReadOnlyList<DiplomaListItemViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDiplomasQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<IReadOnlyList<DiplomaListItemViewModel>>> Handle(
        GetDiplomasQuery query,
        CancellationToken cancellationToken)
    {
        var diplomas = _unitOfWork.Repository<Diploma>()
            .Query()
            .OrderByDescending(x => x.CreatedOnUtc)
            .Select(x => new DiplomaListItemViewModel(
                x.Id,
                x.Title,
                x.Description,
                x.InstructorId,
                x.IsPublished,
                x.CreatedOnUtc))
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<DiplomaListItemViewModel>>.Success(diplomas));
    }
}
