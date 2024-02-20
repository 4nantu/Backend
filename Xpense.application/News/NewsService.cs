using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xpense.application.News.Interfaces;
using Xpense.application.News.Models;
using Xpense.domain.News;
using Xpense.infrastructure.Repositories.News.Interfaces;

namespace Xpense.application.News
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<NewsReadDto> CreateNewsAsync(NewsCreateDto newsCreateDto)
        {
            var newsEntity = new News
            {
                Title = newsCreateDto.Title,
                Content = newsCreateDto.Content,
                Priority = newsCreateDto.Priority,
                DatePublished = DateTime.UtcNow // Asumiendo que queremos establecer la fecha actual al crear la noticia
            };

            newsEntity = await _newsRepository.Create(newsEntity);

            return new NewsReadDto
            {
                Id = newsEntity.Id,
                Title = newsEntity.Title,
                Content = newsEntity.Content,
                DatePublished = newsEntity.DatePublished,
                Priority = newsEntity.Priority
            };
        }

        public async Task<bool> UpdateNewsAsync(Guid id, NewsUpdateDto newsUpdateDto)
        {
            var newsEntity = await _newsRepository.Get(id);

            if (newsEntity == null) return false;

            newsEntity.Title = newsUpdateDto.Title;
            newsEntity.Content = newsUpdateDto.Content;
            newsEntity.Priority = newsUpdateDto.Priority;

            await _newsRepository.Update(newsEntity);

            return true;
        }

        public async Task<bool> DeleteNewsAsync(Guid id)
        {
            return await _newsRepository.Delete(id);
        }

        public async Task<NewsReadDto> GetNewsByIdAsync(Guid id)
        {
            var newsEntity = await _newsRepository.Get(id);

            if (newsEntity == null) return null;

            return new NewsReadDto
            {
                Id = newsEntity.Id,
                Title = newsEntity.Title,
                Content = newsEntity.Content,
                DatePublished = newsEntity.DatePublished,
                Priority = newsEntity.Priority
            };
        }

        public async Task<IEnumerable<NewsReadDto>> GetLatestNewsAsync()
        {
            var newsEntities = await _newsRepository.GetAll();

            return newsEntities.Select(n => new NewsReadDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                DatePublished = n.DatePublished,
                Priority = n.Priority
            }).OrderByDescending(n => n.DatePublished).ThenByDescending(n => n.Priority);
        }

        public async Task<bool> PrioritizeNewsAsync(Guid id, int priority)
        {
            var newsEntity = await _newsRepository.Get(id);

            if (newsEntity == null) return false;

            newsEntity.Priority = priority;

            await _newsRepository.Update(newsEntity);

            return true;
        }
    }
}
