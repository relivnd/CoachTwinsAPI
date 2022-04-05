using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;

namespace CoachTwinsAPI.Logic.Matching
{
    public class AuthService
    {
        private IAuthRepository _repo;

        public AuthService(IAuthRepository repo)
        {
            _repo = repo;
        }
    }
}