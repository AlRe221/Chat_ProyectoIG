using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;

namespace Chat_ProyectoIG
{
   

    public partial class Form1 : MaterialForm
    {
        TabControl generarEmojis = new TabControl();
        FlowLayoutPanel paracaritas = new FlowLayoutPanel();
        FlowLayoutPanel paraAnimalitos = new FlowLayoutPanel();
        FlowLayoutPanel paraComida = new FlowLayoutPanel();
        FlowLayoutPanel paraVarios = new FlowLayoutPanel();

        //cadena de conexión
        string connectionString = "server=127.0.0.1;uid=root;pwd=57057goku75@jua57;database=chat";
        public string UsuarioActual { get; set; }
        public int UsuarioActualId { get; set; }

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
            {":heart:", "❤"},
            {":dog:", "🐶"},
            {":cat:", "😺"},
            {":unicorn:", "🦄"},
            {":fox:", "🦊"},
            {":butterfly:", "🦋"},
            {":croissant:", "🥐"},
            {":avocato:", "🥑"},
            {":egg:", "🥚"},
            {":sandwich:", "🥪"}

        }; //diccionario para guardar los emojis y su id 

        Dictionary<string, List<string>> gruposConMiembros = new Dictionary<string, List<string>>();
        ContextMenuStrip menuReaccion = new ContextMenuStrip();
        Button botonModo;

        public Form1(string usuario, int usuarioId)
        {
            InitializeComponent();
            UsuarioActual = usuario;
            UsuarioActualId = usuarioId;

            // Configuración de MaterialSkin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Indigo500, Primary.Indigo700,
                Primary.Indigo100, Accent.LightBlue200,
                TextShade.WHITE

            );
            chatBox.Font = new Font("Seoge UI", 15, FontStyle.Bold);
            // generaEmoji(generarEmoji);
            generaEmoji(generarEmojis);

            memberList.SelectionMode = SelectionMode.MultiExtended;
            groupList.SelectedIndexChanged += GroupList_SelectedIndexChanged;

            InicializarMenuUsuario();
            InicializarMenuGrupo();

            Button botonMensajePrivado = new Button();
            botonMensajePrivado.Text = "Mensaje Privado";
            botonMensajePrivado.Size = new Size(100, 50);
            botonMensajePrivado.Font = new Font("Segoe UI", 8);
            botonMensajePrivado.BackColor = Color.FromArgb(33, 33, 33);
            botonMensajePrivado.ForeColor = Color.White;
            botonMensajePrivado.FlatStyle = FlatStyle.Flat;
            botonMensajePrivado.FlatAppearance.BorderColor = Color.LightBlue;
            botonMensajePrivado.Location = new Point(20, 200);

            botonMensajePrivado.Click += MensajePrivado_Click;

            panel1.Controls.Add(botonMensajePrivado);

            InicializarMenuReaccion();

            Button botonEnviarGrupo = new Button();
            botonEnviarGrupo.Text = "Enviar a Grupo";
            botonEnviarGrupo.Size = new Size(100, 50);
            botonEnviarGrupo.Font = new Font("Segoe UI", 9);
            botonEnviarGrupo.BackColor = Color.FromArgb(33, 33, 33);
            botonEnviarGrupo.ForeColor = Color.White;
            botonEnviarGrupo.FlatStyle = FlatStyle.Flat;
            botonEnviarGrupo.FlatAppearance.BorderColor = Color.LightBlue;
            botonEnviarGrupo.Location = new Point(20, 240); // Ajusta según tu layout

            botonEnviarGrupo.Click += EnviarMensajeAGrupo_Click;

            panel1.Controls.Add(botonEnviarGrupo);


            animacionPanel.Interval = 30;
            animacionPanel.Tick += AnimarPaneles;

            botonModo = new Button();
            botonModo.Font = new Font("Segoe UI", 9);
            botonModo.Text = "Modo Compacto";
            botonModo.Size = new Size(100, 35);
            botonModo.Location = new Point(20, 320);
            botonModo.Click += AlternarModo_Click;
            panel1.Controls.Add(botonModo);

            CargarUsuariosDesdeBD(); // Nuevo método
            CargarGruposDesdeBD();   // Nuevo método
            CargarMensajes();        // Método mejorado

        }
        ContextMenuStrip menuUsuario = new ContextMenuStrip();
        ContextMenuStrip menuGrupo = new ContextMenuStrip();
        Timer animacionPanel = new Timer();
        int pasoOpacidad = 10;
        bool ocultarPaneles = false;


        // Send chat message
        private async void SendButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputBox.Text))
            {
                string mensaje = inputBox.Text;
                await EnviarMensajeAnimado($"{UsuarioActual}: {mensaje}");
                GuardarMensajeEnBD(null, null, mensaje); // Mensaje general
                inputBox.Clear();
            }
        }

        // Add user to the member list
        private void AddUserButton_Click(object sender, EventArgs e)
        {
            string username = Microsoft.VisualBasic.Interaction.InputBox("Enter username:", "Add User", "NewUser");
            if (!string.IsNullOrWhiteSpace(username))
            {
                memberList.Items.Add(username);
            }
        }

        // Create a new group
        private void CreateGroupButton_Click(object sender, EventArgs e)
        {
            string groupName = Microsoft.VisualBasic.Interaction.InputBox("Enter group name:", "Create Group", "New Group");
            if (!string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show($"Group '{groupName}' created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Display selected member in input box
        private void MemberList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (memberList.SelectedItem != null)
                inputBox.Text = memberList.SelectedItem.ToString();
        }

        private void boton_agregar_Click(object sender, EventArgs e)
        {
            string usuarioInput = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingresa el USUARIO (debe ser único):", // CAMBIO: Especificar que es el usuario
                "Agregar Usuario",
                "NuevoUsuario"
            );

            if (string.IsNullOrWhiteSpace(usuarioInput))
            {
                MessageBox.Show("No se ingresó ningún usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si ya existe en la lista local
            foreach (string usuarioEnLista in memberList.Items)
            {
                if (usuarioEnLista.Equals(usuarioInput, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Ese usuario ya existe en la lista local.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            // Verificar si existe en la base de datos (por usuario)
            if (!UsuarioExisteEnBD(usuarioInput))
            {
                var respuesta = MessageBox.Show($"El usuario '{usuarioInput}' no existe en la base de datos. ¿Desea agregarlo solo localmente?",
                                              "Usuario no encontrado",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                {
                    return;
                }
            }

            memberList.Items.Add(usuarioInput);
            MessageBox.Show($"Usuario '{usuarioInput}' agregado con éxito.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool UsuarioExisteEnBD(string usuario)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // CAMBIO: Verificar por 'usuario' en lugar de 'nombre'
                    string query = "SELECT COUNT(*) FROM usuario WHERE usuario = @usuario";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar usuario: " + ex.Message);
                return false;
            }
        }

        private void chatBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            //necestio hacer que, cuando se escriba :elemojiquesea:, se transforme directamente al emoji. 
            string texto = inputBox.Text;
            if (texto.StartsWith(":"))
            {
                idEmoji.TryGetValue(texto, out string emoji); //buscara en el diccionario el emoji que coincida con el texto. 
                if (emoji != null)
                {
                    inputBox.Text = emoji;
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
                            inputBox.Text = texto;

                            // Ajustar índices para continuar después del emoji
                            aux = texto.Substring(j + 1);
                            j = 0;

                        }
                    }

                    i++;

                }

            }
            inputBox.SelectionStart = texto.Length;
            ResaltarMenciones();

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
            btn.Size = new Size(40, 40);
            btn.Text = char.ConvertFromUtf32(i); //convertir a emoji
            btn.Font = new Font("Segoe UI Emoji", 16); //fuente de emoji
            return btn;
        }

        private void boton_click(Button btn)
        {
            btn.Click += (s, e) =>
            {
                inputBox.Text += btn.Text; //agregar emoji al richtextbox
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
            generarEmojis.Size = new Size(600, 240); //estos valores van a cambiar en el proyecto
            generarEmojis.BackColor = Color.LightGray;
            generarEmojis.Visible = false;
            generarEmojis.Location = new Point(100, 350); //estos valores van a cambiar en el proyecto
            generarEmojis.Anchor = AnchorStyles.Right;


        }

        private TabPage crear_tappages(string nombre)
        {
            TabPage tabPage = new TabPage(nombre);
            return tabPage;

        }

        private void ResaltarMenciones()
        {
            int cursorPos = inputBox.SelectionStart;
            string texto = inputBox.Text;

            // Guardar el texto plano sin formato
            inputBox.TextChanged -= inputBox_TextChanged; // Evitar bucle de eventos
            inputBox.Text = texto;
            inputBox.SelectAll();
            inputBox.SelectionColor = Color.White;
            inputBox.SelectionFont = new Font("Segoe UI", 12, FontStyle.Regular);
            inputBox.DeselectAll();

            foreach (string usuario in memberList.Items)
            {
                string patron = "@" + usuario;
                int index = texto.IndexOf(patron);

                while (index != -1)
                {
                    inputBox.Select(index, patron.Length);
                    inputBox.SelectionColor = Color.LightSkyBlue; // Azul celeste
                    inputBox.SelectionFont = new Font("Segoe UI", 12, FontStyle.Italic);

                    index = texto.IndexOf(patron, index + patron.Length);
                }
            }

            inputBox.SelectionStart = cursorPos;
            inputBox.SelectionLength = 0;
            inputBox.TextChanged += inputBox_TextChanged; // Restaurar evento
        }

        private void memberList_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void GroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupList.SelectedItem == null) return;

            string grupoSeleccionado = groupList.SelectedItem.ToString();

            if (gruposConMiembros.ContainsKey(grupoSeleccionado))
            {
                miembrosGrupo.Items.Clear();

                foreach (string miembro in gruposConMiembros[grupoSeleccionado])
                {
                    miembrosGrupo.Items.Add(miembro);
                }
            }
            else
            {
                MessageBox.Show("Este grupo no tiene miembros asignados.", "Sin miembros", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void InicializarMenuUsuario()
        {
            menuUsuario.Items.Add("Editar nombre", null, EditarUsuario_Click);
            menuUsuario.Items.Add("Eliminar usuario", null, EliminarUsuario_Click);
            menuUsuario.Items.Add("Asignar a grupo", null, AsignarUsuarioAGrupo_Click);

            memberList.ContextMenuStrip = menuUsuario;
        }

        public void InicializarMenuGrupo()
        {
            menuGrupo.Items.Add("Renombrar grupo", null, RenombrarGrupo_Click);
            menuGrupo.Items.Add("Eliminar grupo", null, EliminarGrupo_Click);
            menuGrupo.Items.Add("Ver miembros", null, VerMiembrosGrupo_Click);

            groupList.ContextMenuStrip = menuGrupo;
        }

        private void EditarUsuario_Click(object sender, EventArgs e)
        {
            if (memberList.SelectedItem == null) return;

            string actual = memberList.SelectedItem.ToString();
            string nuevo = Microsoft.VisualBasic.Interaction.InputBox("Nuevo nombre:", "Editar Usuario", actual);

            if (!string.IsNullOrWhiteSpace(nuevo))
            {
                // Actualizar en memberList
                int index = memberList.SelectedIndex;
                memberList.Items[index] = nuevo;

                // Actualizar en todos los grupos
                foreach (var grupo in gruposConMiembros.Keys.ToList())
                {
                    var miembros = gruposConMiembros[grupo];
                    if (miembros.Contains(actual))
                    {
                        miembros.Remove(actual);
                        miembros.Add(nuevo);
                    }
                }

                MessageBox.Show($"Usuario '{actual}' renombrado a '{nuevo}' y actualizado en todos los grupos.", "Actualización completa");
            }
        }


        private void EliminarUsuario_Click(object sender, EventArgs e)
        {
            if (memberList.SelectedItem == null) return;

            string usuario = memberList.SelectedItem.ToString();
            var confirm = MessageBox.Show($"¿Eliminar al usuario '{usuario}' de la lista y de todos los grupos?", "Confirmar", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                memberList.Items.Remove(usuario);

                // Eliminar de todos los grupos
                foreach (var grupo in gruposConMiembros.Keys.ToList())
                {
                    gruposConMiembros[grupo].Remove(usuario);
                }

                MessageBox.Show($"Usuario '{usuario}' eliminado de la lista y de todos los grupos.", "Eliminación completa");
            }
        }


        private void AsignarUsuarioAGrupo_Click(object sender, EventArgs e)
        {
            if (memberList.SelectedItem == null || groupList.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un grupo en la lista para asignar el usuario.", "Falta selección");
                return;
            }

            string usuario = memberList.SelectedItem.ToString();
            string grupo = groupList.SelectedItem.ToString();

            if (!gruposConMiembros.ContainsKey(grupo))
                gruposConMiembros[grupo] = new List<string>();

            if (!gruposConMiembros[grupo].Contains(usuario))
            {
                gruposConMiembros[grupo].Add(usuario);
                MessageBox.Show($"Usuario '{usuario}' asignado al grupo '{grupo}'.", "Asignación exitosa");
            }
            else
            {
                MessageBox.Show("Ese usuario ya está en el grupo.", "Duplicado");
            }
        }

        private void RenombrarGrupo_Click(object sender, EventArgs e)
        {
            if (groupList.SelectedItem == null) return;

            string actual = groupList.SelectedItem.ToString();
            string nuevo = Microsoft.VisualBasic.Interaction.InputBox("Nuevo nombre:", "Renombrar Grupo", actual);

            if (!string.IsNullOrWhiteSpace(nuevo))
            {
                int index = groupList.SelectedIndex;
                groupList.Items[index] = nuevo;

                if (gruposConMiembros.ContainsKey(actual))
                {
                    var miembros = gruposConMiembros[actual];
                    gruposConMiembros.Remove(actual);
                    gruposConMiembros[nuevo] = miembros;
                }
            }
        }

        private void EliminarGrupo_Click(object sender, EventArgs e)
        {
            if (groupList.SelectedItem == null) return;

            var confirm = MessageBox.Show("¿Eliminar este grupo?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                string grupo = groupList.SelectedItem.ToString();
                groupList.Items.Remove(grupo);
                gruposConMiembros.Remove(grupo);

                // Limpia la visualización de miembros
                miembrosGrupo.Items.Clear();
            }
        }


        private void VerMiembrosGrupo_Click(object sender, EventArgs e)
        {
            if (groupList.SelectedItem == null) return;

            string grupo = groupList.SelectedItem.ToString();
            if (gruposConMiembros.ContainsKey(grupo))
            {
                var miembros = string.Join("\n", gruposConMiembros[grupo]);
                MessageBox.Show($"Miembros de '{grupo}':\n{miembros}", "Grupo");
            }
            else
            {
                MessageBox.Show("Este grupo no tiene miembros asignados.", "Grupo vacío");
            }
        }

        
        private async void MensajePrivado_Click(object sender, EventArgs e)
        {
            if (memberList.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un usuario para enviarle un mensaje privado.", "Sin selección");
                return;
            }

            if (string.IsNullOrWhiteSpace(inputBox.Text))
            {
                MessageBox.Show("Escribe un mensaje antes de enviarlo.", "Mensaje vacío");
                return;
            }

            string destinatario = memberList.SelectedItem.ToString();
            string mensaje = inputBox.Text;

            await EnviarMensajeAnimado($"[Privado a {destinatario}] {UsuarioActual}: {mensaje}");
            GuardarMensajeEnBD(destinatario, null, mensaje); // CORRECCIÓN: 3 parámetros
            inputBox.Clear();
        }

        public void InicializarMenuReaccion()
        {
            foreach (var emoji in new[] { "😂", "👍", "❤️", "😮", "😢", "🔥" })
            {
                menuReaccion.Items.Add(emoji, null, (s, e) => ReaccionarMensaje_Click(s, e, emoji));
            }

            chatBox.ContextMenuStrip = menuReaccion;

            chatBox.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    int index = chatBox.IndexFromPoint(e.Location);
                    if (index != ListBox.NoMatches)
                    {
                        chatBox.SelectedIndex = index;
                        menuReaccion.Show(chatBox, e.Location);
                    }
                }
            };
        }
        private void ReaccionarMensaje_Click(object sender, EventArgs e, string emoji)
        {
            int index = chatBox.SelectedIndex;
            if (index != -1)
            {
                string reaccion = $"   {emoji}";

                // Verifica si ya hay una reacción debajo
                if (index + 1 < chatBox.Items.Count && chatBox.Items[index + 1].ToString().Trim() == emoji)
                {
                    MessageBox.Show("Ya se ha agregado esta reacción.", "Duplicado");
                    return;
                }

                chatBox.Items.Insert(index + 1, reaccion);
            }
        }

        private async void EnviarMensajeAGrupo_Click(object sender, EventArgs e)
        {
            if (groupList.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un grupo para enviar el mensaje.", "Grupo no seleccionado");
                return;
            }

            if (string.IsNullOrWhiteSpace(inputBox.Text))
            {
                MessageBox.Show("Escribe un mensaje antes de enviarlo.", "Mensaje vacío");
                return;
            }

            string grupo = groupList.SelectedItem.ToString();
            string mensaje = inputBox.Text;

            await EnviarMensajeAnimado($"[Grupo: {grupo}] {UsuarioActual}: {mensaje}");
            GuardarMensajeEnBD(null, grupo, mensaje); // CORRECCIÓN: 3 parámetros
            inputBox.Clear();
        }

        private async Task EnviarMensajeAnimado(string mensaje)
        {
            chatBox.Items.Add(""); // Espacio para animación

            for (int i = 0; i <= mensaje.Length; i++)
            {
                chatBox.Items[chatBox.Items.Count - 1] = mensaje.Substring(0, i);
                await Task.Delay(25); // Velocidad de animación (ajustable)
            }
        }

        private void AnimarPaneles(object sender, EventArgs e)
        {
            foreach (Control panel in new[] { groupList, memberList, miembrosGrupo })
            {
                if (ocultarPaneles)
                {
                    panel.Visible = true;
                    panel.BackColor = Color.FromArgb(panel.BackColor.R, panel.BackColor.G, panel.BackColor.B, Math.Max(0, panel.BackColor.A - pasoOpacidad));
                    if (panel.BackColor.A <= 0)
                    {
                        panel.Visible = false;
                        animacionPanel.Stop();
                    }
                }
                else
                {
                    panel.Visible = true;
                    panel.BackColor = Color.FromArgb(panel.BackColor.R, panel.BackColor.G, panel.BackColor.B, Math.Min(255, panel.BackColor.A + pasoOpacidad));
                    if (panel.BackColor.A >= 255)
                    {
                        animacionPanel.Stop();
                    }
                }
            }
        }
        bool modoCompacto = false;

        private void AlternarModo_Click(object sender, EventArgs e)
        {
            modoCompacto = !modoCompacto;

            groupList.Visible = !modoCompacto;
            memberList.Visible = !modoCompacto;
            miembrosGrupo.Visible = !modoCompacto;

            botonModo.Text = modoCompacto ? "Modo Expandido" : "Modo Compacto";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //método para guardar mensaje

        private void Crear_Grupo_Click(object sender, EventArgs e)
        {
            string groupName = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingresa el nombre del grupo:",
                "Crear Grupo",
                "NuevoGrupo"
            );

            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show("No se ingresó ningún nombre de grupo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si ya existe en BD
            if (GrupoExisteEnBD(groupName))
            {
                MessageBox.Show("Ese grupo ya existe en la base de datos.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (memberList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecciona al menos un miembro en la lista para asignarlo al grupo.", "Sin miembros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> miembrosSeleccionados = new List<string>();
            foreach (var item in memberList.SelectedItems)
            {
                miembrosSeleccionados.Add(item.ToString());
            }

            // Guardar en BD
            if (GuardarGrupoEnBD(groupName, miembrosSeleccionados))
            {
                gruposConMiembros[groupName] = miembrosSeleccionados;
                groupList.Items.Add(groupName);
                MessageBox.Show($"Grupo '{groupName}' creado con {miembrosSeleccionados.Count} miembro(s).", "Grupo creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool GrupoExisteEnBD(string nombreGrupo)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM grupos WHERE nombre_grupo = @nombre";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", nombreGrupo);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar grupo: " + ex.Message);
                return false;
            }
        }

        private bool GuardarGrupoEnBD(string nombreGrupo, List<string> miembros)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Insertar grupo
                    string queryGrupo = "INSERT INTO grupos (nombre_grupo) VALUES (@nombre)";
                    MySqlCommand cmdGrupo = new MySqlCommand(queryGrupo, conn);
                    cmdGrupo.Parameters.AddWithValue("@nombre", nombreGrupo);
                    cmdGrupo.ExecuteNonQuery();

                    // Obtener ID del grupo insertado
                    long grupoId = cmdGrupo.LastInsertedId;

                    // CAMBIO: Buscar por 'usuario' en lugar de 'nombre'
                    string queryMiembro = "INSERT INTO miembros_grupo (id_grupo, id_usuario) VALUES (@grupoId, (SELECT id_usuario FROM usuario WHERE usuario = @usuario LIMIT 1))";

                    // Agregar al creador como miembro
                    MySqlCommand cmdCreador = new MySqlCommand(queryMiembro, conn);
                    cmdCreador.Parameters.AddWithValue("@grupoId", grupoId);
                    cmdCreador.Parameters.AddWithValue("@usuario", UsuarioActual); 
                    cmdCreador.ExecuteNonQuery();

                    // Agregar demás miembros
                    foreach (string miembro in miembros)
                    {
                        MySqlCommand cmdMiembro = new MySqlCommand(queryMiembro, conn);
                        cmdMiembro.Parameters.AddWithValue("@grupoId", grupoId);
                        cmdMiembro.Parameters.AddWithValue("@usuario", miembro); 
                        cmdMiembro.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar grupo en BD: " + ex.Message);
                return false;
            }
        }

        private void CargarMensajes()
        {
            try
            {
                chatBox.Items.Clear();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT m.remitente, m.destinatario, m.grupo, m.mensaje, m.fecha 
                           FROM mensajes m 
                           WHERE m.remitente = @usuario 
                              OR m.destinatario = @usuario 
                              OR m.destinatario IS NULL 
                              OR m.grupo IN (
                                  SELECT g.nombre_grupo 
                                  FROM grupos g 
                                  LEFT JOIN miembros_grupo mg ON g.id_grupo = mg.id_grupo 
                                  WHERE mg.id_usuario = @usuarioId
                              )
                           ORDER BY m.fecha ASC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuario", UsuarioActual);
                    cmd.Parameters.AddWithValue("@usuarioId", UsuarioActualId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string remitente = reader["remitente"].ToString();
                        string destinatario = reader["destinatario"] == DBNull.Value ? null : reader["destinatario"].ToString();
                        string grupo = reader["grupo"] == DBNull.Value ? null : reader["grupo"].ToString();
                        string mensaje = reader["mensaje"].ToString();

                        string mensajeFormateado;

                        if (!string.IsNullOrEmpty(grupo))
                            mensajeFormateado = $"[Grupo: {grupo}] {remitente}: {mensaje}";
                        else if (!string.IsNullOrEmpty(destinatario))
                        {
                            if (remitente == UsuarioActual)
                                mensajeFormateado = $"[Privado a {destinatario}] Tú: {mensaje}";
                            else
                                mensajeFormateado = $"[Privado] {remitente}: {mensaje}";
                        }
                        else
                            mensajeFormateado = $"{remitente}: {mensaje}";

                        chatBox.Items.Add(mensajeFormateado);
                    }

                    // Hacer scroll al final
                    if (chatBox.Items.Count > 0)
                        chatBox.TopIndex = chatBox.Items.Count - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar mensajes: " + ex.Message);
            }
        }

        private void CargarUsuariosDesdeBD()
        {
            try
            {
                memberList.Items.Clear();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // CAMBIO: Seleccionar 'usuario' en lugar de 'nombre'
                    string query = "SELECT usuario FROM usuario WHERE usuario != @usuarioActual";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuarioActual", UsuarioActual);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // CAMBIO: Agregar el usuario (único) en lugar del nombre
                        memberList.Items.Add(reader["usuario"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void CargarGruposDesdeBD()
        {
            try
            {
                groupList.Items.Clear();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT DISTINCT g.nombre_grupo 
                           FROM grupos g 
                           LEFT JOIN miembros_grupo mg ON g.id_grupo = mg.id_grupo 
                           WHERE mg.id_usuario = @usuarioId OR g.creador_id = @usuarioId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuarioId", UsuarioActualId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string grupo = reader["nombre_grupo"].ToString();
                        groupList.Items.Add(grupo);

                        // Cargar miembros del grupo en la estructura interna
                        CargarMiembrosGrupo(grupo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar grupos: " + ex.Message);
            }
        }

        private void CargarMiembrosGrupo(string nombreGrupo)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // CAMBIO: Seleccionar 'usuario' en lugar de 'nombre'
                    string query = @"SELECT u.usuario 
                           FROM miembros_grupo mg 
                           JOIN usuario u ON mg.id_usuario = u.id_usuario 
                           JOIN grupos g ON mg.id_grupo = g.id_grupo 
                           WHERE g.nombre_grupo = @grupo";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@grupo", nombreGrupo);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<string> miembros = new List<string>();
                    while (reader.Read())
                    {
                        miembros.Add(reader["usuario"].ToString());
                    }

                    gruposConMiembros[nombreGrupo] = miembros;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar miembros del grupo {nombreGrupo}: " + ex.Message);
            }
        }

        private void GuardarMensajeEnBD(string destinatario, string grupo, string mensaje)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO mensajes (remitente, destinatario, grupo, mensaje) VALUES (@remitente, @destinatario, @grupo, @mensaje)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@remitente", UsuarioActual);
                    cmd.Parameters.AddWithValue("@destinatario", string.IsNullOrEmpty(destinatario) ? DBNull.Value : (object)destinatario);
                    cmd.Parameters.AddWithValue("@grupo", string.IsNullOrEmpty(grupo) ? DBNull.Value : (object)grupo);
                    cmd.Parameters.AddWithValue("@mensaje", mensaje);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar mensaje: " + ex.Message);
            }
        }
    }
}
