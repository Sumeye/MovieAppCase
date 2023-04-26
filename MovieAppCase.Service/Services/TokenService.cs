using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieAppCase.Core.Configuration;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using MovieAppCase.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(IOptions<CustomTokenOption> options, UserManager<UserApp> userManager)
        {
            _tokenOption = options.Value;
            _userManager = userManager;
        }

        private string CreateRefrehToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        /// <summary>
        /// Üyelik sistemi gerektiren işlemlerin payloadında olması istenen alanlar dolduruldu.
        /// </summary>
        /// <param name="userApp"></param>
        /// <param name="audiences"></param>
        /// <returns></returns>
        private IEnumerable<Claim> GetClaims(UserApp userApp, List<string> audiences)
        {
            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
                new Claim(ClaimTypes.Name,userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;
        }
        /// <summary>
        /// üyelik sistemi gerektirmeyen işlemler de çağrılacak.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString());
            return claims;
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            var accesTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                //tokenı yayınlayan
                issuer: _tokenOption.Issuer,
                //token ömrü,
                expires: accesTokenExpiration,
                notBefore: DateTime.Now,
                //token ömrü,
                claims: GetClaims(userApp, _tokenOption.Audience),
                //token imzası
                signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefrehToken(),
                AccessTokenExpiration = accesTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpration
            };
            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accesTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                //tokenı yayınlayan
                issuer: _tokenOption.Issuer,
                //token ömrü,
                expires: accesTokenExpiration,
                notBefore: DateTime.Now,
                //token ömrü,
                claims: GetClaimsByClient(client),
                //token imzası
                signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accesTokenExpiration,
            };
            return tokenDto;
        }
    }
}

