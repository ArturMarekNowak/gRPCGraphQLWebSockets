using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// This method allows retrieval of the messages in the project
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<List<Message>> GetMessages()
        {
            var messages = _messagesService.GetMessages();

            if (!messages.Any())
                return NotFound();
            
            return Ok(messages);
        }
        
        /// <summary>
        /// This method allows adding of the message in the project
        /// </summary>
        /// <param name="newMessage">NewMessage object which is just a string with message content</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)] 
        public ActionResult<MessageId> AddMessage(NewMessage newMessage) 
        { 
            var messageId = _messagesService.AddMessage(newMessage);
                     
            return Ok(new MessageId(){ Id = messageId });
        }
    }
}