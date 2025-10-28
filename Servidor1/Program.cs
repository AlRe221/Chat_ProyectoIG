using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
namespace Servidor1
{
    internal class Program
    {
        //ver como inicializar el servidor, investigar como crear ese servidor jajajaj 
        const int port = 13000;

        static void Main(string[] args)
        {

            IPAddress ipAddress = IPAddress.Any; //el servidor detectara el IP establecido en IPserver en la clase Form1. 
            TcpListener tcpList = null;

            try
            {
                tcpList = new TcpListener(ipAddress, port); 
                tcpList.Start();

                Console.WriteLine($"servidor inicializado y escuchando {port}."); //lo mismo en esta línea 

                while (true) {  //estre while funciona para que, mientras la acción de que el servidor este funcionando sea verdadera, pueda conectarse con el cliente sin ningun problema 
                                //y aceptarlo dentro del servidor. 

                    TcpClient cliente = tcpList.AcceptTcpClient();
                    Console.WriteLine("cliente conectado!");  //ocultar - borrar línea una vez comprobemos que todo funciona correctamente. 

                    //en este punto, se manda a llamar una función que maneje la base de datos y lo transforme a un .json. 
                    //puede ser establecida en este mismo espacio. 
                }

            }
            catch (SocketException e)
            {
                Console.WriteLine($"ERROR: {e}");

            }
            finally
            {
                tcpList.Stop();

            }
        }
        //crear el metdo que implemente lo comentado arriba. 
        
    }
}
