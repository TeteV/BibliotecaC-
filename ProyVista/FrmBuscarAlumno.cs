﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buscador.Vista
{
    public partial class FrmBuscarAlumno : Form
    {

        public delegate void accionBoton(int valor);
        public event accionBoton clickBoton;
        public FrmBuscarAlumno()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            clickBoton(0);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            clickBoton(1);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            clickBoton(2);
        }
    }
}
