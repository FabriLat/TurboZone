﻿using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationServiceOptions _options;
        public AuthenticationService(IUserRepository userRepository, IOptions<AuthenticationServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;   
        }

        public User? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password))
            {
                return null;
            }

            var user = _userRepository.GetByEmail(authenticationRequest.Email);
            if (user == null) return null;


            //si es algun rol definido se verifica la contraseña sino no.
            if (user.Rol == UserRol.Client || user.Rol == UserRol.Moderator || user.Rol == UserRol.SysAdmin)
            {
                if (user.Password == authenticationRequest.Password) return user;

            }
            return null;
        }


        public string Authenticate(AuthenticationRequest authenticationRequest)
        {
            var user = ValidateUser(authenticationRequest) ?? throw new NotFoundException("not found");

            //Se secreto se guarda en una variable
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));

            //El secreto se hashea
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            //Se crean los Claims
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("fullName", user.FullName.ToString()));
            claimsForToken.Add(new Claim("role", user.Rol.ToString()));
            claimsForToken.Add(new Claim("state", user.State.ToString()));
         

            //Se crea el jwt
            var jwtSecurityToken = new JwtSecurityToken(
                _options.Issuer, //quien lo creo.
                _options.Audience, //a quien va dirigido.
                claimsForToken, //el claim.
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1), //tiempo de vida del token.
                credentials); //El secreto hasheado.

            //se crea el token de seguridad con el jwt
            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            //se retorna el jwt como string
            return tokenToReturn.ToString();
        }

        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
            

    }
}
