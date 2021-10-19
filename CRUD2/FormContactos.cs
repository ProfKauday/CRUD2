using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD2
{
    public partial class FormContactos : Form
    {
        //declaro una variable gloal de la clase contactologic puedo acceder desde cualquir clase
        private ContactoLogic _contactologic;
        //Creo una nuwva instancia de cocntacto para obtener el id. con esta instancia voy a saber si esta editando o grabando un nuevo contacto
        private Contacto _contacto;
        public FormContactos()
        {
            InitializeComponent();
            _contactologic = new ContactoLogic();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            //guarda el contacto
            SaveContact();
            //cierra la coneccion
            this.Close();
            //actualiza la carga de contactos en la grilla en el formulario main. Llamando al formulario padre para que se actualice
            ((FormMain)this.Owner).CargarContactos();
        }

        //guarda el contacto
        private void SaveContact()
        {
            Contacto contacto = new Contacto();
            contacto.Apellido = txtApellido.Text;
            contacto.Nombre = txtNombre.Text;
            contacto.Direccion = txtDireccion.Text;
            contacto.Telefono = txtTelefono.Text;
            //Si el Id es distinto de null este metdo se ejecuto y id tiene algo , y si no ponemos 0. Si entro por edit contact id tiene algo y si no esta vacio
            // contacto.Id = _contacto != null ? _contacto.Id : 0;

           //Si la instancia _contacto es distinto de null significa que entro por el edit entonces uso el id , y si no entro por guardar y guardo 0
           //ya que usa el mismo boton de guardar necesito diferenciar si esta editando o grabando uno nuevo por eso usa _contacto
           //? para verdadero y : para falso.
            contacto.Id = _contacto != null ? _contacto.Id : 0;
            _contactologic.GurdarContacto(contacto);

        }

        public void LoadContact(Contacto contacto)

        {//aqui en esta variabl global copio la instancia e contacto para guardarme el ID del contacto
            _contacto = contacto;
            if (contacto != null)
            {
                ClearForm();
                txtApellido.Text = contacto.Apellido;
                txtNombre.Text = contacto.Nombre;
                txtDireccion.Text = contacto.Direccion;
                txtTelefono.Text = contacto.Telefono;
            }

        }
        private void ClearForm()
        {
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }

    }
}
