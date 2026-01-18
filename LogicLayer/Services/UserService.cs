using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Validation.Exceptions;
using LogicLayer.Validation;
using DataAccessLayer.Validation;
using System.Linq.Expressions;
using LogicLayer.DTOs.UserDTO;

namespace LogicLayer.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        #region Map
        private UserReadDto MapUser_ReadDto(User user)
        {
            return new UserReadDto()
            {
                IsActive = user.IsActive,
                Permissions = user.Permissions,
                UserId = user.UserId,
                UserName = user.Username
            };
        }
        #endregion

        /// <exception cref="NotFoundException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="WrongPasswordException">
        /// Thrown when the provided Password is Wrong.
        public async Task<UserReadDto> ValidateUserCredentialsAsync(string UserName, string Password)
        {
            User? user = await _userRepository.GetByUserNameAsync(UserName);

            if (user == null)
            {
                throw new NotFoundException(typeof(User));
            }

            if (user.Password != Password)
            {
                throw new WrongPasswordException();
            }

            return MapUser_ReadDto(user);
        }

    }
}
