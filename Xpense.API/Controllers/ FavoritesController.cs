using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xpense.application.Favorites.Interfaces;

namespace Xpense.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Asegura que solo usuarios autenticados puedan acceder
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;

        public FavoritesController(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpPost("{newsId}")]
        public async Task<IActionResult> AddToFavorites(Guid newsId)
        {
            var userId = new Guid(User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid")?.Value);
            var result = await _favoritesService.AddToFavorites(userId, newsId);
            return result ? Ok() : BadRequest("No se pudo agregar a favoritos.");
        }

        [HttpDelete("{newsId}")]
        public async Task<IActionResult> RemoveFromFavorites(Guid newsId)
        {
            var userId = new Guid(User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid")?.Value);
            var result = await _favoritesService.RemoveFromFavorites(userId, newsId);
            return result ? Ok() : BadRequest("No se pudo remover de favoritos.");
        }
    }
}
