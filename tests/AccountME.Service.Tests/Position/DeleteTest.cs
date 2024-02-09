using AccountMe;
using AccountMe.Service.Position.Commands;
using AccountMe.Service.Position.Handlers;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Tests.Position
{

    [Xunit.TraitAttribute("Service", "DeleteAccount")]
    public class DeleteTest
    {
        private readonly DeleteHandler _testee;
        private readonly UpsertHandler _upsertHandler;
        private readonly Repository _AccountRepository;

        public DeleteTest()
        {
            _AccountRepository = new Repository();

            _testee = new DeleteHandler(_AccountRepository);
            _upsertHandler = new UpsertHandler(_AccountRepository);
        }


        [Fact(DisplayName = "DeleteExisting")]
        public async void Handle_DeleteExisting()
        {
            int key = 1;
            //Arrange
            var t1 = new AccountMe.Models.Position()
            {
                Id = key,
                Name = "Test",
            };

            var cmd0 = new UpsertCommand() { Position = t1 };
            await _upsertHandler.Handle(cmd0, CancellationToken.None);
            var count = _AccountRepository.GetAllAsync().Result.Count();

            var cmd1 = new DeleteCommand() { Position = t1 };

            //Act
            await _testee.Handle(cmd1, CancellationToken.None);

            //Assert            
            Assert.Equal(count - 1, _AccountRepository.GetAllAsync().Result.Count());
            Assert.Null(_AccountRepository.GetByKey(key)?.Result);

        }

        [Fact(DisplayName = "DeleteUnexisting")]
        public async void Handle_DeleteUnexisting()
        {
            int key = 1;
            //Arrange
            var t1 = new AccountMe.Models.Position()
            {
                Id = key,
                Name = "Test",
            };
            
            var count = _AccountRepository.GetAllAsync().Result.Count();

            var cmd1 = new DeleteCommand() { Position = t1 };

            //Act e Assert
            var act = _testee.Handle(cmd1, CancellationToken.None);

            // Assert
            await Assert.ThrowsAnyAsync<Exception>(()=> act);
            
        }

    }
}
