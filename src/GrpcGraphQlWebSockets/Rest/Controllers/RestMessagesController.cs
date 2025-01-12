using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcGraphQlWebSockets.Rest.Model;
using GrpcGraphQlWebSockets.Rest.Services.Interfaces;
using GrpcGraphQlWebSockets.SharedModel;
using Microsoft.AspNetCore.Mvc;

namespace GrpcGraphQlWebSockets.Rest.Controllers
{
    [ApiController]
    [Route("rest/messages")]
    public class MessagesController : Controller
    {
        private readonly IRestMessagesService _messagesService;

        public MessagesController(IRestMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        /// <summary>
        /// This method allows retrieval of the messages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<List<Message>> GetMessages()
        {
            var messages = _messagesService.GetMessages();

            return Ok(messages);
        }

        /// <summary>
        /// This method allows addition of the message
        /// </summary>
        /// <param name="newMessage">NewMessage object which is just a string with message content</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<RestMessageId>> CreateMessage(RestNewMessage newMessage)
        {
            var messageId = await _messagesService.CreateMessage(newMessage);

            return Ok(new RestMessageId {Id = messageId});
        }
    }
}