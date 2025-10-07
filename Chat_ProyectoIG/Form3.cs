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

        public bool Login(string usuarioLogin, string contrasenaLogin)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=S3bas!2025_DBchat;database=chat"))

               

                {
                    conn.Open();

                    string query = "SELECT password FROM usuario WHERE usuario = @usuario";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuario", usuarioLogin);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado == null || resultado == DBNull.Value)
                    {
                        MessageBox.Show("Usuario no existe");
                        return false;
                    }

                    string contrasenaEncriptada = resultado.ToString();
                    bool contrasenaCorrecta = VerificarContrasena(contrasenaLogin, contrasenaEncriptada);

                    if (!contrasenaCorrecta)
                        MessageBox.Show("Contraseña incorrecta");


                    return contrasenaCorrecta;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en login: " + ex.Message);
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
            string usuario = textBox1.Text;
            string constrasena = textBox2.Text;

            if (Login(usuario, constrasena))
            {
                Form1 formLogin = new Form1();
                formLogin.Show();
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
