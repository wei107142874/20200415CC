using Basic.Entites;
using Basic.IRepository;
using System;

namespace Basic.Service
{
    public class UserService
    {
        private readonly IRepository<UserEntity> _repository;

        public UserService(IRepository<UserEntity> repository)
        {
            this._repository = repository;
        }

        public long AddUser()
        {
          return  _repository.Add(new UserEntity
            {
                Account = "123",
                FirstName = "三",
                IdCar = "500234199604251821",
                LastName = "张",
                Password = "123456",
                Salt = "111"
            }).Result;
        }
    }

    
}
