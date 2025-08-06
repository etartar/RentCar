using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentCarServer.Application.Services;
using RentCarServer.Domain.Abstractions;
using RentCarServer.Domain.Branches;
using RentCarServer.Domain.LoginTokens;
using RentCarServer.Domain.Roles;
using RentCarServer.Domain.Users;
using RentCarServer.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace RentCarServer.Infrastructure.Services;

internal sealed class JwtProvider(
    IOptions<JwtOptions> jwtOptions,
    ILoginTokenRepository loginTokenRepository,
    IRoleRepository roleRepository,
    IBranchRepository branchRepository,
    IUnitOfWork unitOfWork) : IJwtProvider
{
    public async Task<string> CreateTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        Role role = await roleRepository.FirstOrDefaultAsync(x => x.Id == user.RoleId, cancellationToken);

        Branch branch = await branchRepository.FirstOrDefaultAsync(x => x.Id == user.BranchId, cancellationToken);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("fullName", user.FirstName.Value + " " + user.LastName.Value),
            new Claim("fullNameWithEmail", user.FullName.Value),
            new Claim("email", user.Email.Value),
            new Claim("role", role?.Name.Value ?? string.Empty),
            new Claim("permissions", role is null ? "" : JsonSerializer.Serialize(role.Permissions.Select(r => r.Value).ToArray())),
            new Claim("branch", branch?.Name.Value ?? string.Empty),
            new Claim("branchId", user.BranchId.ToString()),
        };

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

        DateTime expires = DateTime.Now.AddDays(1);

        JwtSecurityToken securityToken = new(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: signingCredentials);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(securityToken);

        #region Login Token Operations

        LoginToken loginToken = LoginToken.Create(
            userId: user.Id,
            token: token,
            expiresDate: expires);

        var loginTokens = await loginTokenRepository
            .Where(x => x.UserId == user.Id && x.IsActive.Value == true)
            .ToListAsync(cancellationToken);

        foreach (var existingToken in loginTokens)
        {
            existingToken.SetIsActive(new IsActive(false));
        }

        loginTokenRepository.UpdateRange(loginTokens);

        await loginTokenRepository.AddAsync(loginToken, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        #endregion

        return token;
    }
}
