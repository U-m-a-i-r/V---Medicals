using System.Security.Claims;

namespace V___Medicals.Services
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));
            //return principal.GetUserId();
            //  return principal.FindFirstValue(ClaimTypes.PrimarySid);
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Email).Trim();
        }
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));
            var username = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (username == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }


            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public static bool IsPatient(this ClaimsPrincipal principal)
        {
            return principal.IsInRole("Patient");
        }
        public static bool IsDoctor(this ClaimsPrincipal principal)
        {
            return principal.IsInRole("Doctor");
        }
        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            return principal.IsInRole("Admin");
        }
        public static string GetName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Name);
        }
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }
    }
}
