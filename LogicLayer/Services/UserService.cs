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

namespace LogicLayer.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }


        public void AddUser(User user)
        {
            ValidationHelper.ValidateEntity(user);

            try
            {
                _userRepository.Add(user);
                _unitOfWork.Save();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUser(User user)
        {
            ValidationHelper.ValidateEntity(user);

            try
            {
                _userRepository.Update(user);
                _unitOfWork.Save();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteUser(int userId)
        {
            User user = _userRepository.GetById(userId);

            if (user == null)
            {
                throw new NotFoundException(typeof(User));
            }

            try
            {
                _userRepository.Delete(user);
                _unitOfWork.Save();
            }
            catch
            {
                throw;
            }
        }

        public User GetUserById(int userId)
        {
            User user = _userRepository.GetById(userId);
            if (user == null)
            {
                throw new NotFoundException(typeof(User));
            }
            return user;
        }

        public List<User> GetAllUsers(int PageNumber,int RowsPerPage)
        {
            return _userRepository.GetAll(PageNumber,RowsPerPage);
        }
    }
}
