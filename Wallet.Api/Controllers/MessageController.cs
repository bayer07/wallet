﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Wallet.Api.Hubs;
using Wallet.Api.Models;
using Wallet.Data.Repositories;
using Wallet.Domain;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public MessageController(MessageRepository messageRepository, IMapper mapper, IHubContext<MessageHub> hubContext)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        private readonly MessageRepository _messageRepository;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly IMapper _mapper;

        [HttpPost]
        public async Task<string> PostMessage(MessageModel messageModel)
        {
            try
            {
                var message = _mapper.Map<Message>(messageModel);

                await _messageRepository.InsertMessage(message);

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            }
            catch (Exception e)
            {
                throw;
            }

            return "Ok";
        }
    }
}
