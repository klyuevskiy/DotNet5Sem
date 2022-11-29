using CityForum.Services.Models;

namespace CityForum.Services.Abstract;

public interface ITopicService
{
    public TopicModel GetTopic(Guid id);
    public void DeleteTopic(Guid id);
    public TopicModel UpdateTopic(Guid id, UpdateTopicModel updateTopicModel);
    public TopicModel CreateTopic(CreateTopicModel createTopicModel);
    public PageModel<TopicModel> GetTopics(int limit = 20, int offset = 0);
}