using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_ProyectoIG
{
    public partial class Form1 : Form
    {
        TabControl generarEmojis = new TabControl();
        FlowLayoutPanel paracaritas = new FlowLayoutPanel();
        FlowLayoutPanel paraAnimalitos = new FlowLayoutPanel();
        FlowLayoutPanel paraComida = new FlowLayoutPanel();
        FlowLayoutPanel paraVarios = new FlowLayoutPanel();

        //preguntar al profe: tengo que poner todos los emojis que puse en mi boton emojis o solo algunos?

        Dictionary<string, string> idEmoji = new Dictionary<string, string>
        {
            {":smile:", "🙂"},
            {":sad:", "😞"},
            {":angry:", "😠"},
            {":rage:", "🤬"},
            {":heart_eyes:", "😍"},
            {":joy:", "😂"},
            {":shy:", "😳"},
            {":scream:", "😱"},
            {":sleeping:", "😴"},
            {":heart:", "❤️"},
            {":dog:", "🐶"},
            {":cat:", "😺"},
            {":unicorn:", "🦄"},
            {":fox:", "🦊"},
            {":butterfly", "🦋"},
            {":croissant", "🥐"},
            {":avocato:", "🥑"},
            {":egg:", "🥚"},
            {":sandwich:", "🥪"}

        }; //diccionario para guardar los emojis y su id 
        public Form1()
        {
            InitializeComponent();
            // generaEmoji(generarEmoji);
            generaEmoji(generarEmojis);
        }


        private void emoji_Click(object sender, EventArgs e)
        {
            generarEmojis.Visible = true; //se hace visible el cuadro de los emojis
            generarEmojis.BringToFront(); //se muestra al frente de todo

        }

        private void agregarCaritas()
        {
            paracaritas.Dock = DockStyle.Fill; //para que ocupe todo el espacio dentro del tabpage
            paracaritas.AutoScroll = true; //para que tenga scroll

            for (int i = 128512; i <= 128591; i++) //rango de emojis
            {
                Button btn = crear_boton(i); //creamos el boton con el emoji dentro
                boton_click(btn); //click del boton 

                paracaritas.Controls.Add(btn); //agregar el boton al floylayoutpanel para que se muestren, ya que en tap control no esta permitido
            }

        }

        private void agregarAnimalitos()
        {
            paraAnimalitos.Dock = DockStyle.Fill;
            paraAnimalitos.AutoScroll = true;

            for (int i = 129408; i <= 129448; i++) //rango de emojis
            {
                Button btn = crear_boton(i);
                boton_click(btn);

                paraAnimalitos.Controls.Add(btn);

            }

        }

        private void agregarVarios()
        {
            paraVarios.Dock = DockStyle.Fill;
            paraVarios.AutoScroll = true;

            for (int i = 9984; i <= 10175; i++) //rango de emojis
            {
                Button btn = crear_boton(i);
                boton_click(btn);

                paraVarios.Controls.Add(btn);
            }

        }
        private void agregarComida()
        {
            paraComida.Dock = DockStyle.Fill;
            paraComida.AutoScroll = true;

            for (int i = 129360; i <= 129391; i++) //rango de emojis
            {
                Button btn = crear_boton(i);
                boton_click(btn);

                paraComida.Controls.Add(btn);
            }

        }

        private Button crear_boton(int i)
        {
            Button btn = new Button();
            btn.Size = new Size(30, 30);
            btn.Text = char.ConvertFromUtf32(i); //convertir a emoji
            btn.Font = new Font("Segoe UI Emoji", 12); //fuente de emoji
            return btn;
        }

        private void boton_click(Button btn)
        {
            btn.Click += (s, e) =>
            {
                textoMensaje.Text += btn.Text; //agregar emoji al richtextbox
                generarEmojis.Visible = false;
            };
        }
        private void generaEmoji(TabControl generarEmojis)
        {
            crear_cuadro_emojis();

            TabPage caritas = crear_tappages("caras");
            TabPage animalitos = crear_tappages("animales");
            TabPage comida = crear_tappages("comida");
            TabPage varios = crear_tappages("varios");

            agregarCaritas(); //crear los botones de las caritas
            caritas.Controls.Add(paracaritas); //añadirlo al tappage
            agregarAnimalitos();
            animalitos.Controls.Add(paraAnimalitos);
            agregarComida();
            comida.Controls.Add(paraComida);
            agregarVarios();
            varios.Controls.Add(paraVarios);


            generarEmojis.Controls.Add(caritas); //agregar al tapcontrol. 
            generarEmojis.Controls.Add(animalitos);
            generarEmojis.Controls.Add(comida);
            generarEmojis.Controls.Add(varios);

            this.Controls.Add(generarEmojis);
        }

        private void crear_cuadro_emojis()
        {
            generarEmojis.Size = new Size(400, 200); //estos valores van a cambiar en el proyecto
            generarEmojis.BackColor = Color.LightGray;
            generarEmojis.Visible = false;
            generarEmojis.Location = new Point(350, 15); //estos valores van a cambiar en el proyecto

        }

        private TabPage crear_tappages(string nombre)
        {
            TabPage tabPage = new TabPage(nombre);
            return tabPage;

        }

        private void enviar_Click(object sender, EventArgs e)
        {
            cajadetexto.Text += textoMensaje.Text;
            cajadetexto.Text += "\n"; //salto de linea
            textoMensaje.Text = "";

        }

        private void textoMensaje_TextChanged(object sender, EventArgs e)
        {
            //necestio hacer que, cuando se escriba :elemojiquesea:, se transforme directamente al emoji. 
            string texto = textoMensaje.Text;
            if (texto.StartsWith(":"))
            {
                idEmoji.TryGetValue(texto, out string emoji); //buscara en el diccionario el emoji que coincida con el texto. 
                if (emoji != null)
                {
                    textoMensaje.Text = emoji;
                }

            }
            else
            {
                int i = 0;
                while (i < texto.Length - 1)
                {
                    string aux = "";

                    if (texto[i] == ' ' && texto[i + 1] == ':')
                    {
                        aux = texto.Substring(i + 1); //tomar desde el : hasta el final 
                        string subaux = "";
                        int j = 0;

                        while (j < aux.Length)
                        {
                            subaux += aux[j]; //se va formando la palabra
                            if (aux[j] == ':' && subaux.Length > 1)
                            {
                                break;
                            }
                            j++;
                        }
                        idEmoji.TryGetValue(subaux, out string emoji); //buscara en el diccionario el emoji que coincida con el texto. 
                        if (emoji != null)
                        {
                            int startIndex = i + 1; //es para identificar donde inician los : 
                            texto = texto.Substring(0, startIndex) + emoji + texto.Substring(startIndex + subaux.Length); //se concatena lo primero que no es emoji, con el emoji, con el demas texto si es que hay mas texto despues del emoji
                            textoMensaje.Text = texto;

                            // Ajustar índices para continuar después del emoji
                            aux = texto.Substring(j + 1);
                            j = 0;

                        }
                    }

                    i++;

                }

            }
            textoMensaje.SelectionStart = texto.Length;

        }
    }

        private void boton_agregar_Click(object sender, EventArgs e)
        {

        }

        private void Crear_Grupo_Click(object sender, EventArgs e)
        {

        }
    }
