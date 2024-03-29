﻿using AccountMe.Service.Tests.Fixtures;
using AccountMe.Service.Transaction.Commands;
using AccountMe.Service.Transaction.Handlers;
using FakeItEasy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Tests.Entities.Transaction
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
            var mediator = new Fake<IMediator>();
            _testee = new UpsertHandler(_AccountRepository, mediator.FakedObject);
        }


        [Fact(DisplayName = "Insert")]
        public async void Handle_Insert()
        {
            //Arrange
            var t = new Models.Transaction()
            {
                Amount = 77,
            };
            var cmd = new UpsertCommand() { Transaction = t };
            var count = _AccountRepository.GetAllAsync(typeof(Models.Transaction)).Result.Count();

            //Act
            var ret = await _testee.Handle(cmd, CancellationToken.None);

            //Assert
            Assert.Same(t, ret);
            Assert.Equal(count + 1, _AccountRepository.GetAllAsync(typeof(Models.Transaction)).Result.Count());

        }

        [Fact(DisplayName = "Update")]
        public async void Handle_Update()
        {
            //Arrange
            var t1 = new Models.Transaction()
            {
                Id = 1,
                Amount = 77,
            };

            var cmd1 = new UpsertCommand() { Transaction = t1 };

            var ret = await _testee.Handle(cmd1, CancellationToken.None);

            var count = _AccountRepository.GetAllAsync(typeof(Models.Transaction)).Result.Count();

            decimal amountAggiornato = 99;
            var t2 = new Models.Transaction()
            {
                Id = 1,
                Amount = amountAggiornato,
            };
            var cmd2 = new UpsertCommand() { Transaction = t2 };

            //Act
            var ret2 = await _testee.Handle(cmd2, CancellationToken.None);

            //Assert
            Assert.Equal(ret2.Amount, amountAggiornato);
            Assert.Equal(count, _AccountRepository.GetAllAsync(typeof(Models.Transaction)).Result.Count());

        }

    }
}
