using AccountMe.Service.PositionHolding.Commands;
using AccountMe.Service.PositionHolding.Handlers;
using AccountMe.Service.Tests.Fixtures;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Tests.Entities.PositionHolding
{

    [Trait("Service", "DeletePositionHolding")]
    [Collection("Repository collection")]
    public class DeleteTest
    {
        private readonly DeleteHandler _testee;
        private readonly UpsertHandler _upsertHandler;
        private readonly Repository _PositionHoldingRepository;

        public DeleteTest(RepositoryFixture repository)
        {
            _PositionHoldingRepository = repository.Repository;

            _testee = new DeleteHandler(_PositionHoldingRepository);
            _upsertHandler = new UpsertHandler(_PositionHoldingRepository);
        }


        [Fact(DisplayName = "DeleteExisting")]
        public async void Handle_DeleteExisting()
        {
            int key = 1;
            //Arrange
            var t1 = new Models.PositionHolding()
            {
                Id = key,
                Quota = 70,
            };

            var cmd0 = new UpsertCommand() { PositionHolding = t1 };
            await _upsertHandler.Handle(cmd0, CancellationToken.None);
            var count = _PositionHoldingRepository.GetAllAsync(typeof(Models.PositionHolding)).Result.Count();

            var cmd1 = new DeleteCommand() { PositionHolding = t1 };

            //Act
            await _testee.Handle(cmd1, CancellationToken.None);

            //Assert            
            Assert.Equal(count - 1, _PositionHoldingRepository.GetAllAsync(typeof(Models.PositionHolding)).Result.Count());
            Assert.Null(_PositionHoldingRepository.GetByKey(typeof(Models.PositionHolding), key)?.Result);

        }

        [Fact(DisplayName = "DeleteUnexisting")]
        public async void Handle_DeleteUnexisting()
        {
            int key = 1;
            //Arrange
            var t1 = new Models.PositionHolding()
            {
                Id = key,
                Quota = 70,
            };

            var count = _PositionHoldingRepository.GetAllAsync(typeof(Models.PositionHolding)).Result.Count();

            var cmd1 = new DeleteCommand() { PositionHolding = t1 };

            //Act e Assert
            var act = _testee.Handle(cmd1, CancellationToken.None);

            // Assert
            await Assert.ThrowsAnyAsync<Exception>(() => act);

        }

    }
}
