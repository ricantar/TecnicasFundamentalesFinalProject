﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TecnicasFundamentalesProyectoFinal.Agregar
{
    public partial class AgregarRegistroForm : Material
    {
        public AgregarRegistroForm()
        {
            InitializeComponent();
        }

        private void AgregarRegistroForm_Load(object sender, EventArgs e)
        {

        }

        private void BtVincular_Click(object sender, EventArgs e)
        {
            bool keep = true;

            DataBaseControl DbControl = new DataBaseControl(DataBaseControl.cPath, "ProjectDataBase.mdf");
            string[] parameters = { "@word" };
            string[] value = { TxtID.Text };

            keep = DbControl.Buscar("Select * from Alumnos where [ID] = @word", parameters, value);
            if (keep)
            {
                parameters = new string[] { "@word" };
                value = new string[] { TxtClave.Text };
                keep = DbControl.Buscar("Select * from Materias where [Clave] = @word", parameters, value);
                if (!keep)
                {
                    keep = false;
                }
            }
            
            if (keep == false)
            {
                MessageBox.Show("Es imposible vincular una materia a un estudiante si uno de los dos no existe");
            }
        }

        private void TxtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}