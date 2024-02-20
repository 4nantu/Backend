using System;

namespace Xpense.domain.Favorites
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }

        // Opcionalmente, puedes incluir propiedades de navegaci�n si est�s usando un ORM que soporte relaciones
        // Esto es �til para cargar datos relacionados de manera eficiente
        // public virtual User User { get; set; }
        // public virtual News News { get; set; }
    }
}
