using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ChefRecetasCS.Models;

namespace ChefRecetasCS.Views
{
    public partial class FrmMain : Form
    {
        // =====================================
        // USUARIO LOGEADO
        // =====================================

        private readonly Usuario _usuario;

        // =====================================
        // CONSTRUCTOR
        // =====================================

        public FrmMain(Usuario u)
        {
            _usuario = u;

            Text =
                $"Chef Recetas — {_usuario.Nombre} [{_usuario.Rol}]";

            Size =
                new Size(1050, 720);

            StartPosition =
                FormStartPosition.CenterScreen;

            BackColor =
                Color.White;

            // =====================================
            // SIDEBAR
            // =====================================

            var sidebar =
                new Panel
                {
                    Dock = DockStyle.Left,
                    Width = 180,

                    BackColor =
                        Color.FromArgb(92, 64, 51)
                };

            // =====================================
            // PANEL CONTENIDO
            // =====================================

            var content =
                new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Beige
                };

            // =====================================
            // TITULO SIDEBAR
            // =====================================

            var lblTitulo =
                new Label
                {
                    Text = "RECETAS ESTRELLA",

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Segoe UI",
                            14,
                            FontStyle.Bold),

                    AutoSize = false,

                    TextAlign =
                        ContentAlignment.MiddleCenter,

                    Dock = DockStyle.Top,

                    Height = 60
                };

            sidebar.Controls.Add(lblTitulo);

            // =====================================
            // MODULOS POR ROL
            // =====================================

            List<string> modulos =
                new List<string>();

            if (_usuario.Rol == "admin")
            {
                modulos.Add("Categorías");
                modulos.Add("Ingredientes");
                modulos.Add("Recetas");
                modulos.Add("Reportes");
                modulos.Add("Usuarios");
            }
            else if (_usuario.Rol == "operador")
            {
                modulos.Add("Ingredientes");
                modulos.Add("Recetas");
                modulos.Add("Reportes");
            }
            else if (_usuario.Rol == "consultor")
            {
                modulos.Add("Recetas");
                modulos.Add("Reportes");
            }

            // =====================================
            // CREAR BOTONES
            // =====================================

            for (int i = 0; i < modulos.Count; i++)
            {
                var btn =
                    SideButton(modulos[i]);

                btn.Top =
                    70 + (i * 50);

                btn.Click += (s, e) =>
                {
                    content.Controls.Clear();

                    Form vista = null;

                    switch (((Button)s).Text)
                    {
                        case "Categorías":

                            vista =
                                new FrmCategorias(_usuario);

                            break;

                        case "Ingredientes":

                            vista =
                                new FrmIngredientes(_usuario);

                            break;

                        case "Recetas":

                            vista = new FrmRecetas(_usuario);
                            break;

                        case "Reportes":

                            vista = new FrmReportes(_usuario);
                            break;

                        case "Usuarios":

                            vista = new FrmUsuarios(_usuario);

                            break;
                    }

                    if (vista != null)
                    {
                        vista.TopLevel = false;

                        vista.FormBorderStyle =
                            FormBorderStyle.None;

                        vista.Dock =
                            DockStyle.Fill;

                        content.Controls.Add(vista);

                        vista.Show();
                    }
                };

                sidebar.Controls.Add(btn);
            }

            // =====================================
            // BOTON CERRAR SESION
            // =====================================

            var btnCerrar =
                new Button
                {
                    Text = "Cerrar Sesión",

                    Dock = DockStyle.Bottom,

                    Height = 50,

                    FlatStyle =
                        FlatStyle.Flat,

                    BackColor =
                        Color.Firebrick,

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Segoe UI",
                            10,
                            FontStyle.Bold)
                };

            btnCerrar.FlatAppearance.BorderSize = 0;

            btnCerrar.Click += (s, e) =>
            {
                Hide();

                var login =
                    new FrmLogin();

                login.Show();

                Close();
            };

            sidebar.Controls.Add(btnCerrar);

            // =====================================
            // VISTA INICIAL SEGUN ROL
            // =====================================

            Form inicio = null;

            if (_usuario.Rol == "admin")
            {
                inicio =
                    new FrmCategorias(_usuario);
            }
            else if (_usuario.Rol == "operador")
            {
                inicio =
                    new FrmIngredientes(_usuario);
            }
            else
            {
                inicio =
                    new FrmRecetas(_usuario);
            }

            inicio.TopLevel = false;

            inicio.FormBorderStyle =
                FormBorderStyle.None;

            inicio.Dock =
                DockStyle.Fill;

            content.Controls.Add(inicio);

            inicio.Show();

            // =====================================
            // AGREGAR CONTROLES
            // =====================================

            Controls.Add(content);

            Controls.Add(sidebar);

            // =====================================
            // CERRAR APP
            // =====================================

            FormClosing += (s, e) =>
            {
                if (Application.OpenForms.Count <= 1)
                {
                    Application.Exit();
                }
            };
        }

        // =====================================
        // BOTONES SIDEBAR
        // =====================================

        private Button SideButton(string texto)
        {
            var btn =
                new Button
                {
                    Text = texto,

                    Width = 160,

                    Height = 40,

                    Left = 10,

                    FlatStyle =
                        FlatStyle.Flat,

                    BackColor =
                        Color.FromArgb(160, 82, 45),

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Segoe UI",
                            10,
                            FontStyle.Bold),

                    Cursor =
                        Cursors.Hand
                };

            btn.FlatAppearance.BorderSize = 0;

            return btn;
        }
    }
}