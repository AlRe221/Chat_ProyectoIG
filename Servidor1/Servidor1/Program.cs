using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Servidor1
{
    public class MensajeChat
    {
        public string Tipo { get; set; } // "publico", "privado", "grupo", "login"
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string Grupo { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class MensajeSistema
    {
        public string Tipo { get; set; } // "actualizar_usuarios", "actualizar_grupos"
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
    }

    internal class Program
    {
        static TcpListener tcpList = null;
        static List<TcpClient> clientesConectados = new List<TcpClient>();
        static Dictionary<string, TcpClient> usuariosConectados = new Dictionary<string, TcpClient>();
        static object lockObject = new object();

        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Any;

            try
            {
                tcpList = new TcpListener(ipAddress, Config.ChatPort);
                tcpList.Start();

                Console.WriteLine($"✅ Servidor escuchando en:");
                Console.WriteLine($"   - Local: 127.0.0.1:{Config.ChatPort}");
                Console.WriteLine($"   - Red: {GetLocalIPAddress()}:{Config.ChatPort}");
                Console.WriteLine($"   - Todos: 0.0.0.0:{Config.ChatPort}");
                Console.WriteLine("Esperando conexiones de clientes...");

                while (true)
                {
                    TcpClient cliente = tcpList.AcceptTcpClient();

                    // Mostrar información del cliente conectado
                    string clientIP = ((IPEndPoint)cliente.Client.RemoteEndPoint).Address.ToString();
                    Console.WriteLine($"🔗 Cliente conectado desde: {clientIP}");

                    Thread clientThread = new Thread(() => ManejarCliente(cliente));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"ERROR de Socket: {e}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR general: {e}");
            }
            finally
            {
                tcpList?.Stop();
                Console.WriteLine("Servidor detenido.");
            }
        }

        static void ManejarCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream();
            byte[] buffer = new byte[4096];
            int bytesRead;
            string usuarioActual = null;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string mensajeRecibido = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Mensaje recibido: {mensajeRecibido}");

                    ProcesarMensaje(mensajeRecibido, cliente, ref usuarioActual);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error con cliente: {ex.Message}");
            }
            finally
            {
                if (usuarioActual != null)
                {
                    RemoverCliente(usuarioActual, cliente);
                }
                cliente.Close();
                Console.WriteLine($"Cliente {(usuarioActual ?? "desconocido")} desconectado.");
            }
        }

        static void ProcesarMensaje(string jsonMensaje, TcpClient clienteRemitente, ref string usuarioActual)
        {
            try
            {
                var mensajeObj = JsonConvert.DeserializeObject<MensajeChat>(jsonMensaje);

                if (mensajeObj.Tipo == "login" && usuarioActual == null)
                {
                    usuarioActual = mensajeObj.Remitente;
                    AgregarCliente(usuarioActual, clienteRemitente);

                    var confirmacion = new MensajeChat
                    {
                        Tipo = "login_confirmado",
                        Remitente = "Servidor",
                        Contenido = "Login exitoso",
                        Fecha = DateTime.Now
                    };
                    EnviarMensajeACliente(clienteRemitente, confirmacion);
                    return;
                }

                // ✅ Manejar creación de grupos
                if (mensajeObj.Tipo == "crear_grupo")
                {
                    // Formato: "nombre_grupo|miembro1,miembro2,miembro3"
                    var partes = mensajeObj.Contenido.Split('|');
                    if (partes.Length >= 2)
                    {
                        string nombreGrupo = partes[0];
                        List<string> miembros = partes[1].Split(',').Where(m => !string.IsNullOrEmpty(m)).ToList();

                        GuardarGrupoEnBD(nombreGrupo, mensajeObj.Remitente, miembros);

                        // Confirmación al creador
                        var confirmacion = new MensajeChat
                        {
                            Tipo = "grupo_creado",
                            Remitente = "Servidor",
                            Contenido = $"Grupo '{nombreGrupo}' creado exitosamente. Miembros notificados.",
                            Fecha = DateTime.Now
                        };
                        EnviarMensajeACliente(clienteRemitente, confirmacion);
                    }
                    else
                    {
                        // Error de formato
                        var error = new MensajeChat
                        {
                            Tipo = "error",
                            Remitente = "Servidor",
                            Contenido = "Formato incorrecto para crear grupo",
                            Fecha = DateTime.Now
                        };
                        EnviarMensajeACliente(clienteRemitente, error);
                    }
                    return;
                }

                // Resto del código para otros tipos de mensaje...
                // Guardar mensajes de chat en BD
                if (mensajeObj.Tipo == "publico" || mensajeObj.Tipo == "privado" || mensajeObj.Tipo == "grupo")
                {
                    GuardarMensajeEnBD(mensajeObj);
                }

                switch (mensajeObj.Tipo)
                {
                    case "publico":
                        ReenviarATodos(mensajeObj, clienteRemitente);
                        break;
                    case "privado":
                        ReenviarPrivado(mensajeObj);
                        break;
                    case "grupo":
                        ReenviarAGrupo(mensajeObj);
                        break;
                    default:
                        Console.WriteLine($"Tipo de mensaje desconocido: {mensajeObj.Tipo}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error procesando mensaje: {ex.Message}");

                var errorMsg = new MensajeChat
                {
                    Tipo = "error",
                    Remitente = "Servidor",
                    Contenido = $"Error: {ex.Message}",
                    Fecha = DateTime.Now
                };
                EnviarMensajeACliente(clienteRemitente, errorMsg);
            }
        }

        static void AgregarCliente(string usuario, TcpClient cliente)
        {
            lock (lockObject)
            {
                if (!usuariosConectados.ContainsKey(usuario))
                {
                    usuariosConectados[usuario] = cliente;
                    clientesConectados.Add(cliente);
                    Console.WriteLine($"Usuario '{usuario}' agregado a la lista de conectados.");

                    var notificacion = new MensajeChat
                    {
                        Tipo = "usuario_conectado",
                        Remitente = "Servidor",
                        Contenido = $"{usuario} se ha conectado",
                        Fecha = DateTime.Now
                    };
                    ReenviarATodos(notificacion, cliente);
                }
            }
        }

        static void RemoverCliente(string usuario, TcpClient cliente)
        {
            lock (lockObject)
            {
                usuariosConectados.Remove(usuario);
                clientesConectados.Remove(cliente);
                Console.WriteLine($"Usuario '{usuario}' removido de la lista de conectados.");

                var notificacion = new MensajeChat
                {
                    Tipo = "usuario_desconectado",
                    Remitente = "Servidor",
                    Contenido = $"{usuario} se ha desconectado",
                    Fecha = DateTime.Now
                };
                ReenviarATodos(notificacion, null);
            }
        }

        static void ReenviarATodos(MensajeChat mensaje, TcpClient clienteRemitente)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mensaje));

            lock (lockObject)
            {
                foreach (var cliente in clientesConectados.ToList())
                {
                    if (cliente != clienteRemitente && cliente.Connected)
                    {
                        try
                        {
                            NetworkStream stream = cliente.GetStream();
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        catch
                        {
                            // Cliente desconectado
                        }
                    }
                }
            }

            Console.WriteLine($"Mensaje público de '{mensaje.Remitente}' reenviado a {clientesConectados.Count - 1} clientes.");
        }

        static void ReenviarPrivado(MensajeChat mensaje)
        {
            if (usuariosConectados.ContainsKey(mensaje.Destinatario))
            {
                var clienteDestino = usuariosConectados[mensaje.Destinatario];
                if (clienteDestino.Connected)
                {
                    EnviarMensajeACliente(clienteDestino, mensaje);
                    Console.WriteLine($"Mensaje privado de '{mensaje.Remitente}' a '{mensaje.Destinatario}' enviado.");
                }
                else
                {
                    Console.WriteLine($"El destinatario '{mensaje.Destinatario}' no está conectado.");
                }
            }
            else
            {
                Console.WriteLine($"El destinatario '{mensaje.Destinatario}' no existe o no está conectado.");
            }
        }

        static void ReenviarAGrupo(MensajeChat mensaje)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mensaje));

            lock (lockObject)
            {
                foreach (var cliente in clientesConectados.ToList())
                {
                    if (cliente.Connected)
                    {
                        try
                        {
                            NetworkStream stream = cliente.GetStream();
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        catch
                        {
                            // Cliente desconectado
                        }
                    }
                }
            }

            Console.WriteLine($"Mensaje de grupo '{mensaje.Grupo}' de '{mensaje.Remitente}' reenviado.");

            // ✅ NUEVO: Enviar notificación de actualización de grupos a todos los clientes
            var notificacionActualizacion = new MensajeChat
            {
                Tipo = "actualizar_grupos",
                Remitente = "Servidor",
                Contenido = "Se ha creado un nuevo grupo",
                Fecha = DateTime.Now
            };

            ReenviarATodos(notificacionActualizacion, null);
        }

        static void EnviarMensajeACliente(TcpClient cliente, MensajeChat mensaje)
        {
            if (cliente.Connected)
            {
                try
                {
                    string json = JsonConvert.SerializeObject(mensaje);
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    NetworkStream stream = cliente.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error enviando mensaje a cliente: {ex.Message}");
                }
            }
        }

        static void GuardarMensajeEnBD(MensajeChat mensaje)
        {
            try
            {
                using (var conn = new MySqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO mensajes (remitente, destinatario, grupo, mensaje) 
                     VALUES (@remitente, @destinatario, @grupo, @mensaje)";

                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@remitente", mensaje.Remitente);
                    cmd.Parameters.AddWithValue("@destinatario",
                        string.IsNullOrEmpty(mensaje.Destinatario) ? DBNull.Value : (object)mensaje.Destinatario);
                    cmd.Parameters.AddWithValue("@grupo",
                        string.IsNullOrEmpty(mensaje.Grupo) ? DBNull.Value : (object)mensaje.Grupo);
                    cmd.Parameters.AddWithValue("@mensaje", mensaje.Contenido);

                    cmd.ExecuteNonQuery();

                    if (!string.IsNullOrEmpty(mensaje.Grupo))
                    {
                        var notificacion = new MensajeChat
                        {
                            Tipo = "actualizar_grupos",
                            Remitente = "Servidor",
                            Contenido = $"Nuevo mensaje en grupo: {mensaje.Grupo}",
                            Fecha = DateTime.Now
                        };
                        Thread.Sleep(100);
                        ReenviarATodos(notificacion, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando mensaje en BD: {ex.Message}");
            }
        }

        // Método para obtener la IP local
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No se pudo determinar la IP";
        }

        static void ReenviarMensajeSistemaATodos(MensajeSistema mensaje)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mensaje));

            lock (lockObject)
            {
                foreach (var cliente in clientesConectados.ToList())
                {
                    if (cliente.Connected)
                    {
                        try
                        {
                            NetworkStream stream = cliente.GetStream();
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        catch
                        {
                            // Cliente desconectado
                        }
                    }
                }
            }

            Console.WriteLine($"Notificación de sistema enviada a {clientesConectados.Count} clientes.");
        }

        static void GuardarGrupoEnBD(string nombreGrupo, string creador, List<string> miembros)
        {
            try
            {
                using (var conn = new MySqlConnection(Config.ConnectionString))
                {
                    conn.Open();

                    // Insertar grupo
                    string queryGrupo = @"INSERT INTO grupos (nombre_grupo, creador_id) 
                                 VALUES (@nombre, (SELECT id_usuario FROM usuario WHERE usuario = @creador))";
                    var cmdGrupo = new MySqlCommand(queryGrupo, conn);
                    cmdGrupo.Parameters.AddWithValue("@nombre", nombreGrupo);
                    cmdGrupo.Parameters.AddWithValue("@creador", creador);
                    cmdGrupo.ExecuteNonQuery();

                    // Obtener ID del grupo insertado
                    long grupoId = cmdGrupo.LastInsertedId;

                    // Insertar miembros (incluyendo al creador)
                    string queryMiembro = @"INSERT INTO miembros_grupo (id_grupo, id_usuario) 
                                   VALUES (@grupoId, (SELECT id_usuario FROM usuario WHERE usuario = @usuario))";

                    // Agregar al creador (ya está incluido automáticamente)
                    var cmdCreador = new MySqlCommand(queryMiembro, conn);
                    cmdCreador.Parameters.AddWithValue("@grupoId", grupoId);
                    cmdCreador.Parameters.AddWithValue("@usuario", creador);
                    cmdCreador.ExecuteNonQuery();

                    // Agregar demás miembros
                    foreach (string miembro in miembros)
                    {
                        // Evitar duplicar al creador
                        if (miembro != creador)
                        {
                            var cmdMiembro = new MySqlCommand(queryMiembro, conn);
                            cmdMiembro.Parameters.AddWithValue("@grupoId", grupoId);
                            cmdMiembro.Parameters.AddWithValue("@usuario", miembro);
                            cmdMiembro.ExecuteNonQuery();
                        }
                    }

                    // ✅ NOTIFICAR SOLO A LOS MIEMBROS DEL GRUPO
                    NotificarNuevoGrupoAMiembros(nombreGrupo, creador, miembros);

                    Console.WriteLine($"✅ Grupo '{nombreGrupo}' creado por '{creador}' con {miembros.Count} miembros adicionales.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error guardando grupo en BD: {ex.Message}");
                throw; // Relanzar para manejar en el llamador
            }
        }

        static void NotificarNuevoGrupoAMiembros(string nombreGrupo, string creador, List<string> miembros)
        {
            var notificacion = new MensajeChat
            {
                Tipo = "actualizar_grupos",
                Remitente = "Servidor",
                Contenido = $"Has sido agregado al grupo: {nombreGrupo}",
                Fecha = DateTime.Now
            };

            // Lista de todos los miembros (incluyendo al creador)
            var todosMiembros = new List<string>(miembros) { creador };

            lock (lockObject)
            {
                foreach (var miembro in todosMiembros)
                {
                    if (usuariosConectados.ContainsKey(miembro))
                    {
                        var cliente = usuariosConectados[miembro];
                        if (cliente.Connected)
                        {
                            try
                            {
                                EnviarMensajeACliente(cliente, notificacion);
                                Console.WriteLine($"✅ Notificación enviada a: {miembro}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"❌ Error notificando a {miembro}: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"⚠️ {miembro} está en la lista pero desconectado");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ {miembro} no está conectado actualmente");
                    }
                }
            }

            Console.WriteLine($"✅ Notificación de grupo '{nombreGrupo}' enviada a {todosMiembros.Count} miembros.");
        }
    }

}