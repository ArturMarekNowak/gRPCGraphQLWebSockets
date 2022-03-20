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

        [HttpGet("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Message> GetMessage(long id)
        {
            var message = _messagesService.GetMessage(id);

            if (message is null)
                return NotFound();
            
            return Ok(message);
        }
        
        [HttpPost]
        [ProducesResponseType(201)] 
        public ActionResult<MessageId> AddMessage(NewMessage newMessage) 
        { 
            var messageId = _messagesService.AddMessage(newMessage);
                     
            return Ok(new MessageId(){ Id = messageId });
        }
    }
}