using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ProyectoU6
{
    class Comentario
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentariotxt { get; set; }
        public int Ip { get; set; }
        public string es_inapropiado { get; set; }
        public int Likes { get; set; }

        public override string ToString()
        {
            if (es_inapropiado == "si")
            {
                return String.Format("Comentario Inapropiado");
            }
            else
            {
                return String.Format($"Autor:{Autor} -- {Comentariotxt} -- Likes: {Likes} -- Id: {Id}");

            }
            
        }

        
    }

    class ComentarioDB
    {
        public static void SaveToFile(List<Comentario>comentarios, string path)
        {
            StreamWriter textOut = null;
            try
            {
                textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach (var comentario in comentarios)
                {
                    textOut.Write(comentario.Id + "|");
                    textOut.Write(comentario.Autor + "|");
                    textOut.Write(comentario.Fecha + "|");
                    textOut.Write(comentario.Comentariotxt + "|");
                    textOut.Write(comentario.Ip + "|");
                    textOut.Write(comentario.es_inapropiado + "|");
                    textOut.WriteLine(comentario.Likes);

                }
            }
            catch (IOException)
            {
                Console.WriteLine("Ya existe el archivo");
            }
            catch (Exception)
            {
                Console.WriteLine("Error");

            }
            finally
            {
                if (textOut != null)
                    textOut.Close();
            }
        }

        public static List<Comentario> ReadFromFile(string path)
        {
                           
                List<Comentario> comentarios = new List<Comentario>();
                StreamReader textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));

                while (textIn.Peek() != -1)
                {
                    string row = textIn.ReadLine();
                    string[] columns = row.Split('|');
                    Comentario c = new Comentario();
                    c.Id = int.Parse(columns[0]);
                    c.Autor = columns[1];
                    c.Fecha = DateTime.Parse(columns[2]);
                    c.Comentariotxt = columns[3];
                    c.Ip = int.Parse(columns[4]);
                    c.es_inapropiado = columns[5];
                    c.Likes = int.Parse(columns[6]);

                    comentarios.Add(c);

                }
                textIn.Close();

                return comentarios;
            
        }

        public static void GetLike(string path)
        {
            List<Comentario> comentarios;

            if (Path.GetExtension(path) == ".txt")
                comentarios = ReadFromFile(path);
            else
                comentarios = null;

            var filtro_comentarios = from c in comentarios
                                     orderby c.Likes descending, c.Fecha descending
                                     select c;
            

            foreach (var c in filtro_comentarios)
                Console.WriteLine(c);
        }

        /*
        static void borrar(string Id,string path)  
        {
            List<Comentario> comentarios;

            if (Path.GetExtension(path) == ".txt")
                comentarios = ReadFromFile(path);
            else
                comentarios = null;

            var filtro_comentarios = from c in comentarios
                                     where c.Id == Id
                                     select c;
            foreach (var c in filtro_comentarios)
            {
                comentarios.Remove(c);
                Console.WriteLine(comentarios);
            }
            
        }
        */

        


    }

    class FueraDeRango : Exception
    {
        public FueraDeRango(string mesage): base(mesage) { }
    }

    class Program
    {

        static bool ValidadId(int Id)
        {
            if (Id < 1000 || Id > 9999)
                throw new FueraDeRango("La ID debe estar entre 1000 y 9999");


            else
                return true;

        }

        static int CapturaId()
        {
            int id = 0;
            int band = 0;
            do
            {

                try
                {
                    id = int.Parse(Console.ReadLine());
                    band = 0;
                }
                catch (FormatException)
                {
                    Console.WriteLine("No se puede escribir una ID con letras");
                    band = 1;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("ID demasiado grande");
                    band = 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    band = 1;
                }

                if (ValidadId(id))
                    band = 0;
                /*
                if (band == 1)
                {
                    Console.WriteLine("Escribe el ID  de nuevo por favor");
                }
                */
                return id;


            }
            while (band == 1);

            //if(ValidadId(id))
            //return id;

            //return id;
        }
        
        
       
        static void Main(string[] args)
        {
            //PARA GUARDAR <-----
            /*
            List<Comentario> comentarios = new List<Comentario>();

            comentarios.Add(new Comentario() { Id = 1567, Autor = "Alan Jimenez", Comentariotxt = "Feliz Fin de semestre", es_inapropiado = "no", Fecha = DateTime.Today, Ip = 192187, Likes = 10 });
            comentarios.Add(new Comentario() { Id = 1867, Autor = "Brandon Muro", Comentariotxt = "Hasta la proxima", es_inapropiado = "no", Fecha = DateTime.Now, Ip = 192198, Likes = 2 });
            comentarios.Add(new Comentario() { Id = 1111, Autor = "Edgar", Comentariotxt = "Viva AMLO", es_inapropiado = "si", Fecha = DateTime.Now, Ip = 192524, Likes = 1 });
            comentarios.Add(new Comentario() { Id = 5687, Autor = "Juan", Comentariotxt = "Feliz jueves", es_inapropiado = "no", Fecha = DateTime.Now, Ip = 192687, Likes = 2 });
            comentarios.Add(new Comentario() { Id = 7789, Autor = "Memo", Comentariotxt = "Martes de plaza", es_inapropiado = "no", Fecha = DateTime.Now, Ip = 192111, Likes = 4 });
            comentarios.Add(new Comentario() { Id = 4687, Autor = "Pepe", Comentariotxt = "Viva profirio", es_inapropiado = "no", Fecha = DateTime.Now, Ip = 192487, Likes = 3 });

            ComentarioDB.SaveToFile(comentarios, @"C:\Users\Alan\OOP\comentarios.txt");
            */

            //PARA LEER E IMPRIMIR <-----
            List<Comentario> comentarios = ComentarioDB.ReadFromFile(@"C:\Users\Alan\OOP\comentarios.txt");

            foreach (var c in comentarios)
                Console.WriteLine(c);

            //Para agregar
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Deseas agregar un comentario? si o no");
            string opc1 = Console.ReadLine();
            while (opc1 == "si")
            {
                List<Comentario> coments = ComentarioDB.ReadFromFile(@"C:\Users\Alan\OOP\comentarios.txt");
                Console.Write("Escribe tu nombre: ");
                string nombreus = Console.ReadLine();
                Console.WriteLine("Escribe el comentario");
                string coment = Console.ReadLine();
                Console.WriteLine("Escribe la Id");
                
                int id = 0;//<----
                int band = 0;
                do
                {
                    try
                    {
                        id = CapturaId();
                        band = 0;
                    }
                    catch (FueraDeRango e)
                    {
                        Console.WriteLine(e.Message);
                        band = 1;
                    }

                    for (int i = 0; i < coments.Count; i++)
                    {
                        if (coments[i].Id == id)
                        {
                            Console.WriteLine("Este ID ya existe");
                            band = 1;
                        }

                    }

                    if (band == 1)
                    {
                        Console.WriteLine("Escribe el ID  de nuevo por favor");
                    }
                }
                while (band == 1);
                
               coments.Add(new Comentario() { Id = id, Autor = nombreus, Comentariotxt = coment, es_inapropiado = "no", Fecha = DateTime.Now, Ip = 192155, Likes = 0 });

                ComentarioDB.SaveToFile(coments, @"C:\Users\Alan\OOP\comentarios.txt");

                //foreach (var c in comentarios)
                //  Console.WriteLine(c);

                Console.WriteLine("Deseas escribir otro comentario?");
                opc1 = Console.ReadLine();
            }

            //PARA BORRAR
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Deseas borrar un comentario? si o no");
            string Opc = Console.ReadLine();
            while (Opc == "si")
            {
                List<Comentario> coments = ComentarioDB.ReadFromFile(@"C:\Users\Alan\OOP\comentarios.txt");
                Console.WriteLine("Escribe el ID del comentario que deseas borrar");
                int id = 0;
                int band = 0;
               
                do
                {
                    try
                    {
                        id = CapturaId();
                        band = 0;
                    }
                    catch (FueraDeRango e)
                    {
                        Console.WriteLine(e.Message);
                        band = 1;
                    }
                    
                    if (band == 1)
                    {
                        Console.WriteLine("Escribe el ID  de nuevo por favor");
                    }
                    
                    

                }
                while (band == 1 );

                //Para el index
                int index = 0;
                
                try
                {
                    for (int i = 0; i < coments.Count; i++)
                    {
                        if (coments[i].Id == id)
                        {
                            index = i;
                        
                        }
                        
                    }

                    coments.RemoveAt(index);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("ID no encontrado");
                }

                ComentarioDB.SaveToFile(coments, @"C:\Users\Alan\OOP\comentarios.txt");

                Console.WriteLine("Deseas borrar otro comentario?");
                Opc = Console.ReadLine();

            }
                        
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Por likes y por fecha");
            ComentarioDB.GetLike(@"C:\Users\Alan\OOP\comentarios.txt");
            Console.WriteLine("---------------------------------------------");
            

        }
    }
}
