using System;
using System.Collections.Generic;
using System.Linq;
using ApiMultiTenant.Models;
using ApiMultiTenant.Repository;

namespace ApiMultiTenant.Service
{
    public interface IUserService
    {
        void Add(Users Users);
        Users GetById(int id);
        List<Users> GetAll();
        void Update(Users Users);
        void Delete(Users Users);
    }

    public class UserService : IUserService
    {

        private readonly UnitOfWork _uow;

        public UserService(UnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public void Add(Users Users)
        {
            _uow.UsersRepository.Add(Users);
            _uow.Commit();
        }


        public void Delete(Users Users)
        {
            _uow.UsersRepository.Delete(Users);
            _uow.Commit();
        }

        public List<Users> GetAll()
        {
            List<Users> Userss = _uow.UsersRepository.Get().ToList();
            return Userss;
        }

        public Users GetById(int id)
        {
            Users Users = _uow.UsersRepository.GetByID(id);

            return Users;
        }

       

        public void Update(Users Users)
        {
            _uow.UsersRepository.Update(Users);
            _uow.Commit();
        }
    }
}