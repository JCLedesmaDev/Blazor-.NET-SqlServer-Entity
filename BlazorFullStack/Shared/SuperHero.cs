using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFullStack.Shared
{
    // Aqui colocaremos el modelo de clase que tendra nuestro Super Heore
    public class SuperHero
    {
        public int Id { get; set; } 

        //Le otorga como valor predefinido, un string vacio
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string HeroName { get; set; } = string.Empty;
    
        public Comic? Comic { get; set; }
        public int ComicId { get; set; }
    }
}
