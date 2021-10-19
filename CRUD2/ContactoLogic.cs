using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD2
{
    class ContactoLogic
    {//declaro una variableclase de acceso a datos
        private ContactoDatos _contactodatos;
        //Esta Clase es la capa de negocios. Aqui estan las validaciones
        //Este metodo guarda los contactos

        //en el constructos Creo una instancia de la clase de acceso a datos 
        public ContactoLogic (){
            //Creo una instancia del ojeto de acceso a datos en el constructor
            _contactodatos = new ContactoDatos();

       }

        public Contacto GurdarContacto(Contacto contacto)
        {

           //Es cero cuando graba un contacto nuevo
            if (contacto.Id == 0)


               _contactodatos.InsertContact(contacto);


            else
            {
              _contactodatos.UpdateContacto(contacto);

            }
            return contacto;
        }
        //Devueve en una lista los contactos de la base de datos
        public List<Contacto> GetContact(string TextSearch = null)

        {

            return _contactodatos.GetContact(TextSearch);
            
        }

        public void BorrarContacto(int id)
        {
            
            _contactodatos.BorrarContacto(id);

        }
    }
}
