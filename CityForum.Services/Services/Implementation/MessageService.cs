using CityForum.Services.Models;
using CityForum.Services.Abstract;
using CityForum.Repository;
using CityForum.Entities.Models;
using AutoMapper;

namespace CityForum.Services.Impelementation;

public class MessageService : IMessageService
{
    private readonly IRepository<Message> messagesRepository;
    private readonly IMapper mapper;

    public MessageService(IRepository<Message> messagesRepository, IMapper mapper)
    {
        this.messagesRepository = messagesRepository;
        this.mapper = mapper;
    }

    private Message GetMessageFromRepository(Guid id)
    {
        Message? message = messagesRepository.GetById(id);
        if (message == null)
        {
            throw new Exception($"Message not found id = {id}");
        }
        return message;
    }

    public MessageModel CreateMessage(CreateMessageModel createMessageModel)
    {
        Message message = mapper.Map<Message>(createMessageModel);
        return mapper.Map<MessageModel>(messagesRepository.Save(message));
    }

    public void DeleteMessage(Guid id)
    {
        messagesRepository.Delete(GetMessageFromRepository(id));
    }

    public PageModel<MessageModel> GetMessages(Guid topicId, int limit = 20, int offset = 0)
    {
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
        Message messageToUpdate = GetMessageFromRepository(id);
        messageToUpdate.Text = updateMessageModel.Text;
        return mapper.Map<MessageModel>(messagesRepository.Save(messageToUpdate));
    }
}