using AutoMapper;
using CityForum.Entities.Models;
using CityForum.Repository;
using CityForum.Services.Abstract;
using CityForum.Services.Models;

namespace CityForum.Services.Impelementation;

public class TopicService : ITopicService
{
    private readonly IRepository<Topic> topicsRepository;
    // для проверки существования создавшего пользователя
    private readonly IRepository<User> usersRepository;
    private readonly IMapper mapper;

    public TopicService(IRepository<Topic> topicsRepository, IRepository<User> usersRepository, IMapper mapper)
    {
        this.topicsRepository = topicsRepository;
        this.usersRepository = usersRepository;
        this.mapper = mapper;
    }

    private void CheckTopicNotExist(string topicName)
    {
        if (topicsRepository.GetAll().Where(x => x.Name.ToLower() == topicName.ToLower()).Count() != 0)
        {
            throw new Exception("Topic already exist");
        }
    }

    private Topic GetTopicFromRepository(Guid id)
    {
        Topic? topic = topicsRepository.GetById(id);
        if (topic == null)
        {
            throw new Exception("Topic not found");
        }
        return topic;
    }

    public TopicModel CreateTopic(CreateTopicModel createTopicModel)
    {
        CheckTopicNotExist(createTopicModel.Name);

        if (usersRepository.GetById(createTopicModel.CreatedUserId) == null)
        {
            throw new Exception("Created user not found");
        }

        Topic topic = mapper.Map<Topic>(createTopicModel);
        return mapper.Map<TopicModel>(topicsRepository.Save(topic));
    }

    public void DeleteTopic(Guid id)
    {
        topicsRepository.Delete(GetTopicFromRepository(id));
    }

    public TopicModel GetTopic(Guid id)
    {
        return mapper.Map<TopicModel>(GetTopicFromRepository(id));
    }

    public PageModel<TopicModel> GetTopics(int limit = 20, int offset = 0)
    {
        var topics = topicsRepository.GetAll();
        int totalCount = topics.Count();
        var chunk = topics.OrderByDescending(x => x.ModificationTime).Skip(offset).Take(limit);

        return new PageModel<TopicModel>()
        {
            Items = mapper.Map<IEnumerable<TopicModel>>(chunk),
            TotalCount = totalCount
        };
    }

    public TopicModel UpdateTopic(Guid id, UpdateTopicModel updateTopicModel)
    {
        CheckTopicNotExist(updateTopicModel.Name);
        Topic topicToUpdate = GetTopicFromRepository(id);

        topicToUpdate.Name = updateTopicModel.Name;
        topicToUpdate = topicsRepository.Save(topicToUpdate);
        return mapper.Map<TopicModel>(topicToUpdate);
    }
}