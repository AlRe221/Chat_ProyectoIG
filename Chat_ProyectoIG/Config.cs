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
        public static string DatabaseServer = "192.168.1.150";
        public static string ChatServer = "192.168.1.150";
        public static int ChatPort = 13000;

        public static string DbUser = "chat_user";
        public static string DbPassword = "chat_password_123";
        public static string DbName = "chat";

        public static string ConnectionString =>
            $"server={DatabaseServer};uid={DbUser};pwd={DbPassword};database={DbName};";
    }
}
