using Microsoft.AspNetCore.Mvc;
using MP3_APBD_S30638.Models;
using MP3_APBD_S30638.Data;

namespace MP3_APBD_S30638.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            var query = ExampleData.Rooms.AsQueryable();
            if (minCapacity.HasValue) query = query.Where(r => r.Capacity >= minCapacity);
            if (hasProjector.HasValue) query = query.Where(r => r.HasProjector == hasProjector);
            if (activeOnly == true) query = query.Where(r => r.IsActive);
            
            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = ExampleData.Rooms.FirstOrDefault(r => r.Id == id);
            return room == null ? NotFound() : Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public IActionResult GetByBuilding(string buildingCode)
        {
            return Ok(ExampleData.Rooms.Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase)));
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            room.Id = ExampleData.Rooms.Max(r => r.Id) + 1;
            ExampleData.Rooms.Add(room);
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Room updatedRoom)
        {
            var index = ExampleData.Rooms.FindIndex(r => r.Id == id);
            if (index == -1) return NotFound();
            updatedRoom.Id = id;
            ExampleData.Rooms[index] = updatedRoom;
            return Ok(updatedRoom);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = ExampleData.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            if (ExampleData.Reservations.Any(res => res.RoomId == id))
                return Conflict("Cannot delete room with existing reservations.");

            ExampleData.Rooms.Remove(room);
            return NoContent();
        }
    }
}
