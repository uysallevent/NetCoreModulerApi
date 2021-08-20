using AuthModule.Dto;
using AuthModule.Dtos;
using AuthModule.Interfaces;
using AuthModule.Models;
using AuthModule.Security.Hashing;
using AuthModule.Security.JWT;
using AuthModule.Validations;
using BaseModule.Business;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthModule.Business
{
    public class AuthBusinessBase : BusinessBase<UserAccount>, IAuthBusinessBase
    {
        private readonly IUserAccountDal _userAccountDal;
        private readonly IUserTokenDal _userTokenDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly IConfiguration _configuration;

        public AuthBusinessBase(
            IUserAccountDal userAccountDal,
            IUserTokenDal userTokenDal,
            ITokenHelper tokenHelper,
            IConfiguration configuration) : base(userAccountDal)
        {
            _userAccountDal = userAccountDal;
            _userTokenDal = userTokenDal;
            _tokenHelper = tokenHelper;
            _configuration = configuration;
        }

        private AccessToken CreateAccessToken(UserAccount user, List<OperationClaim> list)
        {
            var accessToken = _tokenHelper.CreateToken(user, list);
            return accessToken;
        }

        [ValidationAspect(typeof(UserAccountValidationRules))]
        public override async Task<IDataResult<UserAccount>> InsertAsync(UserAccount entity)
        {
            var userCheck = await _userAccountDal.GetAsync(x => x.Username == entity.Username);
            if (userCheck != null)
            {
                throw new Exception("This username already exists in the system");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(entity.Password, out passwordHash, out passwordSalt);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
            var result = await _userAccountDal.AddAsync(entity);
            return new SuccessDataResult<UserAccount>(result);
        }

        public async Task<IDataResult<AccessToken>> Login(LoginDto loginDto)
        {
            var user = await _userAccountDal.GetAsync(x => x.Username == loginDto.Username);
            if (user == null)
            {
                throw new Exception("Username could not be found");
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password.Trim(), user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Incorrect password !!");
            }

            var accessToken = CreateAccessToken(user, new List<OperationClaim> { new OperationClaim { Id = user.Id, Name = user.Username } });
            _ = _userTokenDal.DisableActiveRecord(user.Id);
            var sqlServerDatetime = _userTokenDal.GetSqlServerUtcNow();
            int.TryParse(_configuration.GetSection("TokenOptions.RefreshTokenExpiration").Value, out int expirationDate);
            _ = await _userTokenDal.AddAsync(
                new UserToken
                {
                    UserId = user.Id,
                    RefreshTokenExpirationDate = sqlServerDatetime.AddMinutes(expirationDate),
                    RefreshToken = accessToken.RefreshToken,
                    Status = 1
                });

            return new SuccessDataResult<AccessToken>(
                new AccessToken
                {
                    Token = accessToken.Token,
                    Expiration = accessToken.Expiration,
                    RefreshToken = accessToken.RefreshToken
                }
                );
        }

        public IDataResult<AccessToken> RefreshTokenLogin(int userId, string refreshToken)
        {
            AccessToken accessToken;
            var userToken = _userTokenDal.GetUserAndRefreshToken(userId, refreshToken);
            var sqlServerDatetime = _userTokenDal.GetSqlServerUtcNow();
            if (userToken != null && userToken.RefreshTokenExpirationDate < sqlServerDatetime)
            {
                accessToken = CreateAccessToken(userToken.UserAccount, new List<OperationClaim> { new OperationClaim { Id = userToken.UserAccount.Id, Name = userToken.UserAccount.Username } });
            }
            else
            {
                return new ErrorDataResult<AccessToken>("Refresh token has expired");
            }

            return new SuccessDataResult<AccessToken>(accessToken);
        }
    }
}