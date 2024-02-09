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

    [Xunit.TraitAttribute("Service", "UpsertAccount")]
    public class UpsertTest
    {
        private readonly UpsertHandler _testee;
        private readonly Repository _AccountRepository;

        public UpsertTest()
        {
            _AccountRepository = new Repository();
            _testee = new UpsertHandler(_AccountRepository);
        }

        
        [Fact(DisplayName = "Insert")]
        public async void Handle_Insert()
        {
            //Arrange
            var t = new AccountMe.Models.Position()
            {
                Name = "Test",
            };
            var cmd = new UpsertCommand() { Position = t };
            var count = _AccountRepository.GetAllAsync().Result.Count();

            //Act
            var ret = await _testee.Handle(cmd, CancellationToken.None);

            //Assert
            Assert.Same(t, ret);
            Assert.Equal(count + 1, _AccountRepository.GetAllAsync().Result.Count());
            
        }

        [Fact(DisplayName = "Update")]
        public async void Handle_Update()
        {
            //Arrange
            var t1 = new AccountMe.Models.Position()
            {
                Id = 1,
                Name = "Test",
            };
            
            var cmd1 = new UpsertCommand() { Position = t1 };

            var ret = await _testee.Handle(cmd1, CancellationToken.None);

            var count = _AccountRepository.GetAllAsync().Result.Count();

            string newName = "testAggiornato";
            var t2 = new AccountMe.Models.Position()
            {
                Id = 1,
                Name = newName,
            };
            var cmd2 = new UpsertCommand() { Position = t2 };

            //Act
            var ret2 = await _testee.Handle(cmd2, CancellationToken.None);  

            //Assert
            Assert.Same(ret2.Name, newName);
            Assert.Equal(count, _AccountRepository.GetAllAsync().Result.Count());

        }

    }
}
