using System.Security.Claims;
using System;
using V___Medicals.Data;

namespace V___Medicals.Services.Implementation
{
    public class BaseService : IBaseService
    {
        private readonly IHttpContextAccessor httpContext;
        private ClaimsPrincipal _user;
        private readonly ApplicationDbContext dbContext;
        //private readonly IMapper mapper;

        public BaseService(
            ApplicationDbContext dbContext,
            IHttpContextAccessor httpContext)
        {
            this.dbContext = dbContext;
            this.httpContext = httpContext;
            //this.mapper = mapper;

            if (httpContext != null &&
                httpContext.HttpContext != null &&
                httpContext.HttpContext.User != null)
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                _user = httpContext.HttpContext?.User;
#pragma warning restore CS8601 // Possible null reference assignment.
            }
        }

        public ClaimsPrincipal User => _user;
        public ApplicationDbContext _dbContext => dbContext;
        public IHttpContextAccessor _httpContext => httpContext;
    }
}

