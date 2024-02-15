using AccountMe.Service.Tests.Fixtures;
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

namespace AccountMe.Service.Tests.Entities.User
{

    [Trait("Service", "UpsertUser")]
    [Collection("Repository collection")]
    public class UpsertTest
    {
        private readonly UpsertHandler _testee;
        private readonly Repository _UserRepository;

        public UpsertTest(RepositoryFixture repository)
        {
            _UserRepository = repository.Repository;
            _testee = new UpsertHandler(_UserRepository);
        }


        [Fact(DisplayName = "Insert")]
        public async void Handle_Insert()
        {
            //Arrange
            var t = new Models.User()
            {
                Username = "Test",
            };
            var cmd = new UpsertCommand() { User = t };
            var count = _UserRepository.GetAllAsync(typeof(Models.User)).Result.Count();

            //Act
            var ret = await _testee.Handle(cmd, CancellationToken.None);

            //Assert
            Assert.Same(t, ret);
            Assert.Equal(count + 1, _UserRepository.GetAllAsync(typeof(Models.User)).Result.Count());

        }

        [Fact(DisplayName = "Update")]
        public async void Handle_Update()
        {
            //Arrange
            var t1 = new Models.User()
            {
                Id = 1,
                Username = "Test",
            };

            var cmd1 = new UpsertCommand() { User = t1 };

            var ret = await _testee.Handle(cmd1, CancellationToken.None);

            var count = _UserRepository.GetAllAsync(typeof(Models.User)).Result.Count();

            string newName = "testAggiornato";
            var t2 = new Models.User()
            {
                Id = 1,
                Username = newName,
            };
            var cmd2 = new UpsertCommand() { User = t2 };

            //Act
            var ret2 = await _testee.Handle(cmd2, CancellationToken.None);

            //Assert
            Assert.Same(ret2.Username, newName);
            Assert.Equal(count, _UserRepository.GetAllAsync(typeof(Models.User)).Result.Count());

        }

    }
}
