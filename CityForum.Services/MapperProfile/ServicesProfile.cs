using AutoMapper;
using CityForum.Entities.Models;
using CityForum.Services.Models;

namespace CityForum.Services.MapperProfile;

public class ServicesProfile : Profile
{
    public ServicesProfile()
    {
        #region Users

        CreateMap<User, UserModel>().ReverseMap();

        #endregion

        #region Topic

        CreateMap<Topic, TopicModel>().ReverseMap();
        CreateMap<CreateTopicModel, Topic>();

        #endregion

        #region Message

        CreateMap<Message, MessageModel>().ReverseMap();
        CreateMap<CreateMessageModel, Message>();

        #endregion
    }
}