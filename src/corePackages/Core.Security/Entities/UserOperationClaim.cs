﻿using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class UserOperationClaim : Entity
{
    public Guid UserId { get; set; }

    public Guid OperationClaimId { get; set; }

    public virtual User User { get; set; }

    public virtual OperationClaim OperationClaim { get; set; }

    public UserOperationClaim()
    {
    }

    public UserOperationClaim(Guid id, Guid userId, Guid operationClaimId) : base(id)
    {
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}