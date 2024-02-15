using AccountMe.Service.Position.Commands;
using AccountMe.Service.Position.Handlers;
using AccountMe.Service.Tests.Fixtures;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Tests.Entities.Position
{

    [Trait("Service", "UpsertAccount")]
    [Collection("Repository collection")]
    public class UpsertTest
    {
        private readonly UpsertHandler _testee;
        private readonly Repository _AccountRepository;

        public UpsertTest(RepositoryFixture repository)
        {
            _AccountRepository = repository.Repository;
            _testee = new UpsertHandler(_AccountRepository);
        }


        [Fact(DisplayName = "Insert")]
        public async void Handle_Insert()
        {
            //Arrange
            var t = new Models.Position()
            {
                Name = "Test",
            };
            var cmd = new UpsertCommand() { Position = t };
            var count = _AccountRepository.GetAllAsync(typeof(Models.Position)).Result.Count();

            //Act
            var ret = await _testee.Handle(cmd, CancellationToken.None);

            //Assert
            Assert.Same(t, ret);
            Assert.Equal(count + 1, _AccountRepository.GetAllAsync(typeof(Models.Position)).Result.Count());

        }

        [Fact(DisplayName = "Update")]
        public async void Handle_Update()
        {
            //Arrange
            var t1 = new Models.Position()
            {
                Id = 1,
                Name = "Test",
            };

            var cmd1 = new UpsertCommand() { Position = t1 };

            var ret = await _testee.Handle(cmd1, CancellationToken.None);

            var count = _AccountRepository.GetAllAsync(typeof(Models.Position)).Result.Count();

            string newName = "testAggiornato";
            var t2 = new Models.Position()
            {
                Id = 1,
                Name = newName,
            };
            var cmd2 = new UpsertCommand() { Position = t2 };

            //Act
            var ret2 = await _testee.Handle(cmd2, CancellationToken.None);

            //Assert
            Assert.Same(ret2.Name, newName);
            Assert.Equal(count, _AccountRepository.GetAllAsync(typeof(Models.Position)).Result.Count());

        }

    }
}
