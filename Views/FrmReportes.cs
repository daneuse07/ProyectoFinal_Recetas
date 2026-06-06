using ChefRecetasCS.Controllers;
using ChefRecetasCS.Models;
using ChefRecetasCS.Reports;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChefRecetasCS.Views
{
    public partial class FrmReportes : Form
    {
        private ComboBox cboTabla = new ComboBox();
        private ComboBox cboTipo = new ComboBox();

        private Button btnGenerar = new Button();

        private CategoriaController _ctrlC =
            new CategoriaController();

        private IngredienteController _ctrlI =
            new IngredienteController();

        private RecetaController _ctrlR =
            new RecetaController();

        private UsuarioController _ctrlU =
            new UsuarioController();

        private Usuario _usuario;

        private DateTimePicker dtInicio =
            new DateTimePicker();

        private DateTimePicker dtFin =
            new DateTimePicker();

        private TextBox txtCriterio =
            new TextBox();

        private NumericUpDown nudIdReceta =
            new NumericUpDown();

        private Label lblInicio =
            new Label();

        private Label lblFin =
            new Label();

        private Label lblCriterio =
            new Label();

        private Label lblId =
            new Label();

        public FrmReportes(Usuario usuario)
        {
            _usuario = usuario;

            Text = "Módulo de Reportes";

            BackColor =
                System.Drawing.Color.FromArgb(245, 235, 220);

            Dock = DockStyle.Fill;

            InicializarControles();
        }

        private void InicializarControles()
        {
            Label lblTabla = new Label();
            lblTabla.Text = "Tabla";
            lblTabla.Left = 30;
            lblTabla.Top = 20;
            lblTabla.Width = 100;

            cboTabla.Left = 30;
            cboTabla.Top = 45;
            cboTabla.Width = 250;

            cboTabla.Items.AddRange(new string[]
            {
        "Categorias",
        "Ingredientes",
        "Recetas",
        "Usuarios"
            });

            Label lblTipo = new Label();
            lblTipo.Text = "Tipo de Reporte";
            lblTipo.Left = 30;
            lblTipo.Top = 90;
            lblTipo.Width = 120;

            cboTipo.Left = 30;
            cboTipo.Top = 115;
            cboTipo.Width = 250;

            cboTipo.Items.AddRange(new string[]
            {
        "General",
        "Fecha",
        "Criterio",
        "Estadistico",
        "Detalle"
            });

            cboTipo.SelectedIndexChanged +=
                CboTipo_SelectedIndexChanged;

            lblInicio.Text = "Fecha Inicial";
            lblInicio.Left = 320;
            lblInicio.Top = 20;

            dtInicio.Left = 320;
            dtInicio.Top = 45;
            dtInicio.Width = 220;

            lblFin.Text = "Fecha Final";
            lblFin.Left = 320;
            lblFin.Top = 80;

            dtFin.Left = 320;
            dtFin.Top = 105;
            dtFin.Width = 220;

            lblCriterio.Text = "Criterio";
            lblCriterio.Left = 320;
            lblCriterio.Top = 140;

            txtCriterio.Left = 320;
            txtCriterio.Top = 165;
            txtCriterio.Width = 220;

            lblId.Text = "ID Receta";
            lblId.Left = 320;
            lblId.Top = 200;

            nudIdReceta.Left = 320;
            nudIdReceta.Top = 225;
            nudIdReceta.Width = 220;
            nudIdReceta.Minimum = 1;

            btnGenerar.Text =
                "Generar PDF";

            btnGenerar.Left = 30;
            btnGenerar.Top = 170;

            btnGenerar.Width = 250;
            btnGenerar.Height = 40;

            btnGenerar.Click +=
                GenerarReporte;

            Controls.Add(lblTabla);
            Controls.Add(cboTabla);

            Controls.Add(lblTipo);
            Controls.Add(cboTipo);

            Controls.Add(lblInicio);
            Controls.Add(dtInicio);

            Controls.Add(lblFin);
            Controls.Add(dtFin);

            Controls.Add(lblCriterio);
            Controls.Add(txtCriterio);

            Controls.Add(lblId);
            Controls.Add(nudIdReceta);

            Controls.Add(btnGenerar);

            MostrarCampos(
                false,
                false,
                false);
        }

        private void MostrarCampos(
    bool fechas,
    bool criterio,
    bool detalle)
        {
            lblInicio.Visible = fechas;
            dtInicio.Visible = fechas;

            lblFin.Visible = fechas;
            dtFin.Visible = fechas;

            lblCriterio.Visible = criterio;
            txtCriterio.Visible = criterio;

            lblId.Visible = detalle;
            nudIdReceta.Visible = detalle;
        }

        private void CboTipo_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            string tipo =
                cboTipo.SelectedItem?.ToString();

            MostrarCampos(
                tipo == "Fecha",
                tipo == "Criterio",
                tipo == "Detalle");
        }

        private void GenerarReporte(
            object sender,
            EventArgs e)
        {
            if (cboTabla.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione una tabla.");

                return;
            }

            if (cboTipo.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione un tipo de reporte.");

                return;
            }

            string tabla =
                cboTabla.SelectedItem.ToString();

            string tipo =
                cboTipo.SelectedItem.ToString();

            switch (tabla)
            {
                case "Categorias":
                    GenerarCategorias(tipo);
                    break;

                case "Ingredientes":
                    GenerarIngredientes(tipo);
                    break;

                case "Recetas":
                    GenerarRecetas(tipo);
                    break;

                case "Usuarios":
                    GenerarUsuarios(tipo);
                    break;
            }
        }

        private void GenerarCategorias(
    string tipo)
        {
            string titulo =
                $"Reporte {tipo} de Categorías";

            switch (tipo)
            {
                case "General":
                    {
                        var lista =
                            _ctrlC.ObtenerTodas();

                        GuardarPdf(
                            new RptCategorias(
                                lista,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Fecha":
                    {
                        var lista =
                            _ctrlC.ObtenerPorFecha(
                                dtInicio.Value.Date,
                                dtFin.Value.Date);

                        GuardarPdf(
                            new RptCategorias(
                                lista,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Criterio":
                    {
                        var lista =
                            _ctrlC.ObtenerPorNombre(
                                txtCriterio.Text);

                        GuardarPdf(
                            new RptCategorias(
                                lista,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Estadistico":
                    {
                        var datos =
                            _ctrlC.ObtenerEstadisticas();

                        GuardarPdf(
                            new RptEstadisticaCategorias(
                                datos,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Detalle":
                    {
                        var categoria =
                            _ctrlC.ObtenerDetalle(
                                (int)nudIdReceta.Value);

                        GuardarPdf(
                            new RptDetalleCategoria(
                                categoria,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }
            }
        }

        private void GenerarIngredientes(
    string tipo)
        {
            string titulo =
                $"Reporte {tipo} de Ingredientes";

            switch (tipo)
            {
                case "General":
                    {
                        var lista =
                            _ctrlI.ObtenerIngredientes();

                        GuardarPdf(
                            new RptIngredientes(
                                lista,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Fecha":
                    {
                        var lista =
                            _ctrlI.ObtenerPorFecha(
                                dtInicio.Value.Date,
                                dtFin.Value.Date);

                        GuardarPdf(
                            new RptIngredientes(
                                lista,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Criterio":
                    {
                        var lista =
                            _ctrlI.ObtenerPorNombre(
                                txtCriterio.Text);

                        GuardarPdf(
                            new RptIngredientes(
                                lista,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Estadistico":
                    {
                        var datos =
                            _ctrlI.ObtenerEstadisticas();

                        GuardarPdf(
                            new RptEstadisticaIngredientes(
                                datos,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Detalle":
                    {
                        var ingrediente =
                            _ctrlI.ObtenerDetalle(
                                (int)nudIdReceta.Value);

                        GuardarPdf(
                            new RptDetalleIngrediente(
                                ingrediente,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }
            }
        }

        private void GenerarRecetas(
    string tipo)
        {
            ReporteController ctrl =
                new ReporteController();

            string titulo =
                $"Reporte {tipo} de Recetas";

            switch (tipo)
            {
                case "General":
                    {
                        var lista =
                            ctrl.ObtenerReporte();

                        GuardarPdf(
                            new RptRecetas(
                                lista,
                                _usuario.Nombre,
                                titulo));

                        break;
                    }

                case "Fecha":
                    {
                        var lista =
                            ctrl.ObtenerRecetasPorFecha(
                                dtInicio.Value.Date,
                                dtFin.Value.Date);

                        GuardarPdf(
                            new RptRecetas(
                                lista,
                                _usuario.Nombre,
                                titulo));

                        break;
                    }

                case "Criterio":
                    {
                        if (string.IsNullOrWhiteSpace(
                            txtCriterio.Text))
                        {
                            MessageBox.Show(
                                "Ingrese un criterio.");

                            return;
                        }

                        var lista =
                            ctrl.ObtenerRecetasPorNombre(
                                txtCriterio.Text);

                        GuardarPdf(
                            new RptRecetas(
                                lista,
                                _usuario.Nombre,
                                titulo));

                        break;
                    }



                case "Detalle":
                    {
                        var receta =
                            ctrl.ObtenerDetalleReceta(
                                (int)nudIdReceta.Value);

                        if (receta == null)
                        {
                            MessageBox.Show(
                                "No existe la receta.");

                            return;
                        }

                        GuardarPdf(
                            new RptDetalleReceta(
                                receta,
                                _usuario.Nombre,
                                titulo));

                        break;
                    }
            }
        }

        private void GenerarUsuarios(
    string tipo)
        {
            string titulo =
                $"Reporte {tipo} de Usuarios";

            switch (tipo)
            {
                case "General":
                    {
                        var lista =
                            _ctrlU.ObtenerUsuarios();

                        GuardarPdf(
                            new RptUsuarios(
                                lista,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Fecha":
                    {
                        var lista =
                            _ctrlU.ObtenerPorFecha(
                                dtInicio.Value.Date,
                                dtFin.Value.Date);

                        GuardarPdf(
                            new RptUsuarios(
                                lista,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Criterio":
                    {
                        var lista =
                            _ctrlU.ObtenerPorNombre(
                                txtCriterio.Text);

                        GuardarPdf(
                            new RptUsuarios(
                                lista,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Estadistico":
                    {
                        var datos =
                            _ctrlU.ObtenerEstadisticas();

                        GuardarPdf(
                            new RptEstadisticaUsuarios(
                                datos,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }

                case "Detalle":
                    {
                        var usuario =
                            _ctrlU.ObtenerDetalle(
                                (int)nudIdReceta.Value);

                        GuardarPdf(
                            new RptDetalleUsuario(
                                usuario,
                                _usuario.Nombre,
                                titulo));
                        break;
                    }
            }
        }

        private void GuardarPdf(
            IDocument reporte)
        {
            using SaveFileDialog sfd =
                new SaveFileDialog();

            sfd.Filter =
                "PDF (*.pdf)|*.pdf";

            sfd.FileName =
                "Reporte.pdf";

            if (sfd.ShowDialog()
                == DialogResult.OK)
            {
                reporte.GeneratePdf(
                    sfd.FileName);

                MessageBox.Show(
                    "PDF generado correctamente.");
            }
        }
    }
}