﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TecnicasFundamentalesProyectoFinal.Eliminar
{
    public partial class EliminarMateriaForm : Material
    {
        public EliminarMateriaForm()
        {
            InitializeComponent();
        }

        private void EliminarMateriaForm_Load(object sender, EventArgs e)
        {

        }

        private void TxtUserName_TextChanged(object sender, EventArgs e)
        {
            if (TxtUserName.Text.Length > 0)
            {
                DataBaseControl DBControl = new DataBaseControl(DataBaseControl.cPath,"ProjectDataBase.mdf");
                LVUsuarios.Clear();
                string[] _parameters = { "@userN" };
                string[] _element = { TxtUserName.Text };
                string[] _returned = { "UserName" };

                foreach (string[] item in DBControl.Buscar("Select [Clave] from [Materias] where [Clave] like '%' + @userN + '%'", _parameters, _element, 1))
                {
                    foreach (var it in item)
                    {
                        LVUsuarios.Items.Add(new ListViewItem(it));
                    }

                }
            }
        }

        private void BtEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DataBaseControl DBControl = new DataBaseControl(DataBaseControl.cPath,"ProjectDataBase.mdf");
                string[] para = { "@user" };
                string[] ele = { TxtUserName.Text };
                if (DBControl.Buscar("Select * from [Registro] where [materia]= @user", para, ele))
                {
                    MessageBox.Show("Esta materia actualmente figura en una seccion, imposible eliminar");
                    TxtUserName.Clear();
                    return;
                }
                else
                {
                    if (DBControl.Eliminar("Delete from [Materias] where [Clave] = @user", para, ele))
                    {
                        MessageBox.Show("Materia eliminado con exito");
                        LVUsuarios.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar Materia");
                    }
                }
                
            }
            catch (Exception) { MessageBox.Show("Por favor, verificar los datos"); }
            
        }

        private void BtCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LVUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtUserName.Text = LVUsuarios.FocusedItem.Text;
        }
    }
}
