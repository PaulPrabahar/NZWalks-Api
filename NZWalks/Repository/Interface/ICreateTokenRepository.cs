using Microsoft.AspNetCore.Identity;

namespace NZWalks.Repository.Interface;

public interface ICreateTokenRepository
{
    string CreateJWTToken(IdentityUser user, List<string> roles);
}
