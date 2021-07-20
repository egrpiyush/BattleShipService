using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.AddBattleShip;
using Application.Commands.Attack;
using Application.Commands.CreateBoard;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestApi.Controllers
{
    //[ApiController]
    public class BattleShipController : ApiControllerBase
    {
        private readonly ILogger<BattleShipController> _logger;

        public BattleShipController(ILogger<BattleShipController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<bool> CreateBoard()
        {
            var response = await Mediator.Send(new CreateBoardCommand());
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> AddBattleShip(AddBattleShipCommand request)
        {
            try
            {
                await Mediator.Send(request);
                return new OkObjectResult(true);
            }
            catch (ValidationException ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Attack(AttackCommand request)
        {
            try
            {
                var response = await Mediator.Send(request);
                return new OkObjectResult(response);
            }
            catch (ValidationException ex)
            {
                return new BadRequestResult();
            }
        }
    }
}
