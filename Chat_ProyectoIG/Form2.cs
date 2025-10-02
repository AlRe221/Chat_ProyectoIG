using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Chat_ProyectoIG
{
    public partial class Form2 : Form
    {
        private string password = "";
        private string password2 = "";
        private string nombre = "";
        private string usuario = "";
        private int id;

        public Form2()
        {
            InitializeComponent();
        }

        public int validarContrasena()
        {
            if (password == password2)
                return 1;
            else
                return 0;
        }
        // Método para encriptar 
        public string HashPassword(string password)
        {
            // Generar salt aleatorio
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Crear el hash con PBKDF2
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combinar salt y hash
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        // Método para verificar contraseña
        public bool VerificarContrasena(string password, string hashedPassword)
        {
            try
            {
                // Extraer bytes del hash almacenado
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);

                // Obtener salt
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);

                // Hash la contraseña ingresada con el mismo salt
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
                byte[] hash = pbkdf2.GetBytes(20);

                // Comparar los hashes
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

        // Método para verificar si el usuario existe
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

        // Método para limpiar los campos después del registro
        private void LimpiarCampos()
        {
            nombre = "";
            usuario = "";
            password = "";
            password2 = "";

            // Limpiar los TextBox si existen
            if (Nombre != null) Nombre.Text = "";
            if (Usuario != null) Usuario.Text = "";
            if (Pasword != null) Pasword.Text = "";
            if (PasswordConfir != null) PasswordConfir.Text = "";
        }

        private void Usuario_TextChanged_1(object sender, EventArgs e)
        {
            usuario = Usuario.Text;
        }

        private void registrar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Validar campos vacíos
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(usuario) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password2))
                {
                    MessageBox.Show("Por favor, complete todos los campos");
                    return;
                }

                // Validar longitud mínima de contraseña
                if (password.Length < 6)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres");
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=angelito_1422;database=chat"))
                {
                    conn.Open();

                    // Verificar si el usuario ya existe
                    if (ExisteUsuario(conn))
                    {
                        return;
                    }

                    if (validarContrasena() == 1)
                    {
                        string contrasenaEncriptada = HashPassword(password);

                        // SOLUCIÓN: Incluir todos los campos obligatorios
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
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            Form3 formLogin = new Form3();
            formLogin.Show();
            this.Hide();
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
