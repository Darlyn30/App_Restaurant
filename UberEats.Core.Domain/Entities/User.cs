
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UberEats.Core.Domain.Entities
{
    public class User
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] cuando el id no es la PK de la tabla
        [Key] // en este caso si es PK
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; } // este valor es unique
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Pin {  get; set; }
        public bool IsActive { get; set; }

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<History> History { get; set; } = new List<History>();
    }
}
