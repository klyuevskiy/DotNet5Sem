using CityForum.Services.Models;
using CityForum.Services.Abstract;
using CityForum.Repository;
using CityForum.Entities.Models;
using AutoMapper;

namespace CityForum.Services.Impelementation;

public class MessageService : IMessageService
{
    private readonly IRepository<Message> messagesRepository;

    // для проверки существования написавшего пользователя
    private readonly IRepository<User> usersRepository;

    // для проверки существования темы
    private readonly IRepository<Topic> topicsRepository;
    private readonly IMapper mapper;

    public MessageService(
        IRepository<Message> messagesRepository,
        IRepository<User> usersRepository,
        IRepository<Topic> topicsRepository,
        IMapper mapper)
    {
        this.messagesRepository = messagesRepository;
        this.usersRepository = usersRepository;
        this.topicsRepository = topicsRepository;
        this.mapper = mapper;
    }

    private Message GetMessageFromRepository(Guid id)
    {
        Message? message = messagesRepository.GetById(id);
        if (message == null)
        {
            throw new Exception("Message not found id");
        }
        return message;
    }

    public MessageModel CreateMessage(CreateMessageModel createMessageModel)
    {
        if (usersRepository.GetById(createMessageModel.SendingUserId) == null)
        {
            throw new Exception("Sending user not found");
        }
        if (topicsRepository.GetById(createMessageModel.TopicId) == null)
        {
            throw new Exception("Topic not found");
        }

        Message message = mapper.Map<Message>(createMessageModel);
        return mapper.Map<MessageModel>(messagesRepository.Save(message));
    }

    public void DeleteMessage(Guid id)
    {
        messagesRepository.Delete(GetMessageFromRepository(id));
    }

    public PageModel<MessageModel> GetMessages(Guid topicId, int limit = 20, int offset = 0)
    {
        if (topicsRepository.GetById(topicId) == null)
        {
            throw new Exception("topic not found");
        }

        var messages = messagesRepository.GetAll().Where(x => x.TopicId == topicId);
        int totalCount = messages.Count();
        var chunk = messages.OrderByDescending(x => x.CreationTime).Skip(offset).Take(limit);

        return new PageModel<MessageModel>()
        {
            Items = mapper.Map<IEnumerable<MessageModel>>(chunk),
            TotalCount = totalCount
        };
    }

    public MessageModel UpdateMessage(Guid id, UpdateMessageModel updateMessageModel)
    {
        if (messagesRepository.GetById(id) == null)
        {
            throw new Exception("Message not found");
        }

        Message messageToUpdate = GetMessageFromRepository(id);
        messageToUpdate.Text = updateMessageModel.Text;
        return mapper.Map<MessageModel>(messagesRepository.Save(messageToUpdate));
    }
}