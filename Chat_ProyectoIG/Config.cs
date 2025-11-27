using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_ProyectoIG
{
    public static class Config
    {
        // ⚠️ CAMBIAR POR LA IP DE LA LAPTOP SERVIDOR
        public static string DatabaseServer = "192.168.0.157";
        public static string ChatServer = "192.168.0.157";
        public static int ChatPort = 13000;

        public static string DbUser = "cliente";
        public static string DbPassword = "57057goku75@jua57";
        public static string DbName = "chat_usuario";

        public static string ConnectionString =>
            $"server={DatabaseServer};uid={DbUser};pwd={DbPassword};database={DbName};";
    }
}
