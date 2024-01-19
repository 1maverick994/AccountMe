using AccountMe;
using AccountMe.Service.Category.Commands;
using AccountMe.Service.Category.Handlers;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountME.Service.Tests.Category
{

    [Xunit.TraitAttribute("Service", "DeleteCategory")]
    public class DeleteTest
    {
        private readonly DeleteHandler _testee;
        private readonly UpsertHandler _upsertHandler;
        private readonly Repository _CategoryRepository;

        public DeleteTest()
        {
            _CategoryRepository = new Repository();

            _testee = new DeleteHandler(_CategoryRepository);
            _upsertHandler = new UpsertHandler(_CategoryRepository);
        }


        [Fact(DisplayName = "DeleteExisting")]
        public async void Handle_DeleteExisting()
        {
            int key = 1;
            //Arrange
            var t1 = new AccountMe.Models.Category()
            {
                Id = key,
                Name = "Test",
            };

            var cmd0 = new UpsertCommand() { Category = t1 };
            await _upsertHandler.Handle(cmd0, CancellationToken.None);
            var count = _CategoryRepository.GetAllAsync().Result.Count();

            var cmd1 = new DeleteCommand() { Category = t1 };

            //Act
            await _testee.Handle(cmd1, CancellationToken.None);

            //Assert            
            Assert.Equal(count - 1, _CategoryRepository.GetAllAsync().Result.Count());
            Assert.Null(_CategoryRepository.GetByKey(key)?.Result);

        }

        [Fact(DisplayName = "DeleteUnexisting")]
        public async void Handle_DeleteUnexisting()
        {
            int key = 1;
            //Arrange
            var t1 = new AccountMe.Models.Category()
            {
                Id = key,
                Name = "Test",
            };
            
            var count = _CategoryRepository.GetAllAsync().Result.Count();

            var cmd1 = new DeleteCommand() { Category = t1 };

            //Act e Assert
            var act = _testee.Handle(cmd1, CancellationToken.None);

            // Assert
            await Assert.ThrowsAnyAsync<Exception>(()=> act);
            
        }

    }
}
