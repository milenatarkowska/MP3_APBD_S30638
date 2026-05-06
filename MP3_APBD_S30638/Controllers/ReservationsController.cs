using Microsoft.AspNetCore.Mvc;
using MP3_APBD_S30638.Data;
using MP3_APBD_S30638.Models;

namespace MP3_APBD_S30638.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] DateOnly? date, [FromQuery] Status? status, [FromQuery] int? roomId)
        {
            var query = ExampleData.Reservations.AsQueryable();

            if (date.HasValue)
                query = query.Where(r => r.Date == date.Value);

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            if (roomId.HasValue)
                query = query.Where(r => r.RoomId == roomId.Value);

            return Ok(query.ToList());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reservation = ExampleData.Reservations.FirstOrDefault(r => r.Id == id);
            return reservation == null ? NotFound($"Reservation {id} not found.") : Ok(reservation);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] Reservation resDto)
        {
            var room = ExampleData.Rooms.FirstOrDefault(r => r.Id == resDto.RoomId);
            if (room == null) 
                return NotFound($"Room with ID {resDto.RoomId} does not exist.");
            
            if (!room.IsActive)
                return BadRequest("Cannot book an inactive room.");
            
            if (resDto.EndTime <= resDto.StartTime)
                return BadRequest("End time must be after start time.");
            
            bool hasOverlap = ExampleData.Reservations.Any(r =>
                r.RoomId == resDto.RoomId &&
                r.Date == resDto.Date &&
                r.Status != Status.Cancelled && 
                resDto.StartTime < r.EndTime && resDto.EndTime > r.StartTime);

            if (hasOverlap)
                return Conflict("The room is already booked for this time slot.");
            
            int newId = ExampleData.Reservations.Any() ? ExampleData.Reservations.Max(r => r.Id) + 1 : 1;
            
            var newReservation = new Reservation(
                newId,
                resDto.RoomId,
                resDto.OrganizerName,
                resDto.Topic,
                resDto.Date,
                resDto.StartTime,
                resDto.EndTime,
                resDto.Status
            );

            ExampleData.Reservations.Add(newReservation);

            return CreatedAtAction(nameof(GetById), new { id = newReservation.Id }, newReservation);
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Reservation updatedRes)
        {
            var existingRes = ExampleData.Reservations.FirstOrDefault(r => r.Id == id);
            if (existingRes == null) return NotFound();
            
            bool hasOverlap = ExampleData.Reservations.Any(r =>
                r.Id != id &&
                r.RoomId == updatedRes.RoomId &&
                r.Date == updatedRes.Date &&
                r.Status != Status.Cancelled &&
                updatedRes.StartTime < r.EndTime && updatedRes.EndTime > r.StartTime);

            if (hasOverlap)
                return Conflict("Update failed: new time slot overlaps with an existing reservation.");

            existingRes.RoomId = updatedRes.RoomId;
            existingRes.OrganizerName = updatedRes.OrganizerName;
            existingRes.Topic = updatedRes.Topic;
            existingRes.Date = updatedRes.Date;
            existingRes.StartTime = updatedRes.StartTime;
            existingRes.EndTime = updatedRes.EndTime;
            existingRes.Status = updatedRes.Status;

            return Ok(existingRes);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = ExampleData.Reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();

            ExampleData.Reservations.Remove(res);
            return NoContent();
        }
    }
}