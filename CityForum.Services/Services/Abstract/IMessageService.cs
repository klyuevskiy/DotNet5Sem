using CityForum.Services.Models;

namespace CityForum.Services.Abstract;

public interface IMessageService
{
    public MessageModel CreateMessage(CreateMessageModel createMessageModel);
    public MessageModel UpdateMessage(Guid id, UpdateMessageModel updateMessageModel);
    public void DeleteMessage(Guid id);
    public PageModel<MessageModel> GetMessages(Guid topicId, int limit = 20, int offset = 0);
}