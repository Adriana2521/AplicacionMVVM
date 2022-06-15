using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joyeria.Models
{
    public enum Categoria { Playa, Ciudad, Pueblo ,Escursion}
   public class CatalogoJoyas
   {

        public string Destino { get; set; }
        public Categoria Categoria{ get; set; }

        public string Pais { get; set; }

        public decimal precio { get; set; }

        public string Foto { get; set; } = string.Empty;

        

    }
}
