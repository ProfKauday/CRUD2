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
    public partial class FormMain : Form
    {
        //Creo una instancia de la capa de negocios
        private ContactoLogic _contactologic;
        public FormMain()
        {
            InitializeComponent();
            _contactologic = new ContactoLogic();


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //llamo al formulario para agregar datos.
            FormContactos formcontactos = new FormContactos();
            formcontactos.ShowDialog(this);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            CargarContactos();
        }

        //Carga contactos de la base de datos en la grilla
        public void CargarContactos(String SearchText = null)
        {
            List<Contacto> contactos = _contactologic.GetContact(SearchText);
            GridContactos.DataSource = contactos;

        }

        private void GridContactos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //cell va a ser nulo si no hace click en editar , o sea e la columna de editar. El dataGridviewLinkcell es alguna de las celdas de las columnas de editar o eliminar
            DataGridViewLinkCell cell = (DataGridViewLinkCell)GridContactos.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //Veo que link apreto para saber que hacer
            if (cell.Value.ToString() == "Editar")
            {
                //llamo al formulario de contactos para poder editar
                FormContactos formcontactos = new FormContactos();
                formcontactos.LoadContact(new Contacto
                {
                    Id = int.Parse((GridContactos.Rows[e.RowIndex].Cells[0]).Value.ToString()),
                    Apellido = GridContactos.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    Nombre = GridContactos.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Direccion = GridContactos.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Telefono = GridContactos.Rows[e.RowIndex].Cells[4].Value.ToString(),
                });
                //Muestra el formulario de educion
                formcontactos.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Eliminar")
            {
               BorrarContacto(int.Parse((GridContactos.Rows[e.RowIndex].Cells[0]).Value.ToString()));
                CargarContactos();

            }
        }

        private void BorrarContacto (int id)
        {
            _contactologic.BorrarContacto(id);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarContactos(txtSearch.Text);
            txtSearch.Text = string.Empty;
        }
    }
}
