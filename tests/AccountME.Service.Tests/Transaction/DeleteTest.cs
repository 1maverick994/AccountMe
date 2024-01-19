using AccountMe;
using AccountMe.Service.Transaction.Commands;
using AccountMe.Service.Transaction.Handlers;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountME.Service.Tests.Transaction
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
            var t1 = new AccountMe.Models.Transaction()
            {
                Id = key,
                Amount = 55,
            };

            var cmd0 = new UpsertCommand() { Transaction = t1 };
            await _upsertHandler.Handle(cmd0, CancellationToken.None);
            var count = _AccountRepository.GetAllAsync().Result.Count();

            var cmd1 = new DeleteCommand() { Transaction = t1 };

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
            var t1 = new AccountMe.Models.Transaction()
            {
                Id = key,
                Amount = 55,
            };
            
            var count = _AccountRepository.GetAllAsync().Result.Count();

            var cmd1 = new DeleteCommand() { Transaction = t1 };

            //Act e Assert
            var act = _testee.Handle(cmd1, CancellationToken.None);

            // Assert
            await Assert.ThrowsAnyAsync<Exception>(()=> act);
            
        }

    }
}
