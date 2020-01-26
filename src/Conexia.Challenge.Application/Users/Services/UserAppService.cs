using AutoMapper;
using Conexia.Challenge.Application.Infrastructure.Authorization;
using Conexia.Challenge.Application.Infrastructure.Exceptions;
using Conexia.Challenge.Application.Models;
using Conexia.Challenge.Application.Responses;
using Conexia.Challenge.Application.Users.Services.Interfaces;
using Conexia.Challenge.Domain;
using Conexia.Challenge.Domain.Users;
using Conexia.Challenge.Domain.Users.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Conexia.Challenge.Application.Users.Services
{
    public class UserAppService : IUserAppService
    {
        readonly IUserService _userService;
        readonly IUnitOfWorkFactory _unitOfWorkFactory;
        readonly TokenDescriptor _tokenDescriptor;
        readonly IMapper _mapper;

        public UserAppService(
            IUserService userService,
            IUnitOfWorkFactory unitOfWorkFactory,
            TokenDescriptor tokenDescriptor,
            IMapper mapper)
        {
            _userService = userService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _tokenDescriptor = tokenDescriptor;
            _mapper = mapper;
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            using (_unitOfWorkFactory.StartUnitOfWork())
            {
                var user = await _userService.LoginAsync(email, password);

                if (user == null)
                    throw new InvalidUserException();

                return GetToken(user);
            }
        }

        public async Task<RecoverByIdResponse> RecoverByIdAsync(int id)
        {
            using (_unitOfWorkFactory.StartUnitOfWork())
                return _mapper.Map<RecoverByIdResponse>(
                    await _userService.RecoverByIdAsync(id));
        }

        LoginResponse GetToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });

            var encodedJwt = handler.WriteToken(securityToken);

            return new LoginResponse
            {
                Token = encodedJwt,
                ExpiresIn = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                User = new UserModel
                {
                    Name = user.Name,
                    Email = user.Email
                }
            };
        }
    }
}
