using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gRPCGraphQLWebSockets.Rest.Model;
using gRPCGraphQLWebSockets.Rest.Services.Interfaces;
using gRPCGraphQLWebSockets.SharedModel;
using Microsoft.AspNetCore.Mvc;

namespace gRPCGraphQLWebSockets.Rest.Controllers
{
    [ApiController]
    [Route("rest/messages")]
    public class MessagesController : Controller
    {
        private readonly IRESTMessagesService _messagesService;

        public MessagesController(IRESTMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        /// <summary>
        ///     This method allows retrieval of the messages in the project
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
        ///     This method allows adding of the message in the project
        /// </summary>
        /// <param name="newMessage">NewMessage object which is just a string with message content</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<RESTMessageId>> CreateMessage(RESTNewMessage newMessage)
        {
            var messageId = await _messagesService.CreateMessage(newMessage);

            return Ok(new RESTMessageId {Id = messageId});
        }
    }
}