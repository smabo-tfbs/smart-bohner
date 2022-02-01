using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.Web.Shared;

namespace SmartBohner.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService maintenanceService;
        private readonly ISpecialActionService specialActionService;

        public MaintenanceController(
            IMaintenanceService maintenanceService,
            ISpecialActionService specialActionService)
        {
            this.maintenanceService = maintenanceService;
            this.specialActionService = specialActionService;
        }

        public async Task<MaintenanceInfo> Index()
        {
            return new()
            {
                HasAlarm = await maintenanceService.Alarm(),
                HasNoBeans = await maintenanceService.NoBeans(),
                HasNoWater = await maintenanceService.NoWater(),
                HasWasteFull = await maintenanceService.WasteFull(),
                NeedsCalcClean = await maintenanceService.NeedsCalcClean(),
            };
        }

        [HttpPost]
        [Route("aromaplus")]
        public Task ExecuteAromaPlus(bool newValue)
        {
            return specialActionService.ToggleAromaPlus(newValue);
        }

        [HttpPost, Route("steam")]
        public Task ExecuteSteam()
        {
            return specialActionService.ExecuteSteam();
        }

        [HttpPost, Route("hotwater")]
        public Task ExecuteHotWater()
        {
            return specialActionService.ExecuteHotWater();
        }

        [HttpPost, Route("calcclean")]
        public Task ExecuteCalcClean()
        {
            return specialActionService.ExecuteCalcClean();
        }
    }
}
