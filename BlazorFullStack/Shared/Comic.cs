using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFullStack.Shared
{
    public class Comic
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Color { get; set; }
    }
}
