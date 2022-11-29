using AutoMapper;
using CityForum.WebApi.Models;
using CityForum.Services.Models;

namespace CityForum.WebApi.MapperProfile;

public class PresentationProfile : Profile
{
    public PresentationProfile()
    {
        #region Pages

        CreateMap(typeof(PageModel<>), typeof(PageResponse<>));

        #endregion

        #region Users

        CreateMap<UserModel, UserResponse>();

        #endregion

        #region Topics

        CreateMap<TopicModel, TopicResponse>();
        CreateMap<UpdateTopicRequest, UpdateTopicModel>();
        CreateMap<CreateTopicRequest, CreateTopicModel>();

        #endregion

        CreateMap<MessageModel, MessageResponse>();
        CreateMap<UpdateMessageRequest, UpdateMessageModel>();
        CreateMap<CreateMessageRequest, CreateMessageModel>();

        #region Messages

        #endregion
    }
}