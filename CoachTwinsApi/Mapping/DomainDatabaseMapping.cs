using System;
using System.Linq;
using AutoMapper;
using CoachTwins.Models.Chatting.Messages;
using CoachTwins.Models.Matching;
using CoachTwins.Models.Users;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Entities;
using CoachTwinsClassLibrary.Enums.Matching;
using CoachTwinsClassLibrary.Models.Dashboard;
using Appointment = CoachTwins.Models.Appointments.Appointment;
using Chat = CoachTwins.Models.Chatting.Chat;
using DbStudent = CoachTwinsApi.Db.Entities.Student;
using DbCoach = CoachTwinsApi.Db.Entities.Coach;
using DbUser = CoachTwinsApi.Db.Entities.User;
using Message = CoachTwinsApi.Db.Entities.Message;
using Student = CoachTwins.Models.Users.Student;
using User = CoachTwins.Models.Users.User;

namespace CoachTwinsAPI.Mapping
{
    public class DomainDatabaseMapping: Profile
    {
        public DomainDatabaseMapping(AuthStore authStore)
        {
            CreateMap<Student, DbStudent>()
                .IncludeBase<User, DbUser>()
                .ForMember(u => u.Id, s => s.MapFrom(src => Guid.NewGuid()));

            CreateMap<DbStudent, Student>()
                .IncludeBase<DbUser, User>();

            CreateMap<StudentCoach, DbCoach>()
                .IncludeBase<User, DbUser>()
                .ForMember(u => u.Id, s => s.MapFrom(src => Guid.NewGuid()));

            CreateMap<DbStudent, Coachee>()
                .IncludeAllDerived()
                .ForMember(u => u.Id, s => s.MapFrom(src => Guid.NewGuid()));
            
            CreateMap<DbCoach, StudentCoach>()
                .IncludeBase<DbUser, User>();

            CreateMap<User, DbUser>()
                .ForMember(u => u.Id, s => s.MapFrom(src => Guid.NewGuid()));

            CreateMap<DbUser, User>()
                .ForMember(u => u.Age, u => u.MapFrom(src => src.BirthDate != default ? ((int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(src.BirthDate.ToString("yyyyMMdd"))) / 10000) : 0))
                .ForMember(u => u.Characteristics, u => u.MapFrom(
                        src => src.MatchingCriteria
                            .Where(m => (m.Prefer ?? true) && m.Criteria.Category == "Competences")
                            .Select(m => m.Value)
                    )
                )
                .ForMember(u => u.Interests, u => u.MapFrom(
                        src => src.MatchingCriteria
                            .Where(m => (m.Prefer ?? true) && m.Criteria.Category == "Hobbies")
                            .Select(m => m.Value)
                    )
                )
                .ForMember(u => u.SpecialNeeds, u => u.MapFrom(
                        src => src.MatchingCriteria
                            .Where(m => (m.Prefer ?? true) && m.Criteria.Category == "Affinity")
                            .Select(m => m.Value)
                    )
                );

            CreateMap<Chat, CoachTwinsApi.Db.Entities.Chat>()
                .ForMember(u => u.Id, s => s.MapFrom(src => Guid.NewGuid()));
            CreateMap<CoachTwinsApi.Db.Entities.Chat, Chat>();

            CreateMap<Appointment, CoachTwinsApi.Db.Entities.Appointment>()
                .ForMember(u => u.Id, s => s.MapFrom(src => Guid.NewGuid()));
            CreateMap<CoachTwinsApi.Db.Entities.Appointment, Appointment>()
                .ForMember(a => a.IsOwner, s => s.MapFrom(src => src.Creator != null && src.Creator.Id.Equals(authStore.Guid)));

            CreateMap<Message, CoachTwins.Models.Chatting.Messages.Message>();
            
            CreateMap<TextMessage, Message>()
                .ForMember(u => u.Id, s => s.MapFrom(src => Guid.NewGuid()));
            CreateMap<Message, TextMessage>()
                .IncludeAllDerived()
                .ForMember(u => u.SentAt, s => s.MapFrom(src => src.SendAt));

            CreateMap<Match, CoachingMatch>()
                .ForMember(m => m.StudentCoach, s => s.MapFrom(src => src.Coach));
            
            // admin models
            CreateMap<DbUser, Models.Admin.AdminUser>();
            CreateMap<DbCoach, Models.Admin.AdminUser>()
                .IncludeAllDerived()
                .ForMember(u => u.isACoach, s => s.MapFrom(src => true));
            CreateMap<DbStudent, Models.Admin.AdminUser>()
                .IncludeAllDerived()
                .ForMember(u => u.isACoach, s => s.MapFrom(src => false));;
        }
    }
}