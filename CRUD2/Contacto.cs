using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD2
{
    public class Contacto
    {
        //Esta clase representa el modelo de la tabla. Definimos las misas propiedas que la tabla en la base de datos
        public int Id { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
