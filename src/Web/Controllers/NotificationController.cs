using Application.Interfaces;
using Application.Models.Requests.Notifications;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        [HttpPost("send")]
        public IActionResult SendNotification(NotificationDTO dto)
        {
            var sended = _notificationService.SendNotification(dto.Type, dto.UserId, dto.Brand, dto.Model);
            if (!sended)
            {
                return BadRequest("Tipo de notificación inválido.");
            }
            return Ok();
        }

        [HttpGet("get/{userId}")]
        public IActionResult GetNotifications(int userId)
        {
            var notifications = _notificationService.GetNotification(userId);

            return Ok(notifications);
        }


        [HttpPut("read/{notificationId}")]
        public IActionResult MarkAsRead(int notificationId)
        {
            var result = _notificationService.MarkAsRead(notificationId);
            if (!result)
            {
                return NotFound("Notificación no encontrada.");
            }
            return Ok();
        }
    }
}