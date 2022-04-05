using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwins.Models.Chatting.Messages;
using CoachTwinsApi;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using CoachTwinsAPI.Logic.Matching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web;
using Message = CoachTwinsApi.Db.Entities.Message;

namespace CoachTwinsAPI.SignalR
{
    [LoginRequired]
    public class ChatHub: Hub
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly MatchingService _matchingService;
        private readonly IMatchingRepository _matchingRepository;
        private readonly IMapper _mapper;

        public ChatHub(IUserRepository userRepository, IChatRepository chatRepository, MatchingService matchingService, IMatchingRepository matchingRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _matchingService = matchingService;
            _matchingRepository = matchingRepository;
            _mapper = mapper;
        }

        private async Task<User?> GetUser()
        {
            var userId = Context.User?.GetObjectId();

            if (userId == null)
                return null;
            
            return await _userRepository.Get(Guid.Parse(userId));
        }

        private async Task<Match?> GetMatch(User user, Guid matchId)
        {
            return user == null ? null : _matchingService.GetMatchFromUser(user, matchId);
        }
        
        public async Task Join(Guid matchId)
        {
            var user = await GetUser();
            var match = await GetMatch(user, matchId);

            if (match == null)
                return;

            match.Chat ??= new Chat();

            if (user is Coach)
                match.Chat.UnseenMessagesCoach = 0;
            if (user is Student)
                match.Chat.UnseenMessagesCoachee = 0;
            
            await _matchingRepository.Update(match);

            await Groups.AddToGroupAsync(Context.ConnectionId, match.Id.ToString());
            
            await Clients.Caller.SendAsync("LoadMessages", _mapper.Map<List<TextMessage>>(match.Chat.Messages));
        }

        public async Task MessageReceived(Guid matchId)
        {
            var user = await GetUser();
            var match = await GetMatch(user, matchId);
            
            if (match == null)
                return;

            if (user is Coach)
                match.Chat.UnseenMessagesCoach = 0;
            if (user is Student)
                match.Chat.UnseenMessagesCoachee = 0;
            
            await _chatRepository.Update(match.Chat);
        }
        
        public async Task SendMessage(Guid matchId, string message)
        {
            var user = await GetUser();
            var match = await GetMatch(user, matchId);
            
            if (match == null)
                return;

            var textMessage = new Message()
            {
                Source = user,
                SendAt = DateTime.UtcNow,
                Text = message
            };

            await Clients.Group(match.Id.ToString()).SendAsync("ReceiveMessage", _mapper.Map<TextMessage>(textMessage));
            match.Chat.Messages.Add(textMessage);
            
            if (user is Coach)
                match.Chat.UnseenMessagesCoach++;
            if (user is Student)
                match.Chat.UnseenMessagesCoachee++;
            
            await _chatRepository.Update(match.Chat);
        }
    }
}