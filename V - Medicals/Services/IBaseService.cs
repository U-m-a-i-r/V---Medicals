using System.Security.Claims;
using System;
using V___Medicals.Data;

namespace V___Medicals.Services
{
    public interface IBaseService
    {
        ClaimsPrincipal User { get; }
        ApplicationDbContext _dbContext { get; }
        IHttpContextAccessor _httpContext { get; }
    }
}
