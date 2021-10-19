using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CRUD2
{

    //Esta clase es la responsable de acceder a la base de datos y ejecutar la consulta
    class ContactoDatos
    {
        //creo el string de coneccion
        SqlConnection cn = new SqlConnection("Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = Pruebas; Data Source = KARI\\SQLEXPRESS");
        public void InsertContact(Contacto contacto)
        {
            try
            {
                //abro la coneccion
                cn.Open();

                //creo el sql para insertar
                string query = @"
                                 INSERT INTO Contactos (Apellido, Nombre, Direccion, Telefono) VALUES (@Apellido, @Nombre, @Direccion, @Telefono)";
                //creo los parametrs
                SqlParameter apellido = new SqlParameter();
                apellido.ParameterName = "@apellido";
                apellido.Value = contacto.Apellido;
                apellido.DbType = System.Data.DbType.String;
                SqlParameter nombre = new SqlParameter("@nombre", contacto.Nombre);
                SqlParameter direccion = new SqlParameter("@direccion", contacto.Direccion);
                SqlParameter telefono = new SqlParameter("@telefono", contacto.Telefono);
                //creo el comando mandando os parametro
                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.Add(apellido);
                comando.Parameters.Add(nombre);
                comando.Parameters.Add(direccion);
                comando.Parameters.Add(telefono);
                //Este comando devuelve cantidad de filas afectadas y cero si no
                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            //cierra la coneccion
            finally
            {
                cn.Close();
            }
        }

        public List<Contacto> GetContact(string search = null)
        {
            //Lista para guardar los contactos
            List<Contacto> contactos = new List<Contacto>();
            try
            {
                cn.Open();
                string query = @"SELECT Id,Apellido,Nombre,Direccion,Telefono
                                   FROM Contactos";
                SqlCommand comando = new SqlCommand();
                if (!string.IsNullOrEmpty(search))
                {
                    //Agrega esto al query para que busque 
                    query += @" WHERE Apellido LIKE @Search OR Nombre LIKE @Search OR Direccion LIKE @Search OR Telefono LIKE @Search";

                    //que lo buscado aparece en cualquier lugar de la columna
                    comando.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                comando.CommandText = query;
                comando.Connection = cn;

                //El data reader Trae todos los registros
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    //Itero el reader y lo agrego a la lista
                    contactos.Add(new Contacto
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Apellido = reader["Apellido"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Telefono = reader["Telefono"].ToString(),

                    });
                }



            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
            return contactos;

        }

        public void UpdateContacto(Contacto contacto)
        {
            try
            {
                //abro la coneccion
                cn.Open();

                //creo el sql para insertar
                string query = @"
                                 UPDATE  Contactos 
                       SET Apellido=@Apellido, Nombre=@Nombre,Direccion=@Direccion,Telefono=@Telefono
                          WHERE Id=@Id";
                //creo los parametrs

                SqlParameter id = new SqlParameter("@id", contacto.Id);
                SqlParameter apellido = new SqlParameter("@apellido", contacto.Apellido);
                SqlParameter nombre = new SqlParameter("@nombre", contacto.Nombre);
                SqlParameter direccion = new SqlParameter("@direccion", contacto.Direccion);
                SqlParameter telefono = new SqlParameter("@telefono", contacto.Telefono);
                //creo el comando mandando os parametro
                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.Add(id);
                comando.Parameters.Add(apellido);
                comando.Parameters.Add(nombre);
                comando.Parameters.Add(direccion);
                comando.Parameters.Add(telefono);
                //Este comando devuelve cantidad de filas afectadas y cero si no
                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {

                //throw;
            }
            //cierra la coneccion
            finally
            {
                cn.Close();
            }
        }
        public void BorrarContacto(int id)

        {
            try
            {
                //abro la coneccion
                cn.Open();

                //creo el sql para insertar
                string query = @"
                                 DELETE FROM Contactos WHERE Id=@Id";
                //creo los parametrs


                //creo el comando mandando el parametro
                SqlCommand comando = new SqlCommand(query, cn);
                //Solo necesito id para borrar
                comando.Parameters.Add(new SqlParameter("@id", id));

                //Este comando devuelve cantidad de filas afectadas y cero si no
                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            //cierra la coneccion
            finally
            {
                cn.Close();
            }

        }
    }
}
