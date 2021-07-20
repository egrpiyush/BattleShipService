using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Application.Commands.Attack;
using Application.Commands.SharedModels;
using AutoFixture;
using Shouldly;
using Xunit;

namespace Application.Test.Commands.Attack
{
    public class AttackCommandTests
    {
        private static readonly Fixture Fixture = new Fixture();
        private static readonly CancellationToken CancellationToken = CancellationToken.None;

        [Fact]
        public async void WhenThereIsAShipAtAttackLocation_IsHitShouldBeTrue()
        {
            //Arrange
            var request = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 0, Y = 0 })
                .Create();
            var existingLocations = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location { X = 0, Y = 0 }
            };
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>
                {
                    Fixture.Build<BattleShipModel>()
                    .With(p => p.Length, 1)
                    .With(p => p.Hits, 0)
                    .Create()
                })
                .Create();
            board.BattleShips.First().Locations.AddRange(existingLocations);
            var handler = new AttackCommand.Handler(board);
            //Act
            var result = await handler.Handle(request, CancellationToken);
            //Assert
            result.IsAHit.ShouldBeTrue();
        }

        [Fact]
        public async void WhenThereIsAShipAtAttackLocation_HitCountShouldBeCorrect()
        {
            //Arrange
            var request = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 0, Y = 0 })
                .Create();
            var existingLocations = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location { X = 0, Y = 0 }
            };
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>
                {
                    Fixture.Build<BattleShipModel>()
                    .With(p => p.Length, 1)
                    .With(p => p.Hits, 0)
                    .Create()
                })
                .Create();
            board.BattleShips.First().Locations.AddRange(existingLocations);
            var handler = new AttackCommand.Handler(board);
            //Act
            var result = await handler.Handle(request, CancellationToken);
            //Assert
            board.BattleShips.FirstOrDefault().Hits.ShouldBe(1);
        }

        [Fact]
        public async void WhenAllLocationsOfAShipAreHit_IsSunkShouldBeTrue()
        {
            //Arrange
            var requestFirstHit = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 0, Y = 0 })
                .Create();
            var requestSecondHit = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 0, Y = 1 })
                .Create();
            var existingLocations = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location { X = 0, Y = 0 },
                new Domain.Entities.Location { X = 0, Y = 1 }
            };
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>
                {
                    Fixture.Build<BattleShipModel>()
                    .With(p => p.Length, 2)
                    .With(p => p.Hits, 0)
                    .Create()
                })
                .Create();
            board.BattleShips.First().Locations.AddRange(existingLocations);
            var handler = new AttackCommand.Handler(board);
            //Act
            var firstHitResponse = await handler.Handle(requestFirstHit, CancellationToken);
            var secondHitResponse = await handler.Handle(requestSecondHit, CancellationToken);
            //Assert
            board.BattleShips.FirstOrDefault().IsSunk.ShouldBeTrue();
        }

        [Fact]
        public async void WhenAllLocationsOfAShipAreNotHit_IsSunkShouldBeFalse()
        {
            //Arrange
            var requestFirstHit = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 0, Y = 0 })
                .Create();
            var existingLocations = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location { X = 0, Y = 0 },
                new Domain.Entities.Location { X = 0, Y = 1 }
            };
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>
                {
                    Fixture.Build<BattleShipModel>()
                    .With(p => p.Length, 2)
                    .With(p => p.Hits, 0)
                    .Create()
                })
                .Create();
            board.BattleShips.First().Locations.AddRange(existingLocations);
            var handler = new AttackCommand.Handler(board);
            //Act
            var firstHitResponse = await handler.Handle(requestFirstHit, CancellationToken);
            //Assert
            board.BattleShips.FirstOrDefault().IsSunk.ShouldBeFalse();
        }

        [Fact]
        public async void WhenThereIsNoShipAtAttackLocation_IsHitShouldBeFalse()
        {
            //Arrange
            var request = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 0, Y = 1 })
                .Create();
            var existingLocations = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location { X = 0, Y = 0 }
            };
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>
                {
                    Fixture.Build<BattleShipModel>()
                    .With(p => p.Length, 1)
                    .With(p => p.Hits, 0)
                    .Create()
                })
                .Create();
            board.BattleShips.First().Locations.AddRange(existingLocations);
            var handler = new AttackCommand.Handler(board);
            //Act
            var result = await handler.Handle(request, CancellationToken);
            //Assert
            result.IsAHit.ShouldBeFalse();
        }

        [Fact]
        public async void WhenAllShipAreHit_GameShouldBeOver()
        {
            //Arrange
            var requestFirstHit = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 0, Y = 0 })
                .Create();
            var existingLocations = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location { X = 0, Y = 0 },
                new Domain.Entities.Location { X = 0, Y = 1 }
            };
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>
                {
                    Fixture.Build<BattleShipModel>()
                    .With(p => p.Length, 2)
                    .With(p => p.Hits, 2)
                    .Create(),
                    Fixture.Build<BattleShipModel>()
                    .With(p => p.Length, 2)
                    .With(p => p.Hits, 2)
                    .Create()
                })
                .Create();
            var handler = new AttackCommand.Handler(board);
            //Act
            var hitResponse = await handler.Handle(requestFirstHit, CancellationToken);
            //Assert
            hitResponse.IsGameOver.ShouldBeTrue();
        }
    }
}
