using System;
using System.Threading.Tasks;

public interface IFavoritesService
{
    Task<bool> AddToFavorites(AddToFavoritesDto addToFavoritesDto);
    Task<bool> RemoveFromFavorites(RemoveFromFavoritesDto removeFromFavoritesDto);
    Task<IEnumerable<FavoriteNewsDto>> GetFavoritesByUser(Guid userId);
}