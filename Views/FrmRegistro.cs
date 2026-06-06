using System;
using System.Drawing;
using System.Windows.Forms;
using ChefRecetasCS.Controllers;

namespace ChefRecetasCS.Views
{
    public class FrmRegistro : Form
    {
        private TextBox txtNombre = new TextBox();
        private TextBox txtUsuario = new TextBox();
        private TextBox txtPassword = new TextBox
            {
                UseSystemPasswordChar = true
            };

        // =====================================
        // COMBO ROL
        // =====================================

        private ComboBox txtRol =
            new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            };

        private Button btnRegistrar = new Button
            {
                Text = "Registrar"
            };

        private UsuarioController ctrl = new UsuarioController();

        public FrmRegistro()
        {
            Text ="Registro de Usuario";
            Size = new Size(350, 340);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245,235,220);

            // =====================================
            // OPCIONES DE ROL
            // =====================================

            txtRol.Items.AddRange(
                new string[]
                { "admin", "operador", "consultor"
                });

            txtRol.SelectedIndex = 0;

            // =====================================
            // CAMPOS
            // =====================================

            CrearCampo( "Nombre:", txtNombre, 30);
            CrearCampo( "Usuario:", txtUsuario, 80);
            CrearCampo(
                "Password:",
                txtPassword,
                130);

            CrearCampo(
                "Rol:",
                txtRol,
                180);

            // =====================================
            // BOTON
            // =====================================

            btnRegistrar.Size =
                new Size(120, 35);

            btnRegistrar.Location =
                new Point(100, 240);

            btnRegistrar.BackColor =
                Color.SaddleBrown;

            btnRegistrar.ForeColor =
                Color.White;

            btnRegistrar.FlatStyle =
                FlatStyle.Flat;

            btnRegistrar.Click +=
                Registrar;

            Controls.Add(
                btnRegistrar);
        }

        // =========================================
        // CREAR CAMPOS
        // =========================================

        private void CrearCampo(
            string texto,
            Control ctrlCampo,
            int y)
        {
            Controls.Add(
                new Label
                {
                    Text = texto,

                    Location =
                        new Point(
                            30,
                            y + 5),

                    AutoSize = true
                });

            ctrlCampo.Location =
                new Point(120, y);

            ctrlCampo.Width = 160;

            Controls.Add(ctrlCampo);
        }

        // =========================================
        // REGISTRAR
        // =========================================

        private void Registrar(
            object sender,
            EventArgs e)
        {
            try
            {
                ctrl.Registrar(
                    txtNombre.Text.Trim(),
                    txtUsuario.Text.Trim(),
                    txtPassword.Text,
                    txtRol.Text,
                    true);

                MessageBox.Show(
                    "Usuario registrado correctamente.");

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}