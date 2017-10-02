using Core.Entities;
using Core.Repository;
using Service.Models;
using Service.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class UserService : BaseService, IUserService
    {
        private IRepository<User> _userRepository;

        public UserService()
        {
            _userRepository = GetRepository<User>();
        }

        public int? CreateUser(UserServiceModel userModel)
        {
            var user = new User
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Avatar = userModel.Avatar,
                BirthDate = userModel.BirthDate,
                Budget = userModel.Budget
            };

            _userRepository.Add(user);
            _userRepository.Complete();

            if (_userRepository.IsError)
            {
                throw new Exception(Resources.TextAbort);
            }

            return user.Id;
        }

        public void DeleteUser(int? Id)
        {
            var user = _userRepository.GetByID(Id);
            if (user == null)
            {
                throw new Exception(Resources.ValidationUserNotFound);
            }
            else
            {
                _userRepository.Remove(user);
                _userRepository.Complete();

                if (_userRepository.IsError)
                {
                    throw new Exception(Resources.TextAbort);
                }
            }
        }

        public IEnumerable<UserServiceModel> GetAllUsers()
        {
            return _userRepository.GetAll(orderBy: ob => ob.OrderByDescending(u => u.CreateTime)).Select(u => new UserServiceModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Avatar = u.Avatar,
                BirthDate = u.BirthDate,
                Budget = u.Budget,
                CreateTime = u.CreateTime
            }).ToList();
        }

        public void UpdateUser(UserServiceModel userModel)
        {
            var user = _userRepository.GetByID(userModel.Id);
            if (user == null)
            {
                throw new Exception(Resources.ValidationUserNotFound);
            }
            else
            {
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.Avatar = userModel.Avatar;
                user.BirthDate = userModel.BirthDate;
                user.Budget = userModel.Budget;

                _userRepository.Update(user);
                _userRepository.Complete();

                if (_userRepository.IsError)
                {
                    throw new Exception(Resources.TextAbort);
                }
            }
        }

        public UserServiceModel GetUserById(int? Id)
        {
            var user = _userRepository.GetByID(Id);
            if (user == null)
            {
                return null;
            }

            return new UserServiceModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                BirthDate = user.BirthDate,
                Budget = user.Budget,
                CreateTime = user.CreateTime
            };
        }
    }
}
