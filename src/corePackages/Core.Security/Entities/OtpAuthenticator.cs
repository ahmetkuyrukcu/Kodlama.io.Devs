﻿using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class OtpAuthenticator : Entity
{
    public Guid UserId { get; set; }

#pragma warning disable CA1819 // Properties should not return arrays
    public byte[] SecretKey { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

    public bool IsVerified { get; set; }

    public virtual User User { get; set; }

    public OtpAuthenticator()
    {
    }

    public OtpAuthenticator(Guid id, Guid userId, byte[] secretKey, bool isVerified) : this()
    {
        Id = id;
        UserId = userId;
        SecretKey = secretKey;
        IsVerified = isVerified;
    }
}