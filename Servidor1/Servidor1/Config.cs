using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor1
{
    public static class Config
    {
        // Configuración del servidor de chat
        public static int ChatPort = 13000;

        // Configuración de la base de datos
        public static string DatabaseServer = "192.168.0.157";
        public static string DbUser = "cliente";
        public static string DbPassword = "57057goku75@jua57";
        public static string DbName = "chat_usuario";

        public static string ConnectionString =>
            $"server={DatabaseServer};uid={DbUser};pwd={DbPassword};database={DbName};";
    }
}
