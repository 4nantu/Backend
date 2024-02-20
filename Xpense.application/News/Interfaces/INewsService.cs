using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xpense.application.News.Models;

public interface INewsService
{
    Task<NewsReadDto> CreateNewsAsync(NewsCreateDto newsCreateDto);
    Task<bool> UpdateNewsAsync(Guid id, NewsUpdateDto newsUpdateDto);
    Task<bool> DeleteNewsAsync(Guid id);
    Task<NewsReadDto> GetNewsByIdAsync(Guid id);
    Task<IEnumerable<NewsReadDto>> GetLatestNewsAsync();
    Task<bool> PrioritizeNewsAsync(Guid id, int priority);
}
