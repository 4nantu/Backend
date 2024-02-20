using System;
using System.Threading.Tasks;
using Xpense.application.Favorites.Interfaces;
using Xpense.domain.News; // Asume la existencia de un dominio o entidad para las noticias
using Xpense.infrastructure.Repositories.Favorites.Interfaces; // Asume la existencia de un repositorio de favoritos

namespace Xpense.application.Favorites
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly INewsRepository _newsRepository; // Necesario para validar la existencia de las noticias

        public FavoritesService(IFavoritesRepository favoritesRepository, INewsRepository newsRepository)
        {
            _favoritesRepository = favoritesRepository;
            _newsRepository = newsRepository;
        }

        public async Task<bool> AddToFavorites(Guid userId, Guid newsId)
        {
            // Verificar si la noticia existe
            var newsExists = await _newsRepository.Exists(newsId);
            if (!newsExists)
            {
                throw new InvalidOperationException("La noticia especificada no existe.");
            }

            // Verificar si ya está en favoritos para evitar duplicados
            var alreadyFavorited = await _favoritesRepository.IsFavorite(userId, newsId);
            if (alreadyFavorited)
            {
                return false; // O manejar como se vea conveniente
            }

            // Agregar a favoritos
            return await _favoritesRepository.AddToFavorites(userId, newsId);
        }

        public async Task<bool> RemoveFromFavorites(Guid userId, Guid newsId)
        {
            // Verificar si la noticia está en favoritos
            var isFavorite = await _favoritesRepository.IsFavorite(userId, newsId);
            if (!isFavorite)
            {
                return false; // O manejar como se vea conveniente
            }

            // Remover de favoritos
            return await _favoritesRepository.RemoveFromFavorites(userId, newsId);
        }
    }
}
