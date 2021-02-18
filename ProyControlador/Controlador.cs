using System;
using System.Windows.Forms;
using Buscador.Vista;
using Buscador.Modelo;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Buscador.Controlador
{
    public class Controlador
    {
        Vista.Menu menu = null;
        Vista.FrmVerAlumnos vistaAlumno = null;
        Vista.FrmBuscarAlumno buscarAlumno = null;
        Modelo.Modelo modelo = null;

        public Controlador()
        {

            modelo = new Modelo.Modelo();
            IniciarMenu();
            
        }
        public void IniciarMenu()
        {
            try
            {
                menu = new Vista.Menu();

                menu.clickBoton += MenuClickBoton;

                menu.ShowDialog();
            }
            catch(Exception e) {
                MessageBox.Show("Ha ocurrido un error");
            }
           
            
        }
        private void MenuClickBoton(int valor)
        {

            if (valor == 0) { 
            if (MessageBox.Show("¿Seguro de que quieres salir de la aplicación?", "Salir", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();   
             }
            
            if (valor == 1)
            {
                menu.Hide();
                VerAlumnos();
            }

            if (valor == 2) { 
                menu.Hide();
                BuscarEliminarAlumno();
            }
        }

        private void VerAlumnos()
        {
            try
            {
                vistaAlumno = new Vista.FrmVerAlumnos();
                vistaAlumno.clickBoton += VAlumnoClickBoton;
                MostrarDatos();
                vistaAlumno.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al acceder a VistaAlumnos");
            }
        }

        private void VAlumnoClickBoton(int valor)
        {
            if (valor == 0) {
                vistaAlumno.Hide();
                menu.Show();
            }
                
        }
        private void BuscarEliminarAlumno()
        {
            try {
                buscarAlumno = new Vista.FrmBuscarAlumno();
                buscarAlumno.clickBoton += BucarAlumnoClickBoton;
                buscarAlumno.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al abrir Buscar alumno");
            }
            
        }

        private void BucarAlumnoClickBoton(int valor)
        {
            if (valor == 0)
            {
                
                BuscarAlumno();
            }
             
            if (valor == 1)
            {
                if (MessageBox.Show("¿Seguro de que quieres eliminar este alumno?","Eliminar alumno", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    EliminarAlumno();
                else
                    MessageBox.Show("Alumno no eliminado");

            }

            if (valor == 2)
            {
                buscarAlumno.Hide();
                menu.Show();
            }
        }
        
        public void MostrarDatos()
        {
            try
            {
                DataSet listaAlumnos = new DataSet();
                MySqlDataAdapter adaptador = modelo.MostrarAlumnos();
                adaptador.Fill(listaAlumnos);
                DataGridView tablaAlumnos = vistaAlumno.GetDataGridAlumnos();

                tablaAlumnos.DataSource = listaAlumnos.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al mostrar los datos de los alumnos");
            }
           
            
        }

        public void BuscarAlumno()
        {
            try
            {
                String dni = buscarAlumno.TxtDniAlumno.Text;

                DataSet alumno = new DataSet();
                MySqlDataAdapter adaptador = modelo.BuscadorAlumno(dni);
                adaptador.Fill(alumno);
                DataGridView tablaAlumno = buscarAlumno.DataGridAlumno;

                tablaAlumno.DataSource = alumno.Tables[0];
                tablaAlumno.Columns[0].Visible = false;

                buscarAlumno.TxtDniAlumno.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al buscar un alumno");
            }
          
        }

        public void EliminarAlumno()
        {
            try
            {
                String dni = buscarAlumno.TxtDniAlumno.Text;
                modelo.EliminarAlumno(dni);
                MessageBox.Show("Alumno eliminado");
                buscarAlumno.TxtDniAlumno.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al eliminar alumno");
            }
    

        }



    }
}
