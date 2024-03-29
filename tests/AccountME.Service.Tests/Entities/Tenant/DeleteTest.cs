﻿using AccountMe.Service.Tenant.Commands;
using AccountMe.Service.Tenant.Handlers;
using AccountMe.Service.Tests.Fixtures;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Tests.Entities.Tenant
{

    [Trait("Service", "DeleteTenant")]
    [Collection("Repository collection")]
    public class DeleteTest
    {
        private readonly DeleteHandler _testee;
        private readonly UpsertHandler _upsertHandler;
        private readonly Repository _tenantRepository;

        public DeleteTest(RepositoryFixture repository)
        {
            _tenantRepository = repository.Repository;

            _testee = new DeleteHandler(_tenantRepository);
            _upsertHandler = new UpsertHandler(_tenantRepository);
        }


        [Fact(DisplayName = "DeleteExisting")]
        public async void Handle_DeleteExisting()
        {
            int key = 1;
            //Arrange
            var t1 = new Models.Tenant()
            {
                Id = key,
                Name = "Test",
            };

            var cmd0 = new UpsertCommand() { Tenant = t1 };
            await _upsertHandler.Handle(cmd0, CancellationToken.None);
            var count = _tenantRepository.GetAllAsync(typeof(Models.Tenant)).Result.Count();

            var cmd1 = new DeleteCommand() { Tenant = t1 };

            //Act
            await _testee.Handle(cmd1, CancellationToken.None);

            //Assert            
            Assert.Equal(count - 1, _tenantRepository.GetAllAsync(typeof(Models.Tenant)).Result.Count());
            Assert.Null(_tenantRepository.GetByKey(typeof(Models.Tenant), key)?.Result);

        }

        [Fact(DisplayName = "DeleteUnexisting")]
        public async void Handle_DeleteUnexisting()
        {
            int key = 1;
            //Arrange
            var t1 = new Models.Tenant()
            {
                Id = key,
                Name = "Test",
            };

            var count = _tenantRepository.GetAllAsync(typeof(Models.Tenant)).Result.Count();

            var cmd1 = new DeleteCommand() { Tenant = t1 };

            //Act e Assert
            var act = _testee.Handle(cmd1, CancellationToken.None);

            // Assert
            await Assert.ThrowsAnyAsync<Exception>(() => act);

        }

    }
}
