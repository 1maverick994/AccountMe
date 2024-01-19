using AccountMe;
using AccountMe.Service.Tenant.Commands;
using AccountMe.Service.Tenant.Handlers;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountME.Service.Tests.Tenant
{

    [Xunit.TraitAttribute("Service", "UpsertTenant")]
    public class UpsertTest
    {
        private readonly UpsertHandler _testee;
        private readonly Repository _tenantRepository;

        public UpsertTest()
        {
            _tenantRepository = new Repository();
            _testee = new UpsertHandler(_tenantRepository);
        }

        
        [Fact(DisplayName = "Insert")]
        public async void Handle_Insert()
        {
            //Arrange
            var t = new AccountMe.Models.Tenant()
            {
                Name = "Test",
            };
            var cmd = new UpsertCommand() { Tenant = t };
            var count = _tenantRepository.GetAllAsync().Result.Count();

            //Act
            var ret = await _testee.Handle(cmd, CancellationToken.None);

            //Assert
            Assert.Same(t, ret);
            Assert.Equal(count + 1, _tenantRepository.GetAllAsync().Result.Count());
            
        }

        [Fact(DisplayName = "Update")]
        public async void Handle_Update()
        {
            //Arrange
            var t1 = new AccountMe.Models.Tenant()
            {
                Id = 1,
                Name = "Test",
            };
            
            var cmd1 = new UpsertCommand() { Tenant = t1 };

            var ret = await _testee.Handle(cmd1, CancellationToken.None);

            var count = _tenantRepository.GetAllAsync().Result.Count();

            string newName = "testAggiornato";
            var t2 = new AccountMe.Models.Tenant()
            {
                Id = 1,
                Name = newName,
            };
            var cmd2 = new UpsertCommand() { Tenant = t2 };

            //Act
            var ret2 = await _testee.Handle(cmd2, CancellationToken.None);  

            //Assert
            Assert.Same(ret2.Name, newName);
            Assert.Equal(count, _tenantRepository.GetAllAsync().Result.Count());

        }

    }
}
