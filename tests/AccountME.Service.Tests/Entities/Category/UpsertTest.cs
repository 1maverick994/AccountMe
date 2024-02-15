using AccountMe.Service.Category.Commands;
using AccountMe.Service.Category.Handlers;
using AccountMe.Service.Tests.Fixtures;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Tests.Entities.Category
{

    [Trait("Service", "UpsertCategory")]
    [Collection("Repository collection")]
    public class UpsertTest
    {
        private readonly UpsertHandler _testee;
        private readonly Repository _CategoryRepository;

        public UpsertTest(RepositoryFixture repository)
        {
            _CategoryRepository = repository.Repository;
            _testee = new UpsertHandler(_CategoryRepository);
        }


        [Fact(DisplayName = "Insert")]
        public async void Handle_Insert()
        {
            //Arrange
            var t = new Models.Category()
            {
                Name = "Test",
            };
            var cmd = new UpsertCommand() { Category = t };
            var count = _CategoryRepository.GetAllAsync(typeof(Models.Category)).Result.Count();

            //Act
            var ret = await _testee.Handle(cmd, CancellationToken.None);

            //Assert
            Assert.Same(t, ret);
            Assert.Equal(count + 1, _CategoryRepository.GetAllAsync(typeof(Models.Category)).Result.Count());

        }

        [Fact(DisplayName = "Update")]
        public async void Handle_Update()
        {
            //Arrange
            var t1 = new Models.Category()
            {
                Id = 1,
                Name = "Test",
            };

            var cmd1 = new UpsertCommand() { Category = t1 };

            var ret = await _testee.Handle(cmd1, CancellationToken.None);

            var count = _CategoryRepository.GetAllAsync(typeof(Models.Category)).Result.Count();

            string newName = "testAggiornato";
            var t2 = new Models.Category()
            {
                Id = 1,
                Name = newName,
            };
            var cmd2 = new UpsertCommand() { Category = t2 };

            //Act
            var ret2 = await _testee.Handle(cmd2, CancellationToken.None);

            //Assert
            Assert.Same(ret2.Name, newName);
            Assert.Equal(count, _CategoryRepository.GetAllAsync(typeof(Models.Category)).Result.Count());

        }

    }
}
