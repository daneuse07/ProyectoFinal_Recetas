using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChefRecetasCS.Controllers;

namespace ChefRecetasCS.Views
{
    public partial class FrmLogin : Form
    {
        private TextBox txtUsuario = new TextBox();

        private TextBox txtPassword =
            new TextBox
            {
                UseSystemPasswordChar = true
            };

        private Button btnIngresar =
            new Button
            {
                Text = "Ingresar"
            };

        private Button btnRegistro =
            new Button
            {
                Text = "Registrarse"
            };

        private Label lblError =
            new Label
            {
                ForeColor = Color.Red
            };

        private Label lblIntentos =
            new Label
            {
                ForeColor = Color.DarkRed,
                AutoSize = true
            };

        private ProgressBar progressBar =
            new ProgressBar();

        private UsuarioController _ctrl =
            new UsuarioController();

        private int intentos = 0;

        // =====================================
        // CONSTRUCTOR
        // =====================================

        public FrmLogin()
        {
            Text = "Chef Estrella — Login";

            Size = new Size(380, 340);

            FormBorderStyle =
                FormBorderStyle.FixedDialog;

            MaximizeBox = false;

            StartPosition =
                FormStartPosition.CenterScreen;

            BackColor =
                Color.FromArgb(245, 235, 220);

            var lblTit =
                new Label
                {
                    Text = "Recetas Estrella",

                    Font =
                        new Font(
                            "Arial",
                            18,
                            FontStyle.Bold),

                    ForeColor =
                        Color.SaddleBrown,

                    AutoSize = true,

                    Location =
                        new Point(90, 20)
                };

            Agregar(
                "Usuario:",
                txtUsuario,
                70,
                120);

            Agregar(
                "Password:",
                txtPassword,
                110,
                120);

            // ===============================
            // BARRA PROGRESO
            // ===============================

            progressBar.Location =
                new Point(60, 150);

            progressBar.Size =
                new Size(240, 20);

            progressBar.Minimum = 0;

            progressBar.Maximum = 100;

            progressBar.Value = 0;

            // ===============================
            // LABEL INTENTOS
            // ===============================

            lblIntentos.Location =
                new Point(60, 180);

            lblIntentos.Text =
                "Intentos restantes: 3";

            // ===============================
            // BOTON INGRESAR
            // ===============================

            btnIngresar.Size =
                new Size(120, 35);

            btnIngresar.Location =
                new Point(115, 205);

            btnIngresar.BackColor =
                Color.FromArgb(56, 142, 60);

            btnIngresar.ForeColor =
                Color.White;

            btnIngresar.FlatStyle =
                FlatStyle.Flat;

            btnIngresar.Click +=
                BtnIngresar_Click;

            // ===============================
            // BOTON REGISTRO
            // ===============================

            btnRegistro.Size =
                new Size(120, 35);

            btnRegistro.Location =
                new Point(115, 245);

            btnRegistro.BackColor =
                Color.Peru;

            btnRegistro.ForeColor =
                Color.White;

            btnRegistro.FlatStyle =
                FlatStyle.Flat;

            btnRegistro.Click += (s, e) =>
            {
                new FrmRegistro()
                    .ShowDialog();
            };

            // ===============================
            // MENSAJES
            // ===============================

            lblError.AutoSize = true;

            lblError.Location =
                new Point(40, 290);

            Controls.AddRange(
                new Control[]
                {
                    lblTit,
                    progressBar,
                    lblIntentos,
                    btnIngresar,
                    btnRegistro,
                    lblError
                });

            AcceptButton =
                btnIngresar;
        }

        // =====================================
        // CREAR CONTROLES
        // =====================================

        private void Agregar(
            string texto,
            TextBox tb,
            int y,
            int xTb)
        {
            var lbl =
                new Label
                {
                    Text = texto,

                    AutoSize = true,

                    ForeColor =
                        Color.SaddleBrown,

                    Font =
                        new Font(
                            "Segoe UI",
                            10,
                            FontStyle.Bold),

                    Location =
                        new Point(40, y + 3)
                };

            tb.Location =
                new Point(xTb, y);

            tb.Width = 170;

            Controls.Add(lbl);
            Controls.Add(tb);
        }

        // =====================================
        // LOGIN
        // =====================================

        private async void BtnIngresar_Click(
            object sender,
            EventArgs e)
        {
            lblError.Text = "";

            var u =
                _ctrl.Login(
                    txtUsuario.Text.Trim(),
                    txtPassword.Text);

            if (u != null)
            {
                btnIngresar.Enabled = false;

                progressBar.Value = 0;

                // Animación de carga
                for (int i = 0; i <= 100; i += 5)
                {
                    progressBar.Value = i;

                    await Task.Delay(25);
                }

                lblError.Text =
                    "Acceso concedido.";

                await Task.Delay(300);

                var main =
                    new FrmMain(u);

                main.Show();

                Hide();
            }
            else
            {
                intentos++;

                progressBar.Value =
                    Math.Min(intentos * 34, 100);

                int restantes =
                    3 - intentos;

                lblIntentos.Text =
                    $"Intentos restantes: {Math.Max(restantes, 0)}";

                lblError.Text =
                    $"Credenciales incorrectas. Intento {intentos}/3";

                if (intentos >= 3)
                {
                    btnIngresar.Enabled = false;

                    txtUsuario.Enabled = false;

                    txtPassword.Enabled = false;

                    btnRegistro.Enabled = false;

                    lblError.Text =
                        "Acceso bloqueado por 30 segundos.";

                    await BloquearSistema();
                }
            }
        }

        // =====================================
        // BLOQUEO
        // =====================================

        private async Task BloquearSistema()
        {
            progressBar.Value = 0;

            for (int i = 30; i > 0; i--)
            {
                lblError.Text =
                    $"Sistema bloqueado. Espere {i} segundos.";

                progressBar.Value =
                    (int)(((30 - i) / 30.0) * 100);

                await Task.Delay(1000);
            }

            intentos = 0;

            progressBar.Value = 0;

            txtUsuario.Enabled = true;

            txtPassword.Enabled = true;

            btnIngresar.Enabled = true;

            btnRegistro.Enabled = true;

            lblIntentos.Text =
                "Intentos restantes: 3";

            lblError.Text =
                "Puede intentar nuevamente.";
        }
    }
}