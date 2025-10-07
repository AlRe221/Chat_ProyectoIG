using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Chat_ProyectoIG
{
    public partial class Form2 : Form
    {
        private string password = "";
        private string password2 = "";
        private string nombre = "";
        private string usuario = "";

        public Form2()
        {
            InitializeComponent();
        }

        public int validarContrasena()
        {
            return password == password2 ? 1 : 0;
        }

        public string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerificarContrasena(string password, string hashedPassword)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
                byte[] hash = pbkdf2.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ExisteUsuario(MySqlConnection conexion)
        {
            string checkQuery = "SELECT COUNT(*) FROM usuario WHERE usuario = @usuario";
            MySqlCommand checkCmd = new MySqlCommand(checkQuery, conexion);
            checkCmd.Parameters.AddWithValue("@usuario", usuario);

            int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (userCount > 0)
            {
                MessageBox.Show("El usuario ya existe");
                return true;
            }

            return false;
        }

        private void LimpiarCampos()
        {
            nombre = "";
            usuario = "";
            password = "";
            password2 = "";

            if (Nombre != null) Nombre.Text = "";
            if (Usuario != null) Usuario.Text = "";
            if (Pasword != null) Pasword.Text = "";
            if (PasswordConfir != null) PasswordConfir.Text = "";
        }

        private bool ProbarConexion()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=chatuser;pwd=S3bas!2025_DBchat;database=chat"))
                {
                    conn.Open();
                    conn.Close();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de conexión a la base de datos: " + ex.Message);
                return false;
            }
        }

        private void registrar_Click_1(object sender, EventArgs e)
        {
            if (!ProbarConexion()) return;

            try
            {
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(usuario) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password2))
                {
                    MessageBox.Show("Por favor, complete todos los campos");
                    return;
                }

                if (password.Length < 6)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres");
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=chatuser;pwd=S3bas!2025_DBchat;database=chat"))
                {
                    conn.Open();

                    if (ExisteUsuario(conn)) return;

                    if (validarContrasena() == 1)
                    {
                        string contrasenaEncriptada = HashPassword(password);

                        string query = "INSERT INTO usuario (nombre, usuario, password) VALUES (@nombre, @usuario, @password)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@password", contrasenaEncriptada);

                        int resultado = cmd.ExecuteNonQuery();

                        if (resultado > 0)
                        {
                            MessageBox.Show("Usuario registrado exitosamente");
                            LimpiarCampos();

                            Form3 formLogin = new Form3();
                            formLogin.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de MySQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general: " + ex.Message);
            }
        }

        private void Usuario_TextChanged_1(object sender, EventArgs e)
        {
            usuario = Usuario.Text;
        }

        private void Pasword_TextChanged(object sender, EventArgs e)
        {
            password = Pasword.Text;
        }

        private void PasswordConfir_TextChanged(object sender, EventArgs e)
        {
            password2 = PasswordConfir.Text;
        }

        private void Nombre_TextChanged(object sender, EventArgs e)
        {
            nombre = Nombre.Text;
        }
    }
}
