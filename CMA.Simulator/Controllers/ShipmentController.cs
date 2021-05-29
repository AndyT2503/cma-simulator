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
    [Route("shipments")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly MainDbContext _mainDbContext;

        public ShipmentController(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipment(IEnumerable<ShipmentDto> shipmentDtos)
        {
            foreach (var shipment in shipmentDtos)
            {
                var newShipment = new Shipment
                {
                    ShipmentId = shipment.ShipmentReference,
                    CargoShipmetId = shipment.CargoShipmetId,
                    ClosingDate = shipment.ClosingDate,
                    ContainerHandlingReference = shipment.ContainerHandlingReference,
                    Destination = shipment.Destination,
                    PolWaypoint = shipment.PolWaypoint,
                    SalingDate = shipment.SalingDate,
                    ShipperOwnedBooking = shipment.ShipperOwnedBooking,
                    Voyage = shipment.Voyage,
                    CargoEquipments = shipment.CargoEquipments.Select(x => new CargoEquipment
                    {
                        AssignedContainerNumber = x.AssignedContainerNumber,
                        Commodity = x.Commodity,
                        EquipmentSizeType = x.EquipmentSizeType,
                        Grade = x.Grade
                    }).ToList()
                };
                _mainDbContext.Add(newShipment);
            }
            await _mainDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{shipmentId}")]
        public async Task<IActionResult> GetShipmentInfo(string shipmentId)
        {
            var shipment = await _mainDbContext.Shipments.Include(x => x.CargoEquipments).FirstOrDefaultAsync(x => x.ShipmentId == shipmentId);
            if(shipment is null)
            {
                throw new Exception($"Bad request parameters: The shipment {shipmentId} doesn't exist");
            }
            var result = new ShipmentDto
            {
                ShipmentReference = shipment.ShipmentId,
                CargoShipmetId = shipment.CargoShipmetId,
                ClosingDate = shipment.ClosingDate,
                ContainerHandlingReference = shipment.ContainerHandlingReference,
                Destination = shipment.Destination,
                PolWaypoint = shipment.PolWaypoint,
                SalingDate = shipment.SalingDate,
                ShipperOwnedBooking = shipment.ShipperOwnedBooking,
                Voyage = shipment.Voyage,
                CargoEquipments = shipment.CargoEquipments.Select(x => new CargoEquipmentDto
                {
                    AssignedContainerNumber = x.AssignedContainerNumber,
                    EquipmentSizeType = x.EquipmentSizeType,
                    Commodity = x.Commodity,
                    Grade = x.Grade
                }).ToList()
            };
            return Ok(result);
        }
    }
}
