﻿using AccountMe;
using AccountMe.Service.User.Commands;
using AccountMe.Service.User.Handlers;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountME.Service.Tests.User
{

    [Xunit.TraitAttribute("Service", "DeleteUser")]
    public class DeleteTest
    {
        private readonly DeleteHandler _testee;
        private readonly UpsertHandler _upsertHandler;
        private readonly Repository _UserRepository;

        public DeleteTest()
        {
            _UserRepository = new Repository();

            _testee = new DeleteHandler(_UserRepository);
            _upsertHandler = new UpsertHandler(_UserRepository);
        }


        [Fact(DisplayName = "DeleteExisting")]
        public async void Handle_DeleteExisting()
        {
            int key = 1;
            //Arrange
            var t1 = new AccountMe.Models.User()
            {
                Id = key,
                Username = "Test",
            };

            var cmd0 = new UpsertCommand() { User = t1 };
            await _upsertHandler.Handle(cmd0, CancellationToken.None);
            var count = _UserRepository.GetAllAsync().Result.Count();

            var cmd1 = new DeleteCommand() { User = t1 };

            //Act
            await _testee.Handle(cmd1, CancellationToken.None);

            //Assert            
            Assert.Equal(count - 1, _UserRepository.GetAllAsync().Result.Count());
            Assert.Null(_UserRepository.GetByKey(key)?.Result);

        }

        [Fact(DisplayName = "DeleteUnexisting")]
        public async void Handle_DeleteUnexisting()
        {
            int key = 1;
            //Arrange
            var t1 = new AccountMe.Models.User()
            {
                Id = key,
                Username = "Test",
            };
            
            var count = _UserRepository.GetAllAsync().Result.Count();

            var cmd1 = new DeleteCommand() { User = t1 };

            //Act e Assert
            var act = _testee.Handle(cmd1, CancellationToken.None);

            // Assert
            await Assert.ThrowsAnyAsync<Exception>(()=> act);
            
        }

    }
}
