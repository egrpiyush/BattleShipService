using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Application.Commands.AddBattleShip;
using Application.Commands.SharedModels;
using Application.Interfaces;
using AutoFixture;
using FluentValidation;
using Moq;
using Shouldly;
using Xunit;

namespace Application.Test.Commands.AddBattleShip
{
    public class AddBattleShipCommandTests
    {
        private static readonly Fixture Fixture = new Fixture();
        private static readonly CancellationToken CancellationToken = CancellationToken.None;

        [Fact]
        public async void WhenOrientationIsHorizontalWithOriginAsX0Y0AndLengthIs3_BoardShouldHave1BattleShip()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.Orientation, Domain.Entities.Orientation.Horizontal)
                .With(p => p.StartingLocation, new Domain.Entities.Location { X = 0, Y = 0 })
                .With(p => p.Length, 3)
                .Create();
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>())
                .Create();
            var handler = new AddBattleShipCommand.Handler(board);
            //Act
            var result = await handler.Handle(request, CancellationToken);
            //Assert
            board.BattleShips.Count.ShouldBe(1);
        }

        [Fact]
        public async void WhenOrientationIsHorizontalWithOriginAsX0Y0AndLengthIs3_ThereShouldBe3Locations()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.Orientation, Domain.Entities.Orientation.Horizontal)
                .With(p => p.StartingLocation, new Domain.Entities.Location { X = 0, Y = 0 })
                .With(p => p.Length, 3)
                .Create();
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>())
                .Create();
            var handler = new AddBattleShipCommand.Handler(board);
            //Act
            var result = await handler.Handle(request, CancellationToken);
            //Assert
            board.BattleShips.FirstOrDefault().Locations.Count.ShouldBe(3);
        }

        [Fact]
        public async void WhenOrientationIsVerticalWithOriginAsX0Y0AndLengthIs3_BoardShouldHave1BattleShip()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.Orientation, Domain.Entities.Orientation.Vertical)
                .With(p => p.StartingLocation, new Domain.Entities.Location { X = 0, Y = 0 })
                .With(p => p.Length, 3)
                .Create();
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>())
                .Create();
            var handler = new AddBattleShipCommand.Handler(board);
            //Act
            var result = await handler.Handle(request, CancellationToken);
            //Assert
            board.BattleShips.Count.ShouldBe(1);
        }

        [Fact]
        public async void WhenOrientationIsVerticalWithOriginAsX0Y0AndLengthIs3_ThereShouldBe3Locations()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.Orientation, Domain.Entities.Orientation.Vertical)
                .With(p => p.StartingLocation, new Domain.Entities.Location { X = 0, Y = 0 })
                .With(p => p.Length, 3)
                .Create();
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>())
                .Create();
            var handler = new AddBattleShipCommand.Handler(board);
            //Act
            var result = await handler.Handle(request, CancellationToken);
            //Assert
            board.BattleShips.FirstOrDefault().Locations.Count.ShouldBe(3);
        }

        [Fact]
        public async void WhenANewBattleShipIsAddedOverAnExistingOne_ShouldThrowValidationException()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.Orientation, Domain.Entities.Orientation.Horizontal)
                .With(p => p.StartingLocation, new Domain.Entities.Location { X = 0, Y = 0 })
                .With(p => p.Length, 3)
                .Create();
            var existingLocations = new List<Domain.Entities.Location>
            {
                new Domain.Entities.Location { X = 0, Y = 0 },
                new Domain.Entities.Location { X = 0, Y = 1 },
                new Domain.Entities.Location { X = 0, Y = 2 }
            };
            var board = Fixture.Build<Board>()
                .With(p => p.BattleShips, new List<BattleShipModel>
                {
                    Fixture.Build<BattleShipModel>()
                    .With(p => p.Orientation, Domain.Entities.Orientation.Horizontal)
                    .With(p => p.StartingLocation, new Domain.Entities.Location { X = 0, Y = 0 })
                    .With(p => p.Length, 3)
                    .Create()
                })
                .Create();
            board.BattleShips.First().Locations.AddRange(existingLocations);
            var handler = new AddBattleShipCommand.Handler(board);
            //Act & Assert
            await handler.Handle(request, CancellationToken).ShouldThrowAsync<ValidationException>();
        }
    }
}
