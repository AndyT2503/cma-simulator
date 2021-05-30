using CMA_Simulator.Dto;
using CMA_Simulator.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMA_Simulator.Controllers
{
    [Route("containers")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly MainDbContext _mainDbContext;

        public ContainerController(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateContainer(IEnumerable<ContainerDto> containerList)
        {
            foreach (var container in containerList)
            {
                var newContainer = new Container
                {
                    LeaseType = container.LeaseType,
                    ContainerNumber = container.ContainerNumber,
                    BuildYearAndCountry = container.BuildYearAndCountry,
                    CreditTermsRule = container.CreditTermsRule,
                    EquipmentSizeType = container.EquipmentSizeType,
                    LasEventDateTime = container.LasEventDateTime,
                    LastEvent = container.LastEvent
                };
                await _mainDbContext.AddAsync(newContainer);
            }
            await _mainDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllContainer()
        {
            var listContainer = await _mainDbContext.Containers.Select(x => new ContainerDto
            {
                BuildYearAndCountry = x.BuildYearAndCountry,
                ContainerNumber = x.ContainerNumber,
                CreditTermsRule = x.CreditTermsRule,
                EquipmentSizeType = x.EquipmentSizeType,
                LasEventDateTime = x.LasEventDateTime,
                LastEvent = x.LastEvent,
                LeaseType = x.LeaseType
            }).ToListAsync();
            return Ok(listContainer);
            
        }

        [HttpGet("{containerNumber}")]
        public async Task<IActionResult> GetContainerInfor(string containerNumber)
        {
            var container = await _mainDbContext.Containers.FirstOrDefaultAsync(x => x.ContainerNumber == containerNumber);
            if(container is null)
            {
                throw new Exception($"Bad request parameters: The container {containerNumber} doesn't exist");
            }
            var result = new ContainerResponseDto
            {
                BuildYearAndCountry = container.BuildYearAndCountry,
                CreditTermsRule = container.CreditTermsRule,
                EquipmentSizeType = container.EquipmentSizeType,
                LasEventDateTime = container.LasEventDateTime,
                LastEvent = container.LastEvent,
                LeaseType = container.LeaseType
            };
            return Ok(result);
        }

        [HttpPut("{containerNumber}")]
        public async Task<IActionResult> UpdateContainerLastEvent(string containerNumber, ContainerUpdateDto input)
        {
            var container = await _mainDbContext.Containers.FirstOrDefaultAsync(x => x.ContainerNumber == containerNumber);
            if (container is null)
            {
                throw new Exception($"Bad request parameters: The container {containerNumber} doesn't exist");
            }
            if(!string.IsNullOrEmpty(input.LastEvent))
            {
                container.LastEvent = input.LastEvent;
            }
            if(input.CreditTermsRule is not null)
            {
                container.CreditTermsRule = input.CreditTermsRule;
            }
            await _mainDbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("{containerNumber}/reuse")]
        public async Task CreateReuse(string containerNumber,CreateReuseDto input)
        {
            var container = await _mainDbContext.Containers.FirstOrDefaultAsync(x => x.ContainerNumber == containerNumber);
            if (container is null)
            {
                throw new Exception("Container doesn't exist");
            }
            var bookingContainsCont = await _mainDbContext.Shipments.Include(x => x.CargoEquipments)
                            .FirstOrDefaultAsync(x => x.CargoEquipments.Any(i => i.AssignedContainerNumber == container.ContainerNumber) &&
                                                      x.Destination.Substring(0, 2) != "VN");
            if(bookingContainsCont is not null)
            {
                throw new Exception("Container is assign to another booking");
            }
            var bookingAssignCont = await _mainDbContext.Shipments.FirstOrDefaultAsync(x => x.ShipmentId == input.ShipmentReference && x.Destination == input.Destination);
            if (bookingAssignCont.Destination.Substring(0, 2) == "VN")
            {
                throw new Exception("Shipment Reference is not a booking");
            }
            if (bookingAssignCont is null)
            {
                throw new Exception("Booking doesn't exist");
            }
            var cargoEquipmentAssginCont = await _mainDbContext.CargoEquipments
                                .FirstOrDefaultAsync(x => x.ShipmentId == bookingAssignCont.ShipmentId
                                                          && x.EquipmentSizeType == container.EquipmentSizeType && String.IsNullOrEmpty(x.AssignedContainerNumber));
            if(cargoEquipmentAssginCont is null)
            {
                throw new Exception("Booking is full slot");
            }
            cargoEquipmentAssginCont.AssignedContainerNumber = containerNumber;
            await _mainDbContext.SaveChangesAsync();
        }
    }
}
