using Moasher.Domain.Entities;

namespace Moasher.Domain.Common.Abstracts;

public abstract class ApprovableDbEntity : AuditableDbEntity
{
    private EditRequest? _editRequest;
    public bool Approved { get; set; }

    public void SetEditRequest(EditRequest editRequest)
    {
        _editRequest = editRequest;
    }

    public bool HasEditRequest()
    {
        return _editRequest != null;
    }

    public EditRequest GetEditRequest()
    {
        return _editRequest!;
    }
}