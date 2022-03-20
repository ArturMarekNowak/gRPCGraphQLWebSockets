using gRPCGraphQLWebSockets.Model;
using gRPCGraphQLWebSockets.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace gRPCGraphQLWebSockets.Controllers
{
    [ApiController]
    [Route("rest/messages")]
    public class MessagesController : Controller
    {
        private readonly IMessagesService _messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpGet("{messageId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Message> GetMessage(int messageId)
        {
            var message = _messagesService.GetMessage(messageId);

            if (message is null)
                return NotFound();
            
            return Ok(message);
        }
        
        [HttpPost]
        [ProducesResponseType(201)] 
        public ActionResult<Message> AddMessage(NewMessage newMessage) 
        { 
            var messageId = _messagesService.AddMessage(newMessage);
                     
            return Ok(new MessageId(){ Id = messageId });
        }
    }
}