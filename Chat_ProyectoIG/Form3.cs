using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_ProyectoIG
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public bool Login(string usuarioLogin, string contrasenaLogin, out int usuarioId)
        {
            usuarioId = -1;

            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=angelito_1422;database=chat"))
                {
                    conn.Open();

                    string query = "SELECT id_usuario, password FROM usuario WHERE usuario = @usuario";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuario", usuarioLogin);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        MessageBox.Show("Usuario no existe");
                        return false;
                    }

                    usuarioId = reader.GetInt32("id_usuario");
                    string contrasenaEncriptada = reader["password"].ToString();
                    reader.Close();

                    bool contrasenaCorrecta = VerificarContrasena(contrasenaLogin, contrasenaEncriptada);

                    if (!contrasenaCorrecta)
                    {
                        MessageBox.Show("Contraseña incorrecta");
                        usuarioId = -1;
                    }

                    return contrasenaCorrecta;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en login: " + ex.Message);
                usuarioId = -1;
                return false;
            }
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            string usuarioLogin = textBox1.Text; // Esto es el campo 'usuario'
            string constrasena = textBox2.Text;

            if (Login(usuarioLogin, constrasena, out int usuarioId))
            {
                // CAMBIO: Pasar usuarioLogin (que es el usuario único) en lugar del nombre
                Form1 formChat = new Form1(usuarioLogin, usuarioId);
                formChat.Show();
                this.Hide();
            }
        }

        private void linkLabel1_LinkClicked_1()
        {
            throw new NotImplementedException();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 formRegistro = new Form2();
            formRegistro.Show();
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
