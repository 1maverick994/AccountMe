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

    [Trait("Service", "UpsertPositionHolding")]
    [Collection("Repository collection")]
    public class UpsertTest
    {
        private readonly UpsertHandler _testee;
        private readonly Repository _PositionHoldingRepository;

        public UpsertTest(RepositoryFixture repository)
        {
            _PositionHoldingRepository = repository.Repository;
            _testee = new UpsertHandler(_PositionHoldingRepository);
        }


        [Fact(DisplayName = "Insert")]
        public async void Handle_Insert()
        {
            //Arrange
            var t = new Models.PositionHolding()
            {
                Quota = 30
            };
            var cmd = new UpsertCommand() { PositionHolding = t };
            var count = _PositionHoldingRepository.GetAllAsync(typeof(Models.PositionHolding)).Result.Count();

            //Act
            var ret = await _testee.Handle(cmd, CancellationToken.None);

            //Assert
            Assert.Same(t, ret);
            Assert.Equal(count + 1, _PositionHoldingRepository.GetAllAsync(typeof(Models.PositionHolding)).Result.Count());

        }

        [Fact(DisplayName = "Update")]
        public async void Handle_Update()
        {
            //Arrange
            var t1 = new Models.PositionHolding()
            {
                Id = 1,
                Quota = 30
            };

            var cmd1 = new UpsertCommand() { PositionHolding = t1 };

            var ret = await _testee.Handle(cmd1, CancellationToken.None);

            var count = _PositionHoldingRepository.GetAllAsync(typeof(Models.PositionHolding)).Result.Count();

            decimal newQuota = 70;
            var t2 = new Models.PositionHolding()
            {
                Id = 1,
                Quota = newQuota,
            };
            var cmd2 = new UpsertCommand() { PositionHolding = t2 };

            //Act
            var ret2 = await _testee.Handle(cmd2, CancellationToken.None);

            //Assert
            Assert.Equal(ret2.Quota, newQuota);
            Assert.Equal(count, _PositionHoldingRepository.GetAllAsync(typeof(Models.PositionHolding)).Result.Count());

        }

    }
}
