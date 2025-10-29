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
        public static string DatabaseServer = "192.168.1.150";
        public static string DbUser = "chat_user";
        public static string DbPassword = "chat_password_123";
        public static string DbName = "chat";

        public static string ConnectionString =>
            $"server={DatabaseServer};uid={DbUser};pwd={DbPassword};database={DbName};";
    }
}
