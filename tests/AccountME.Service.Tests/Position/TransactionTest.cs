﻿using AccountMe;
using AccountMe.Service.Position.Commands;
using AccountMe.Service.Position.Handlers;
using AccountMe.Service.Transaction.Events;
using FakeItEasy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace AccountMe.Service.Tests.Position
{

    [Xunit.TraitAttribute("Position", "Transaction")]
    public class TransactionTest
    {
        private readonly AccountMe.Service.Transaction.Handlers.UpsertHandler _testee;
        private readonly Repository _AccountRepository;
        private readonly IMediator _mediator;

        public TransactionTest()
        {
            _AccountRepository = new Repository();
            _mediator = A.Fake<IMediator>();
            _testee = new AccountMe.Service.Transaction.Handlers.UpsertHandler(_AccountRepository, _mediator);
           
        }


        [Fact(DisplayName = "InsertIncome")]
        public async void Handle_InsertIncome()
        {

            //Arrange
            const decimal p1InitialBalance = 100;
            var p1 = new AccountMe.Models.Position()
            {
                Id = 1,
                Name = "position1",
                Balance = p1InitialBalance
            };

            const decimal eventAmount = 100;
            var eventt = new UpsertDeleteEvent()
            {
                PositionInId = p1.Id,
                Amount = eventAmount,
                TransactionType = TransactionType.Income
            };

            var handler = new TransactionUpsertDeleteHandler(_AccountRepository, _mediator);

            // (A.Captured dovrebbe "catturare" l'argument, ma in realtà rimane null, forse bug della libreria dato che è stato introdotto nella versione 8.1 che sto usando)
            var notWorkingCapturedUpsertCommand = A.Captured<UpsertCommand>();

            var positionUpsertCommands = new List<UpsertCommand>();
            var positionUpsertCall = A.CallTo(() => _mediator.Send(notWorkingCapturedUpsertCommand._, A<CancellationToken>.Ignored))
                .Invokes(cmd =>
                {
                    if (cmd.Arguments != null && cmd.Arguments.Count > 0 && cmd.Arguments.First().GetType() == typeof(UpsertCommand))
                        positionUpsertCommands.Add((UpsertCommand)cmd.Arguments.First());
                });

            // Act
            await _AccountRepository.Insert(p1);
            await handler.Handle(eventt, CancellationToken.None);

            // Assert
            positionUpsertCall.MustHaveHappenedOnceExactly();

            Assert.Single(positionUpsertCommands);
            Assert.Equal(p1InitialBalance + eventAmount, positionUpsertCommands.First().Position.Balance);



        }

        [Fact(DisplayName = "InsertOutcome")]
        public async void Handle_InsertOutcome()
        {

            //Arrange
            const decimal p1InitialBalance = 100;
            var p1 = new AccountMe.Models.Position()
            {
                Id = 2,
                Name = "position2",
                Balance = p1InitialBalance
            };

            const decimal eventAmount = 100;
            var eventt = new UpsertDeleteEvent()
            {
                PositionOutId = p1.Id,
                Amount = eventAmount,
                TransactionType = TransactionType.Outcome
            };

            var handler = new TransactionUpsertDeleteHandler(_AccountRepository, _mediator);

            // (A.Captured dovrebbe "catturare" l'argument, ma in realtà rimane null, forse bug della libreria dato che è stato introdotto nella versione 8.1 che sto usando)
            var notWorkingCapturedUpsertCommand = A.Captured<UpsertCommand>();

            var positionUpsertCommands = new List<UpsertCommand>();
            var positionUpsertCall = A.CallTo(() => _mediator.Send(notWorkingCapturedUpsertCommand._, A<CancellationToken>.Ignored))
                .Invokes(cmd =>
                {
                    if (cmd.Arguments != null && cmd.Arguments.Count > 0 && cmd.Arguments.First().GetType() == typeof(UpsertCommand))
                        positionUpsertCommands.Add((UpsertCommand)cmd.Arguments.First());
                });

            // Act
            await _AccountRepository.Insert(p1);
            await handler.Handle(eventt, CancellationToken.None);

            // Assert
            positionUpsertCall.MustHaveHappenedOnceExactly();

            Assert.Single(positionUpsertCommands);
            Assert.Equal(p1InitialBalance - eventAmount, positionUpsertCommands.First().Position.Balance);



        }

        [Fact(DisplayName = "InsertTransfer")]
        public async void Handle_InsertTransfer()
        {

            //Arrange
            const decimal pInitialBalance = 100;
            var p3 = new AccountMe.Models.Position()
            {
                Id = 3,
                Name = "position3",
                Balance = pInitialBalance
            };

            var p4 = new AccountMe.Models.Position()
            {
                Id = 4,
                Name = "position4",
                Balance = pInitialBalance
            };

            const decimal eventAmount = 100;
            var eventt = new UpsertDeleteEvent()
            {
                PositionOutId = p3.Id,
                PositionInId = p4.Id,
                Amount = eventAmount,
                TransactionType = TransactionType.Transfer
            };

            var handler = new TransactionUpsertDeleteHandler(_AccountRepository, _mediator);

            // (A.Captured dovrebbe "catturare" l'argument, ma in realtà rimane null, forse bug della libreria dato che è stato introdotto nella versione 8.1 che sto usando)
            var notWorkingCapturedUpsertCommand = A.Captured<UpsertCommand>();

            var positionUpsertCommands = new List<UpsertCommand>();
            var positionUpsertCall = A.CallTo(() => _mediator.Send(notWorkingCapturedUpsertCommand._, A<CancellationToken>.Ignored))
                .Invokes(cmd =>
                {
                    if (cmd.Arguments != null && cmd.Arguments.Count > 0 && cmd.Arguments.First().GetType() == typeof(UpsertCommand))
                        positionUpsertCommands.Add((UpsertCommand)cmd.Arguments.First());
                });

            // Act
            await _AccountRepository.Insert(p3);
            await _AccountRepository.Insert(p4);
            await handler.Handle(eventt, CancellationToken.None);

            // Assert
            positionUpsertCall.MustHaveHappenedTwiceExactly();

            Assert.Equal(2, positionUpsertCommands.Count);

            var upsertP3 = positionUpsertCommands.First(us=> us.Position.Id == p3.Id);
            var upsertP4 = positionUpsertCommands.First(us => us.Position.Id == p4.Id);

            Assert.Equal(pInitialBalance - eventAmount, upsertP3.Position.Balance);
            Assert.Equal(pInitialBalance + eventAmount, upsertP4.Position.Balance);



        }



    }
}
