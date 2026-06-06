using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ChefRecetasCS.Controllers;
using ChefRecetasCS.Models;

namespace ChefRecetasCS.Views
{
    public partial class FrmUsuarios : Form
    {
        // =====================================
        // COMPONENTES
        // =====================================

        private DataGridView dgv =
            new DataGridView();

        private TextBox txtBuscar =
            new TextBox();

        private Button btnBuscar =
            new Button
            {
                Text = "Buscar"
            };

        private TextBox txtNombre =
            new TextBox
            {
                Width = 180
            };

        private TextBox txtUsuario =
            new TextBox
            {
                Width = 180
            };

        private TextBox txtPassword =
            new TextBox
            {
                Width = 180,
                UseSystemPasswordChar = true
            };

        private ComboBox cboRol =
            new ComboBox
            {
                Width = 180,
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        private CheckBox chkActivo =
            new CheckBox
            {
                Text = "Usuario Activo",
                Checked = true
            };

        private Button btnNuevo =
            new Button
            {
                Text = "Nuevo"
            };

        private Button btnGuardar =
            new Button
            {
                Text = "Guardar"
            };

        private Button btnEliminar =
            new Button
            {
                Text = "Eliminar"
            };

        // =====================================
        // CONTROLADOR
        // =====================================

        private UsuarioController ctrl =
            new UsuarioController();

        // =====================================
        // VARIABLES
        // =====================================

        private int idSeleccion = 0;

        private Usuario _usuario;

        // =====================================
        // CONSTRUCTOR
        // =====================================

        public FrmUsuarios(Usuario u)
        {
            _usuario = u;

            Dock = DockStyle.Fill;

            BackColor = Color.Beige;

            ConfigurarDGV();

            // =====================================
            // ROLES
            // =====================================

            cboRol.Items.AddRange(
                new string[]
                {
                    "admin",
                    "operador",
                    "consultor"
                });

            cboRol.SelectedIndex = 0;

            // =====================================
            // PANEL SUPERIOR
            // =====================================

            var topPanel =
                new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 50,
                    BackColor = Color.Beige
                };

            txtBuscar.Width = 220;

            txtBuscar.Location =
                new Point(10, 12);

            btnBuscar.Location =
                new Point(240, 10);

            btnBuscar.BackColor =
                Color.SandyBrown;

            btnBuscar.ForeColor =
                Color.White;

            btnBuscar.FlatStyle =
                FlatStyle.Flat;

            btnBuscar.Click +=
                (s, e) =>
                {
                    string texto =
                        txtBuscar.Text.ToLower();

                    dgv.DataSource =
                        ctrl.ObtenerUsuarios()
                            .FindAll(u2 =>
                                u2.Nombre
                                    .ToLower()
                                    .Contains(texto)
                                ||
                                u2.UserName
                                    .ToLower()
                                    .Contains(texto));
                };

            topPanel.Controls.Add(txtBuscar);

            topPanel.Controls.Add(btnBuscar);

            // =====================================
            // SPLIT
            // =====================================

            var panel =
                new SplitContainer
                {
                    Dock = DockStyle.Fill,
                    SplitterDistance = 650
                };

            panel.Panel1.Padding =
                new Padding(5);

            panel.Panel1.Controls.Add(dgv);

            panel.Panel2.Controls.Add(
                PanelFormulario());

            Controls.Add(panel);

            Controls.Add(topPanel);

            // =====================================
            // EVENTOS
            // =====================================

            dgv.SelectionChanged +=
                DGV_SelectionChanged;

            btnNuevo.Click +=
                (s, e) => Limpiar();

            btnGuardar.Click +=
                (s, e) => Guardar();

            btnEliminar.Click +=
                (s, e) => Eliminar();

            // =====================================
            // CARGAR DATOS
            // =====================================

            CargarDGV();
        }

        // =====================================
        // CONFIGURAR DGV
        // =====================================

        private void ConfigurarDGV()
        {
            dgv.Dock = DockStyle.Fill;

            dgv.ReadOnly = true;

            dgv.SelectionMode =
                DataGridViewSelectionMode
                    .FullRowSelect;

            dgv.AutoGenerateColumns = true;

            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode
                    .Fill;

            dgv.AllowUserToAddRows = false;

            dgv.BackgroundColor =
                Color.White;
        }

        // =====================================
        // FORMULARIO
        // =====================================

        private Panel PanelFormulario()
        {
            var p =
                new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Beige
                };

            int y = 40;

            void Fila(
                string texto,
                Control ctrlCampo)
            {
                p.Controls.Add(
                    new Label
                    {
                        Text = texto,

                        Location =
                            new Point(10, y + 3),

                        AutoSize = true,

                        Font =
                            new Font(
                                "Segoe UI",
                                10,
                                FontStyle.Bold)
                    });

                ctrlCampo.Location =
                    new Point(110, y);

                p.Controls.Add(ctrlCampo);

                y += ctrlCampo.Height + 15;
            }

            Fila("Nombre:", txtNombre);

            Fila("Usuario:", txtUsuario);

            Fila("Password:", txtPassword);

            Fila("Rol:", cboRol);

            Fila("Estado:", chkActivo);

            // =====================================
            // BOTONES
            // =====================================

            btnNuevo.Location =
                new Point(10, y + 20);

            btnGuardar.Location =
                new Point(100, y + 20);

            btnEliminar.Location =
                new Point(190, y + 20);

            btnNuevo.BackColor =
                Color.SandyBrown;

            btnGuardar.BackColor =
                Color.SandyBrown;

            btnEliminar.BackColor =
                Color.Firebrick;

            btnNuevo.ForeColor =
                Color.White;

            btnGuardar.ForeColor =
                Color.White;

            btnEliminar.ForeColor =
                Color.White;

            btnNuevo.FlatStyle =
                FlatStyle.Flat;

            btnGuardar.FlatStyle =
                FlatStyle.Flat;

            btnEliminar.FlatStyle =
                FlatStyle.Flat;

            p.Controls.AddRange(
                new Control[]
                {
                    btnNuevo,
                    btnGuardar,
                    btnEliminar
                });

            return p;
        }

        // =====================================
        // CARGAR TABLA
        // =====================================

        private void CargarDGV()
        {
            dgv.DataSource = null;

            dgv.DataSource =
                ctrl.ObtenerUsuarios();

            if (dgv.Columns["Activo"] != null)
            {
                dgv.Columns["Activo"].HeaderText =
                    "Activo";

            }
        }

        // =====================================
        // SELECCION
        // =====================================

        private void DGV_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgv.CurrentRow == null)
                return;

            if (dgv.CurrentRow.DataBoundItem == null)
                return;

            var u =
                (Usuario)dgv
                    .CurrentRow
                    .DataBoundItem;

            idSeleccion =
                u.IdUsuario;

            txtNombre.Text =
                u.Nombre;

            txtUsuario.Text =
                u.UserName;

            cboRol.Text =
                u.Rol;

            chkActivo.Checked =
                u.Activo;

            txtPassword.Clear();
        }

        // =====================================
        // GUARDAR
        // =====================================

        private void Guardar()
        {
            try
            {
                ctrl.GuardarUsuario(
                    idSeleccion,
                    txtNombre.Text.Trim(),
                    txtUsuario.Text.Trim(),
                    txtPassword.Text,
                    cboRol.Text,
                    chkActivo.Checked);

                CargarDGV();
                Limpiar();

                MessageBox.Show("Usuario guardado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =====================================
        // ELIMINAR
        // =====================================

        private void Eliminar()
        {
            if (idSeleccion == 0)
                return;

            if (MessageBox.Show(
                "¿Eliminar usuario?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                ctrl.EliminarUsuario(
                    idSeleccion);

                CargarDGV();

                Limpiar();
            }
        }

        // =====================================
        // LIMPIAR
        // =====================================

        private void Limpiar()
        {
            idSeleccion = 0;

            txtNombre.Clear();

            txtUsuario.Clear();

            txtPassword.Clear();

            cboRol.SelectedIndex = 0;

            chkActivo.Checked = true;
        }


    }
}