using Microsoft.EntityFrameworkCore;
using Nyma.Application.Security;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Message;
using Nyma.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Application.Services.Implementations
{
    public class MessageService : IMessageService
    {
        #region constructor

        private readonly AppDbContext _context;

        public MessageService(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<bool> CreateMessage(CreateMessageViewModel message)
        {
            Message newMessage = new Message()
            {
                Email = message.Email.SanitizeText(),
                Name = message.Name.SanitizeText(),
                Text = message.Text.SanitizeText()
            };

            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<MessageViewModel>> GetAllMessages()
        {
            List<MessageViewModel> messages = await _context.Messages
                .Select(m => new MessageViewModel()
                {
                    Id = m.Id,
                    Email = m.Email,
                    Name = m.Name,
                    Text = m.Text,
                })
                .ToListAsync();

            return messages;
        }

        public async Task<bool> DeleteMessage(long id)
        {
            Message message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);

            if (message == null) return false;

            _context.Messages.Remove(message);
           await _context.SaveChangesAsync();

            return true;
        }

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        #endregion
    }
}
