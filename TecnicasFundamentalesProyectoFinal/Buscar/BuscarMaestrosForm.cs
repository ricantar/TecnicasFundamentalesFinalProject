﻿using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TecnicasFundamentalesProyectoFinal
{
    public partial class BuscarMaestrosForm : MaterialForm
    {
        public BuscarMaestrosForm()
        {
            InitializeComponent();
            MaterialSkinManager MaterialSkinControl = MaterialSkinManager.Instance;
            MaterialSkinControl.AddFormToManage(this);
            MaterialSkinControl.Theme = MaterialSkinManager.Themes.LIGHT;
            MaterialSkinControl.ColorScheme = new ColorScheme(
                primary: Primary.Blue400, darkPrimary: Primary.Blue500,
                lightPrimary: Primary.Blue300, accent: Accent.LightBlue200,
                textShade: TextShade.WHITE
                );
            this.Size = new Size(1589, 750);
        }

        private void BuscarMaestrosForm_Load(object sender, EventArgs e)
        {
            DataBaseControl DBControl = new DataBaseControl(DataBaseControl.cPath, "ProjectDataBase.mdf");
            BindingSource MasterSource = new BindingSource();
            MasterSource.DataSource = DBControl.ObtenerTabla("Select * from Maestros");
            DGVMaestros.DataSource = MasterSource;
            DGVMaestros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGVMaestros.ReadOnly = true;
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            DataBaseControl DBControl = new DataBaseControl(DataBaseControl.cPath, "ProjectDataBase.mdf");
            BindingSource MasterSource = new BindingSource();
            string[] parameters = { "@word" };
            string[] value = { TxtBuscar.Text };
            MasterSource.DataSource = DBControl.ObtenerTabla("Select * from Maestros where [Nombre] like '%'+@word+'%' or [ID] like '%'+@word+'%' ", parameters, value);
            DGVMaestros.DataSource = MasterSource;
            DGVMaestros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGVMaestros.ReadOnly = true;
        }

        private void BtSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}