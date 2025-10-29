using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Chat_ProyectoIG
{

    public class MensajeChat
    {
        public string Tipo { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string Grupo { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
    }

    public partial class Form1 : MaterialForm
    {
        TabControl generarEmojis = new TabControl();
        FlowLayoutPanel paracaritas = new FlowLayoutPanel();
        FlowLayoutPanel paraAnimalitos = new FlowLayoutPanel();
        FlowLayoutPanel paraComida = new FlowLayoutPanel();
        FlowLayoutPanel paraVarios = new FlowLayoutPanel();

        //conexión de cliente 
        private TcpClient tcpCliente;
        private NetworkStream serverStream;
        private bool servidorConectado = false;

        //cadena de conexión
        string connectionString = Config.ConnectionString;
        public string UsuarioActual { get; set; }
        public int UsuarioActualId { get; set; }

        //preguntar al profe: tengo que poner todos los emojis que puse en mi boton emojis o solo algunos?

        Dictionary<string, string> imagenEmoji = new Dictionary<string, string>
        {
            {":smile:", "emojis/smileEmoji.png"},
            {":sad:", "emojis/sadEmoji.png"},
            {":angry:", "emojis/angryFace.png"},
            {":rage:", "emojis/rageEmoji.png"},
            {":heart_eyes:", "emojis/heartEyesEmoji.png"},
            {":joy:", "emojis/joyEmoji.png"},
            {":shy:", "emojis/shyEmoji.png"},
            {":scream:", "emojis/screamFace.png"},
            {":sleeping:", "emojis/sleepingFace.png"},
            {":disgust:","emojis/disgustEmoji.png" },
            //animales
            {":dog:", "emojis/dogEmoji.png"},
            {":cat:", "emojis/catEmoji.png"},
            {":unicorn:", "emojis/unicornEmoji.png"},
            {":fox:", "emojis/foxEmoji.png"},
            {":butterfly:", "emojis/butterflyEmoji.png"},
            {":bear:","emojis/bearEmoji.png" },
            {":penguin:","emojis/penguinEmoji.png" },
            {":gorila:","emojis/gorilaEmoji.png" },
            {":fish:","emojis/fishEmoji.png" },
            {":bunny:","emojis/bunnyEmoji.png" },
            //comida
            {":croissant:", "emojis/crossaintEmoji.png"},
            {":avocato:", "emojis/avocadoEmoji.png"},
            {":egg:", "emojis/eggEmoji.png"},
            {":sandwich:", "emojis/sandwichEmoji.png"},
            {":pickle:", "emojis/pickleEmoji.png" },
            {":potato:","emojis/potatoEmoji.png" },
            {":carrot:","emojis/carrotEmoji.png" },
            {":bread:","emojis/breadEmoji.png" },
            {":cake:","emojis/cakeEmoji.png" },
            {"icecream:","emojis/icecreamEmoji.png" },
            //varios
            {":redheart:", "emojis/HeartEmoji.png"},
            {":blueheart:","emojis/blueheartEmoji.png" },
            {":purpleheart:","emojis/purpleheartEmoji.png" },
            {":yellowheart:","emojis/yellowheartEmoji.png" },
            {":greenheart:","emojis/greenheartEmoji.png" },
            {":star:","emojis/starEmoji.png" },
            {":moon:","emojis/moonEmoji.png" },
            {":sun:","emojis/sunEmoji.png" },
            {":scissors:","emojis/scissorsEmoji.png" },
            {":rainbow:","emojis/rainbowEmoji.png" }


        }; //diccionario para guardar los emojis y su ruta de acceso


        Dictionary<string, string> emojiCategorias = new Dictionary<string, string>
        {
            {":smile:", "caritas"},
            {":sad:", "caritas"},
            {":angry:", "caritas"},
            {":rage:", "caritas"},
            {":heart_eyes:", "caritas"},
            {":joy:", "caritas"},
            {":shy:", "caritas"},
            {":scream:", "caritas"},
            {":sleeping:", "caritas"},
            {":disgust:","caritas" },
            //animales
            {":dog:", "animales"},
            {":cat:", "animales"},
            {":unicorn:", "animales"},
            {":fox:", "animales"},
            {":butterfly:", "animales"},
            {":bear:","animales" },
            {":penguin:","animales" },
            {":gorila:","animales" },
            {":fish:","animales" },
            {":bunny:","animales" },
            //comida
            {":croissant:", "comida"},
            {":avocato:", "comida"},
            {":egg:", "comida"},
            {":sandwich:", "comida"},
            {":pickle:", "comida" },
            {":potato:","comida" },
            {":carrot:","comida" },
            {":bread:","comida" },
            {":cake:","comida" },
            {"icecream:","comida" },
            //varios
            {":redheart:", "varios"},
            {":blueheart:","varios" },
            {":purpleheart:","varios" },
            {":yellowheart:","varios" },
            {":greenheart:","varios" },
            {":star:","varios" },
            {":moon:","varios" },
            {":sun:","varios" },
            {":scissors:","varios" },
            {":rainbow:","varios" }
        }; //identificar a que tapPage va

        // REEMPLAZA ESTO EN TU CLASE Form1
        Dictionary<string, string> idEmoji = new Dictionary<string, string>
        {
            // Caritas
            {":smile:", "\U0001F642"}, // 🙂
            {":sad:", "\U0001F625"},   // 😞
            {":angry:", "\U0001F620"}, // 😠
            {":rage:", "\U0001F92C"},  // 🤬 (Código: 1F92C)
            {":heart_eyes:", "\U0001F60D"}, // 😍
            {":joy:", "\U0001F602"},   // 😂
            {":shy:", "\U0001F633"},   // 😳
            {":scream:", "\U0001F631"}, // 😱
            {":sleeping:", "\U0001F634"}, // 😴
            {":disgust:", "\U0001F922"}, // 🤢
    
            // Animales
            {":dog:", "\U0001F436"},   // 🐶
            {":cat:", "\U0001F63A"},   // 😺
            {":unicorn:", "\U0001F984"}, // 🦄
            {":fox:", "\U0001F98A"},   // 🦊
            {":butterfly:", "\U0001F98B"}, // 🦋
            {":bear:", "\U0001F43B"},   // 🐻
            {":penguin:", "\U0001F427"}, // 🐧
            {":gorila:", "\U0001F98D"}, // 🦍
            {":fish:", "\U0001F41F"},   // 🎣 (Nota: Usaste la caña de pescar, pero el código es este)
            {":bunny:", "\U0001F430"},  // 🐰
    
            // Comida
            {":croissant:", "\U0001F950"}, // 🥐
            {":avocato:", "\U0001F951"},  // 🥑
            {":egg:", "\U0001F95A"},     // 🥚
            {":sandwich:", "\U0001F96A"}, // 🥪
            {":pickle:", "\U0001F952"},   // 🥒
            {":potato:", "\U0001F954"},   // 🥔
            {":carrot:", "\U0001F955"},   // 🥕
            {":bread:", "\U0001F35E"},    // 🍞
            {":cake:", "\U0001F382"},     // 🎂
            {"icecream:", "\U0001F368"}, // 🍦 (Corregí tu clave, era "icecream")
    
            // Varios
            {":redheart:", "\U00002764\U0000FE0F"}, // ❤️ (requiere un VARIATION SELECTOR)
            {":blueheart:", "\U0001F499"}, // 💙
            {":purpleheart:", "\U0001F49C"}, // 💜
            {":yellowheart:", "\U0001F49B"}, // 💛
            {":greenheart:", "\U0001F49A"}, // 💚
            {":star:", "\U00002B50"},      // ⭐
            {":moon:", "\U0001F315"},      // 🌕
            {":sun:", "\U00002600\U0000FE0F"}, // ☀️ (requiere un VARIATION SELECTOR)
            {":scissors:", "\U00002702\U0000FE0F"}, // ✂️ (requiere un VARIATION SELECTOR)
            {":rainbow:", "\U0001F308"}     // 🌈
        };

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
            // generaEmoji(generarEmoi);
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


            //Implementación Servidor - Cliente

            _ = Task.Run(() => conexionClienteServidor());

        }
        ContextMenuStrip menuUsuario = new ContextMenuStrip();
        ContextMenuStrip menuGrupo = new ContextMenuStrip();
        Timer animacionPanel = new Timer();
        int pasoOpacidad = 10;
        bool ocultarPaneles = false;

        #region Métodos de Conexión Servidor

        private async Task conexionClienteServidor()
        {
            string serverIP = Config.ChatServer;
            int serverPort = Config.ChatPort;

            try
            {
                this.tcpCliente = new TcpClient();
                await this.tcpCliente.ConnectAsync(serverIP, serverPort);
                serverStream = tcpCliente.GetStream();
                servidorConectado = true;

                // Enviar mensaje de login al servidor
                await EnviarLoginAlServidor();

                // Iniciar escucha de mensajes del servidor
                _ = Task.Run(() => EscucharServidor());

                // Mostrar mensaje en el chat
                this.Invoke(new Action(() =>
                {
                    chatBox.Items.Add("✓ Conectado al servidor");
                    chatBox.TopIndex = chatBox.Items.Count - 1;
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show($"Error conectando al servidor: {ex.Message}");
                }));
            }
        }

        private async Task EnviarLoginAlServidor()
        {
            if (!servidorConectado) return;

            try
            {
                var mensajeLogin = new
                {
                    Tipo = "login",
                    Remitente = UsuarioActual,
                    Destinatario = (string)null,
                    Grupo = (string)null,
                    Contenido = "Conectándose al chat",
                    Fecha = DateTime.Now
                };

                string json = JsonConvert.SerializeObject(mensajeLogin);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                await serverStream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error enviando login: {ex.Message}");
            }
        }

        private async Task EscucharServidor()
        {
            byte[] buffer = new byte[4096];
            int bytesRead;

            try
            {
                while (servidorConectado && (bytesRead = await serverStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string mensajeJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    this.Invoke(new Action(() =>
                    {
                        ProcesarMensajeRecibido(mensajeJson);
                    }));
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    chatBox.Items.Add($"✗ Error de conexión con el servidor: {ex.Message}");
                    servidorConectado = false;
                }));
            }
        }

        private void ProcesarMensajeRecibido(string mensajeJson)
        {
            try
            {
                var mensaje = JsonConvert.DeserializeObject<MensajeChat>(mensajeJson);

                switch (mensaje.Tipo)
                {
                    case "publico":
                    case "grupo":
                        chatBox.Items.Add($"{mensaje.Remitente}: {mensaje.Contenido}");
                        break;
                    case "privado":
                        chatBox.Items.Add($"[Privado] {mensaje.Remitente}: {mensaje.Contenido}");
                        break;
                    case "usuario_conectado":
                    case "usuario_desconectado":
                        chatBox.Items.Add($"⚡ {mensaje.Contenido}");
                        break;
                    case "error":
                        chatBox.Items.Add($"❌ {mensaje.Contenido}");
                        break;
                    case "login_confirmado":
                        chatBox.Items.Add($"✓ {mensaje.Contenido}");
                        break;
                }

                // Scroll al final
                chatBox.TopIndex = chatBox.Items.Count - 1;
            }
            catch (Exception ex)
            {
                chatBox.Items.Add($"Error procesando mensaje: {ex.Message}");
            }
        }

        private async Task EnviarMensajeAlServidor(string tipo, string destinatario, string grupo, string contenido)
        {
            if (!servidorConectado || tcpCliente?.Connected != true)
            {
                MessageBox.Show("No hay conexión con el servidor");
                return;
            }

            try
            {
                var mensaje = new
                {
                    Tipo = tipo,
                    Remitente = UsuarioActual,
                    Destinatario = destinatario,
                    Grupo = grupo,
                    Contenido = contenido,
                    Fecha = DateTime.Now
                };

                string json = JsonConvert.SerializeObject(mensaje);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                await serverStream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error enviando mensaje: {ex.Message}");
            }
        }

        #endregion


        // Send chat message
        private async void SendButton_Click(object sender, EventArgs e)
        {
            string mensajeEnviado = inputBox.Text;
            if (!string.IsNullOrWhiteSpace(inputBox.Text))
            {
                mensajeEnviado = mensajeEnviado.Replace("\u0001", string.Empty);
                mensajeEnviado = mensajeEnviado.Replace("\u0002", string.Empty);

                // **NUEVO:** Limpiar otros posibles caracteres de control al principio/final
                mensajeEnviado = mensajeEnviado.Trim();

                // 1. Sustituir todos los atajos por sus caracteres Unicode
                foreach (var par in idEmoji)
                {
                    // Reemplazar todas las ocurrencias en el texto
                    mensajeEnviado = mensajeEnviado.Replace(par.Key, par.Value);
                }
                await EnviarMensajeAnimado($"{UsuarioActual}: {mensajeEnviado}");
                GuardarMensajeEnBD(null, null, mensajeEnviado); // Mensaje general
                await EnviarMensajeAlServidor("publico", null, null, mensajeEnviado);
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

        private static bool isAwaitingSubstitution = false;
        private static bool isReconstructing = false;
        // Asegúrate de que tu diccionario 'imagenEmoji' esté declarado a nivel de clase también.
        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            // Si estamos reconstruyendo, salimos inmediatamente para evitar recursividad.
            if (isReconstructing) return;

            string texto = inputBox.Text;
            isAwaitingSubstitution = false;

            foreach (var par in imagenEmoji)
            {
                string atajo = par.Key;

                // Buscamos si el atajo está al final y fue la última cosa escrita
                int index = -1;
                if (texto.Length >= atajo.Length)
                {
                    if (texto.EndsWith(atajo)) index = texto.Length - atajo.Length;
                    else if (texto.EndsWith(atajo + " ")) index = texto.Length - atajo.Length - 1; // Para atajos seguidos de un espacio

                    if (index >= 0)
                    {
                        // Aseguramos que haya un espacio antes, o que esté al inicio del texto.
                        if (index == 0 || texto[index - 1] == ' ')
                        {
                            isAwaitingSubstitution = true; // El emoji está listo para ser sustituido
                            break;
                        }
                    }
                }
            }
            ResaltarMenciones();
        }
        private void inputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo actuamos si se está esperando una sustitución y la tecla es un espacio o un Enter.
            if (!isAwaitingSubstitution) return;

            if (e.KeyChar == ' ' || e.KeyChar == (char)Keys.Enter)
            {
                // Consumir la tecla para que no se inserte en el texto.
                e.Handled = true;
                isAwaitingSubstitution = false; // Bajamos la bandera

                // Ejecutamos la sustitución y reconstrucción en el método auxiliar.
                SustituirEmojiYReconstruir(e.KeyChar);
            }
        }

        private void SustituirEmojiYReconstruir(char teclaDisparo)
        {
            isReconstructing = true;

            string texto = inputBox.Text;
            Color bgColor = inputBox.BackColor;
            int emojiSize = 18;

            string atajoEncontrado = null;
            string rutaImagenEncontrada = null;
            int indexSustitucion = -1;

            try
            {
                // 1. RE-BUSCAMOS el atajo final
                foreach (var par in imagenEmoji)
                {
                    string atajo = par.Key;
                    string rutaImagen = par.Value;

                    if (texto.EndsWith(atajo))
                    {
                        indexSustitucion = texto.Length - atajo.Length;

                        if (indexSustitucion == 0 || texto[indexSustitucion - 1] == ' ')
                        {
                            atajoEncontrado = atajo;
                            rutaImagenEncontrada = rutaImagen;
                            break;
                        }
                    }
                }

                if (atajoEncontrado != null)
                {
                    // 2. PREPARACIÓN DE LA IMAGEN
                    Image img = null;
                    try { img = Image.FromFile(rutaImagenEncontrada); } catch { return; }

                    Bitmap finalImage = new Bitmap(emojiSize, emojiSize);
                    using (Graphics g = Graphics.FromImage(finalImage))
                    {
                        g.Clear(bgColor);
                        g.DrawImage(img, 0, 0, emojiSize, emojiSize);
                    }

                    Clipboard.SetImage(finalImage);

                    // 3. SUSTITUCIÓN ASÍNCRONA (CRÍTICO)

                    // CRÍTICO 1: Forzar el foco antes de BeginInvoke
                    inputBox.Focus();

                    // Usamos BeginInvoke para garantizar que la operación de pegar ocurra después de que
                    // el control haya terminado de procesar KeyPress.
                    inputBox.BeginInvoke(new Action(() =>
                    {
                        // Aseguramos que el control aún tiene el foco
                        inputBox.Focus();

                        // 3.1. Seleccionar el atajo COMPLETO para que la imagen lo reemplace
                        inputBox.SelectionStart = indexSustitucion;
                        inputBox.SelectionLength = atajoEncontrado.Length;

                        // Reemplazar atajo con la imagen (la imagen toma 1 posición)
                        inputBox.Paste();

                        // 3.2. INSERCIÓN DEL ATAJO OCULTO Y LA TECLA DE DISPARO

                        int ultimoIndex = indexSustitucion + 1; // Índice después de la imagen

                        // Insertar atajo OCULTO
                        inputBox.SelectionStart = ultimoIndex;
                        inputBox.SelectionLength = 0;

                        inputBox.SelectionFont = new Font(inputBox.Font.Name, 1f);
                        inputBox.SelectionColor = bgColor;
                        inputBox.SelectedText = atajoEncontrado;

                        // Restablecer formato normal
                        inputBox.SelectionFont = inputBox.Font;
                        inputBox.SelectionColor = inputBox.ForeColor;

                        ultimoIndex += atajoEncontrado.Length;

                        // Insertar la tecla que disparó el evento (Espacio o Enter)
                        inputBox.SelectionStart = ultimoIndex;
                        inputBox.SelectionLength = 0;
                        inputBox.SelectedText = teclaDisparo.ToString();
                        ultimoIndex += 1;

                        Clipboard.Clear();

                        // RESTAURACIÓN FINAL DENTRO DEL INVOKE
                        inputBox.SelectionStart = ultimoIndex;
                        inputBox.SelectionLength = 0;
                        inputBox.Focus();

                        // Permitimos que TextChanged vuelva a funcionar.
                        isReconstructing = false;
                    }));

                    // 🚨 Importante: Salir del método principal, la lógica continúa en el BeginInvoke.
                    return;
                }
            }
            finally
            {
                // Si no se encontró el atajo (if (atajoEncontrado != null) fue false), 
                // desactivamos la bandera aquí. Si se encontró, se desactiva dentro del BeginInvoke.
                if (atajoEncontrado == null)
                {
                    isReconstructing = false;
                }
            }
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

            foreach (var emojiItem in imagenEmoji) // Iteramos sobre TODOS los emojis
            {
                string claveEmoji = emojiItem.Key;
                string rutaImagen = emojiItem.Value;

                // VERIFICAMOS si pertenece a la categoría "caritas"
                if (emojiCategorias.TryGetValue(claveEmoji, out string categoria) && categoria == "caritas")
                {
                    // Creamos y añadimos SÓLO las caritas
                    Button btn = crear_boton_imagen(claveEmoji, rutaImagen);
                    paracaritas.Controls.Add(btn);
                }
            }

        }

        private void agregarAnimalitos()
        {
            paraAnimalitos.Dock = DockStyle.Fill;
            paraAnimalitos.AutoScroll = true;

            foreach (var emojiItem in imagenEmoji)
            {
                string claveEmoji = emojiItem.Key;
                string rutaImagen = emojiItem.Value;

                // Filtramos por "animales"
                if (emojiCategorias.TryGetValue(claveEmoji, out string categoria) && categoria == "animales")
                {
                    Button btn = crear_boton_imagen(claveEmoji, rutaImagen);
                    paraAnimalitos.Controls.Add(btn);
                }
            }

        }

        private void agregarVarios()
        {
            paraVarios.Dock = DockStyle.Fill;
            paraVarios.AutoScroll = true;

            foreach (var emojiItem in imagenEmoji)
            {
                string claveEmoji = emojiItem.Key;
                string rutaImagen = emojiItem.Value;

                // Filtramos por "animales"
                if (emojiCategorias.TryGetValue(claveEmoji, out string categoria) && categoria == "varios")
                {
                    Button btn = crear_boton_imagen(claveEmoji, rutaImagen);
                    paraVarios.Controls.Add(btn);
                }
            }

        }

        private void agregarComida()
        {
            paraComida.Dock = DockStyle.Fill;
            paraComida.AutoScroll = true;

            foreach (var emojiItem in imagenEmoji)
            {
                string claveEmoji = emojiItem.Key;
                string rutaImagen = emojiItem.Value;

                // Filtramos por "animales"
                if (emojiCategorias.TryGetValue(claveEmoji, out string categoria) && categoria == "comida")
                {
                    Button btn = crear_boton_imagen(claveEmoji, rutaImagen);
                    paraComida.Controls.Add(btn);
                }
            }

        }

     

        private Button crear_boton_imagen(string claveEmoji, string rutaImagen)
        {
            Button btn = new Button();
            btn.Size = new Size(60, 60);
            btn.Tag = claveEmoji;

            try
            {
                btn.BackgroundImage = Image.FromFile(rutaImagen);
                btn.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"ERROR AL GENERAR IMAGEN {ex}");
            }

            boton_click(btn);
            return btn;
        }

        private void boton_click(Button btn)
        {
            btn.Click += (s, e) =>
            {
                string atajo = btn.Tag.ToString(); // Obtiene el atajo (ej: ":smile:")

                if (imagenEmoji.TryGetValue(atajo, out string rutaImagen))
                {
                    Image img = null;
                    try { img = Image.FromFile(rutaImagen); } catch { return; }

                    // Variables de estilo
                    Color bgColor = inputBox.BackColor;
                    int emojiSize = 18;

                    // --- 1. Preparar la Imagen ---
                    Bitmap finalImage = new Bitmap(emojiSize, emojiSize);
                    using (Graphics g = Graphics.FromImage(finalImage))
                    {
                        g.Clear(bgColor);
                        g.DrawImage(img, 0, 0, emojiSize, emojiSize);
                    }

                    // --- 2. Insertar la IMAGEN Visible ---
                    int originalStart = inputBox.SelectionStart;
                    inputBox.SelectionLength = 0;
                    inputBox.SelectedText = " "; // Insertar un espacio

                    inputBox.SelectionStart = originalStart;
                    inputBox.SelectionLength = 1; // Seleccionar el espacio

                    Clipboard.SetImage(finalImage);
                    inputBox.Paste(); // La imagen se pega
                    Clipboard.Clear();

                    // --- 3. Insertar el ATAJO DE TEXTO OCULTO CON MARCADORES ---

                    // Posición después de la imagen (la imagen ocupa 1 carácter invisible)
                    int index = originalStart + 1;
                    inputBox.SelectionStart = index;
                    inputBox.SelectionLength = 0;

                    // 🚨 CAMBIO CLAVE: Envuelve el atajo con marcadores de aislamiento
                    string atajoMarcado = "\u0001" + atajo + "\u0002"; // Ejemplo: "\u0001:smile:\u0002"

                    // Aplicar formato para ocultar el texto
                    inputBox.SelectionFont = new Font(inputBox.Font.Name, 1f);
                    inputBox.SelectionColor = bgColor; // Mismo color que el fondo

                    // Insertar la cadena marcada
                    inputBox.SelectedText = atajoMarcado;

                    // Restablecer el formato normal para el texto que sigue
                    inputBox.SelectionFont = inputBox.Font;
                    inputBox.SelectionColor = inputBox.ForeColor;

                    // Mover el cursor al final de la inserción
                    inputBox.SelectionStart = index + atajoMarcado.Length;
                }

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

        /*   private void ResaltarMenciones()
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
           }*/


        private void ResaltarMenciones()
        {
            // CRÍTICO: No ejecutar mientras el emoji se está procesando.
            if (isReconstructing) return;

            // Guardamos la posición del cursor
            int cursorPosition = inputBox.SelectionStart;
            string texto = inputBox.Text;

            // Desactivamos el evento TextChanged para evitar recursión.
            inputBox.TextChanged -= inputBox_TextChanged;

            try
            {
                // NO HAREMOS LIMPIEZA TOTAL PARA NO ROMPER EL EMOJI OCULTO.

                int startIndex = 0;

                while (startIndex < texto.Length)
                {
                    // 1. Busca el '@'
                    int mencionesIndex = texto.IndexOf('@', startIndex);

                    if (mencionesIndex == -1) break;

                    // 2. Busca el final de la palabra
                    int endIndex = texto.IndexOf(' ', mencionesIndex);
                    if (endIndex == -1) endIndex = texto.Length;

                    // Solo resaltamos si hay caracteres después del '@' (ej. @a vs @)
                    if (endIndex > mencionesIndex + 1)
                    {
                        int length = endIndex - mencionesIndex;

                        // 3. Aplicar el formato de mención
                        inputBox.SelectionStart = mencionesIndex;
                        inputBox.SelectionLength = length;

                        inputBox.SelectionColor = Color.LightBlue; // Color de mención
                        inputBox.SelectionFont = new Font(inputBox.Font.Name, inputBox.Font.Size, FontStyle.Bold);

                        // Mover el punto de inicio para la próxima búsqueda
                        startIndex = endIndex;
                    }
                    else
                    {
                        // Si solo encontramos '@' sin texto, pasamos al siguiente carácter.
                        startIndex = mencionesIndex + 1;
                    }
                }

                // 🚨 CAMBIO CLAVE: Reasegurar el formato de la posición actual del cursor 🚨
                // Esto previene que las menciones afecten lo que se escribe después.
                inputBox.SelectionStart = texto.Length;
                inputBox.SelectionLength = 0;
                inputBox.SelectionColor = inputBox.ForeColor;
                inputBox.SelectionFont = inputBox.Font;
            }
            finally
            {
                // Restaurar el cursor y reactivar eventos
                inputBox.SelectionStart = Math.Min(cursorPosition, inputBox.Text.Length);
                inputBox.SelectionLength = 0;
                inputBox.Focus();

                inputBox.TextChanged += inputBox_TextChanged;
            }
        }




        private void memberList_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void GroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupList.SelectedItem == null)
            {
                miembrosGrupo.Items.Clear();
                CargarMensajes(null);
                return;

            }

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

            CargarMensajes(grupoSeleccionado);
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


        /*private async void MensajePrivado_Click(object sender, EventArgs e)
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
        }*/

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

            // Obtener el texto visible, ignorando el atajo oculto.
            string mensajeEnviado = inputBox.Text;

            // Limpieza de caracteres de control
            mensajeEnviado = mensajeEnviado.Replace("\u0001", string.Empty);
            mensajeEnviado = mensajeEnviado.Replace("\u0002", string.Empty);
            mensajeEnviado = mensajeEnviado.Trim();

            // Sustituir atajos por caracteres Unicode (como en SendButton_Click)
            foreach (var par in idEmoji)
            {
                mensajeEnviado = mensajeEnviado.Replace(par.Key, par.Value);
            }

            // Enviar y guardar
            await EnviarMensajeAnimado($"[Privado a {destinatario}] {UsuarioActual}: {mensajeEnviado}");
            GuardarMensajeEnBD(destinatario, null, mensajeEnviado);
            //Enviar al servidor
            await EnviarMensajeAlServidor("privado", destinatario, null, mensajeEnviado);

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

        /* private async void EnviarMensajeAGrupo_Click(object sender, EventArgs e)
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
         }*/

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

            // 1. Obtener el texto visible, ignorando el atajo oculto.
            string mensajeEnviado = inputBox.Text;

            // 2. Lógica de Limpieza y Sustitución (como en SendButton_Click)

            // Limpieza de caracteres de control
            mensajeEnviado = mensajeEnviado.Replace("\u0001", string.Empty);
            mensajeEnviado = mensajeEnviado.Replace("\u0002", string.Empty);
            mensajeEnviado = mensajeEnviado.Trim();

            // Sustituir atajos por caracteres Unicode (usando el diccionario idEmoji)
            foreach (var par in idEmoji)
            {
                mensajeEnviado = mensajeEnviado.Replace(par.Key, par.Value);
            }

            // 3. Enviar y guardar
            await EnviarMensajeAnimado($"[Grupo: {grupo}] {UsuarioActual}: {mensajeEnviado}");
            GuardarMensajeEnBD(null, grupo, mensajeEnviado); // El mensaje general a grupo usa el grupo como segundo parámetro
            //Enviar al servidor
            await EnviarMensajeAlServidor("grupo", null, grupo, mensajeEnviado);
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

        private void CargarMensajes(string grupoFiltro = null)
        {
            try
            {
                chatBox.Items.Clear();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;
                    MySqlCommand cmd;

                    if (!string.IsNullOrEmpty(grupoFiltro))
                    {
                        query = @"SELECT m.remitente, m.destinatario, m.grupo, m.mensaje, m.fecha 
                          FROM mensajes m 
                          WHERE m.grupo = @grupo
                          ORDER BY m.fecha ASC";
                        cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@grupo", grupoFiltro);
                    }
                    else
                    {
                        query = @"SELECT m.remitente, m.destinatario, m.grupo, m.mensaje, m.fecha 
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

                        cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@usuario", UsuarioActual);
                        cmd.Parameters.AddWithValue("@usuarioId", UsuarioActualId);
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
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

        /*private async Task conexionClienteServidor()
        {
            string serverIP = "127.0.0.1"; //se modificará al estar en la universidad, por el momneto se probará la conexión local para asegurar
                                           // de que lo implementado funciona. 
                                           // El IP se modificara con el de la uni y, cada integrante modificará esta línea manualmente
                                           // con el fin de no generar inestabilidad en el programa.
            int serverPort = 13000; 
            //Debe contar con el mismo puerto de conexión que el servidor. 

            try
            {
                this.tcpCliente = new TcpClient();
                await this.tcpCliente.ConnectAsync(serverIP, serverPort); //no puede pasar a la siguiente linea hasta que se conecte asincronamente.

                MessageBox.Show("Conexión exitosa!"); //oculpar - borrar linea una vez comprobemos que todo funciona adecuadamente

                //crar un metodo que, reciba el .json o al cliente y actualizar los mensajes. 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR {ex} ");

            }
        }*/
    }
}
