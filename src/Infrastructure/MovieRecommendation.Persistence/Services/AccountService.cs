using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieRecommendation.Application.Constants.Authentication;
using MovieRecommendation.Application.Exceptions;
using MovieRecommendation.Application.Features.Queries.Accounts.Login;
using MovieRecommendation.Application.Helpers;
using MovieRecommendation.Application.Interfaces.Identity;
using MovieRecommendation.Domain.Entities;
using MovieRecommendation.Domain.Models.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MovieRecommendation.Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        public AccountService(UserManager<ApplicationUser> userManager,
            IOptions<JWTSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }
        
        public async Task<LoginQueryResponse> Login(LoginQueryRequest request)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email.Trim());
            if (user == null)
            {
                throw new ApiException($"You are not registered with '{request.Email}'.") { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            if (!user.EmailConfirmed)
            {
                throw new ApiException($"Account Not Confirmed for '{request.Email}'.") { StatusCode = (int)HttpStatusCode.BadRequest };
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
            {
                throw new ApiException($"Invalid Credentials for '{request.Email}'.") { StatusCode = (int)HttpStatusCode.BadRequest };
            }

            string ipAddress = IpHelper.GetIpAddress();
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user, ipAddress);
            LoginQueryResponse response = new LoginQueryResponse();
            response.Id = user.Id.ToString();
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            IList<string> rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task<List<LoginQueryResponse>> GetUsers()
        {
            //return await _userManager.Users.ToListAsync();

            return await _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).Select(x=>new LoginQueryResponse
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Roles = (List<string>)x.UserRoles.Select(x=> x.Role.Name),
                UserName = x.UserName
            }).ToListAsync(); //lazzyloading
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, string ipAddress)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimName.Id, user.Id.ToString()),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
