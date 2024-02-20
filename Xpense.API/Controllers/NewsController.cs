using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xpense.application.News.Interfaces;
using Xpense.application.News.Models;

namespace Xpense.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class NewsController : ControllerBase
	{
		private readonly INewsService _newsService;

		public NewsController(INewsService newsService)
		{
			_newsService = newsService;
		}

		[HttpGet]
		public async Task<IActionResult> GetLatestNews()
		{
			var news = await _newsService.GetLatestNewsAsync();
			return Ok(news);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetNewsById(Guid id)
		{
			var news = await _newsService.GetNewsByIdAsync(id);
			return news != null ? Ok(news) : NotFound();
		}

		[HttpPost]
		[Authorize(Roles = "Editor")]
		public async Task<IActionResult> CreateNews([FromBody] NewsCreateDto newsCreateDto)
		{
			var createdNews = await _newsService.CreateNewsAsync(newsCreateDto);
			return CreatedAtAction(nameof(GetNewsById), new { id = createdNews.Id }, createdNews);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Editor")]
		public async Task<IActionResult> UpdateNews(Guid id, [FromBody] NewsUpdateDto newsUpdateDto)
		{
			var result = await _newsService.UpdateNewsAsync(id, newsUpdateDto);
			return result ? Ok() : NotFound();
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Editor")]
		public async Task<IActionResult> DeleteNews(Guid id)
		{
			var result = await _newsService.DeleteNewsAsync(id);
			return result ? Ok() : NotFound();
		}

		[HttpPut("prioritize/{id}")]
		[Authorize(Roles = "Editor")]
		public async Task<IActionResult> PrioritizeNews(Guid id, [FromBody] int priority)
		{
			var result = await _newsService.PrioritizeNewsAsync(id, priority);
			return result ? Ok() : NotFound();
		}
	}
}
