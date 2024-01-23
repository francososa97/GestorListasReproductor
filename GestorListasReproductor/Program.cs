using GestorListasReproductor.Models;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace GestorListasReproductor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = GetProjectDirectory();

            // Construye la ruta del archivo de entrada (lista de reproducción)
            string inputFolderPath = Path.Combine(projectDirectory, "output");
            string inputFilePath = Path.Combine(inputFolderPath, "input.txt");

            // Lee la lista de reproducción desde el archivo
            string listaReproduccionNuevaString = File.ReadAllText(inputFilePath);



            var listaReproduccionNueva = BuildAndlogListReproduccion(listaReproduccionNuevaString.TrimStart('\r', '\n'));
            ExportToPdf(listaReproduccionNueva);
        }

        #region Metodos Privados
        private static string GetProjectDirectory()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 4; i++) // Ajusta el número según la ubicación del proyecto en tu estructura de carpetas
            {
                currentDirectory = Path.GetDirectoryName(currentDirectory);
            }
            return currentDirectory;
        }
        private static void ExportToPdf(List<Video> listaReproduccionNueva)
        {
            string projectDirectory = GetProjectDirectory();
            string inputFolderPath = Path.Combine(projectDirectory, "output");
            string outputPath = Path.Combine(inputFolderPath, "output.txt");
            using (var writer = new StreamWriter(outputPath))
            {
                string duracionTotalListaReproduccion = GetTotalTime(listaReproduccionNueva);

                writer.WriteLine("Lista de Videos");
                writer.WriteLine("\n________________________________________________________________________________\n");
                writer.WriteLine($"Duracion total: {duracionTotalListaReproduccion}\n");
                writer.WriteLine($"Te quedan por ver {listaReproduccionNueva.Count} videos\n");

                BuildListaPorTiempo(listaReproduccionNueva, writer,0, 1800);
                BuildListaPorTiempo(listaReproduccionNueva, writer, 1800, 3600);
                BuildListaPorTiempo(listaReproduccionNueva, writer, 3600);

                writer.WriteLine("\n________________________________________________________________________________\n");
                int index = 1;
                foreach (var video in listaReproduccionNueva)
                {
                    // Escribe información de video en el archivo de texto
                    writer.WriteLine($"{index} {GetTime(video.Duracion)} {video.Titulo}");
                    index++;
                }
                writer.WriteLine("\n________________________________________________________________________________\n");

            }
            Console.WriteLine($"Archivo de texto creado exitosamente en: {outputPath}");
        }

        private static void BuildListaPorTiempo(List<Video> listaReproduccionNueva, StreamWriter writer, int secondsStart, int secondEnd = 0)
        {
            int index = 1;
            writer.WriteLine("\n________________________________________________________________________________\n");
            var videosMenores = new List<Video>();
            if (secondEnd > 0)
                videosMenores = listaReproduccionNueva.Where(x => x.Duracion >= secondsStart && x.Duracion < secondEnd).ToList();
            else
                videosMenores = listaReproduccionNueva.Where(x => x.Duracion >= secondsStart).ToList();

            writer.WriteLine($"Tenemos {videosMenores.Count()} " +
            $"{(secondEnd > 0 ? $"videos mayores a {secondsStart / 60} minutos y menores a {secondEnd / 60} minutos" : "Videos mayores a una hora")}");
            videosMenores.ForEach(x => writer.WriteLine($"{index} {GetTime(x.Duracion)} {x.Titulo}"));
        }

        private static string GetDifTiempo(List<Video> listaReproduccionNueva, List<Video> listaReproduccionOriginal)
        {
            string tiempoListNueva = GetTotalTime(listaReproduccionNueva);
            string tiempoListOriginal = GetTotalTime(listaReproduccionOriginal);

            string[] partesTiempo1 = tiempoListOriginal.Split(':');
            string[] partesTiempo2 = tiempoListNueva.Split(':');

            // Convierte las partes a enteros
            int horas1 = int.Parse(partesTiempo1[0]);
            int minutos1 = int.Parse(partesTiempo1[1]);
            int segundos1 = int.Parse(partesTiempo1[2]);

            int horas2 = int.Parse(partesTiempo2[0]);
            int minutos2 = int.Parse(partesTiempo2[1]);
            int segundos2 = int.Parse(partesTiempo2[2]);

            // Realiza la resta
            int diferenciaHoras = horas1 - horas2;
            int diferenciaMinutos = minutos1 - minutos2;
            int diferenciaSegundos = segundos1 - segundos2;

            // Asegura que los minutos y segundos estén en el rango adecuado
            if (diferenciaMinutos < 0)
            {
                diferenciaMinutos += 60;
                diferenciaHoras--;
            }

            if (diferenciaSegundos < 0)
            {
                diferenciaSegundos += 60;
                diferenciaMinutos--;
            }


            return $"{diferenciaHoras}:{diferenciaMinutos}:{diferenciaSegundos}";
        }

        private static string GetTotalTime(List<Video> listaReproduccion)
        {
            var totalTime = 0;
            listaReproduccion.ForEach(video => totalTime += video.Duracion);
            return GetTime(totalTime);
        }

        private static List<Video> BuildAndlogListReproduccion(string lista)
        {
            Console.WriteLine("\nEjecutando tareas para obtener el listado de reproduccion!\n");
            var ListaReproduccion = CreateListaReproduccion(lista);


            List<Video> ListReproduccionOrder = OrderListReproc(ListaReproduccion);
            var videosOrdenados = GetObtenerVideosMasCortos(ListReproduccionOrder);
            return videosOrdenados;
        }

        private static void BuscarVideosEnRango(List<Video> videos, int secondEnd,int secondInit = 0)
        {
            if (secondInit > 0)
            {;
                var cantVideosEnRango = videos.Where(x => x.Duracion <= secondEnd).Count();
                //Console.WriteLine($"tenemos videos {cantVideosEnRango} mas cortos que {secondEnd / 60} minutos");
            }
            else
            {
                var cantVideosEnRango = videos.Where(x => x.Duracion <= secondEnd && x.Duracion > secondInit).Count();
                //Console.WriteLine($"tenemos videos {cantVideosEnRango} mas cortos que {secondEnd / 60} minutos");
            }
        }

        private static List<Video> OrderListReproc(List<Video> ListaReproduccion, bool modResume = false)
        {
            List<Video> autores = new List<Video>();
            var ListReproduccionOrder = ListaReproduccion.OrderBy(x => x.Duracion).ToList();
            ListReproduccionOrder.ForEach(x =>
            {
                if (!autores.Where(a => a.Autor.Contains(x.Autor)).Any())
                {
                    x.Cantidad = ListReproduccionOrder.Where(a => a.Autor == x.Autor).Count();
                    autores.Add(x);
                }

            });

            if (!modResume)
            {
                var listaOrdenadaCantidad = autores.OrderByDescending(x => x.Cantidad).ToList();
                //listaOrdenadaCantidad.ForEach(autor => Console.WriteLine($"El autor {autor.Autor} tiene {ListReproduccionOrder.Where(x => x.Autor == autor.Autor).Count()}"));
                //Console.WriteLine("----------------------------------------------------------------------");
            }

            return ListReproduccionOrder;
        }

        private static void GetDiferenciaListaReprouduccion(List<Video> listaReproduccion, List<Video> listaReproduccionVieja)
        {
            int secondsListNew = 0;
            int secondsListOld = 0;
            listaReproduccion.ForEach(item => secondsListNew += item.Duracion);
            listaReproduccionVieja.ForEach(item => secondsListOld += item.Duracion);
            //Console.WriteLine($"Lista vieja {listaReproduccionVieja.Count()} Tiempo total antiguo {GetTime(secondsListOld)}");
            //Console.WriteLine($"Lista nueva {listaReproduccion.Count()} Tiempo total nuevo {GetTime(secondsListNew)}");
            //Console.WriteLine($"Felicidades viste {listaReproduccionVieja.Count() - listaReproduccion.Count()} videos y descontaste {GetTime(secondsListOld - secondsListNew)}\n\n");
        }
        private static List<Video> GetObtenerVideosMasCortos(List<Video> listReproduccionOrder)
        {
            var videosOrdenadosDuracion = listReproduccionOrder.OrderBy(x => x.Duracion).ToList();
            int idVideo = 0;
            videosOrdenadosDuracion.ForEach(x => {
                //Console.WriteLine($"{idVideo} {GetTime(x.Duracion)}.{x.Titulo}");
                idVideo++;
            });
            return videosOrdenadosDuracion;
        }


        private static string GetTime(int totalSeconds)
        {
            int hor, min, seg;
            hor = totalSeconds / 3600;
            min = ((totalSeconds - hor * 3600) / 60);
            seg = totalSeconds - (hor * 3600 + min * 60);
            return $"{hor}:{min}:{seg}";
        }

        /// <summary>
        /// Obtiene un listado de videos, de la lista de reporduccion enviada.
        /// </summary>
        /// <returns></returns>
        private static List<Video> CreateListaReproduccion(string listaReproduccion, bool modResume = false)
        {
            int totalSeconds;
            List<Video> videosCantidad = BuildListVideo(listaReproduccion, out totalSeconds);
            videosCantidad.ForEach(item => totalSeconds += item.Duracion);
            return videosCantidad.OrderByDescending(x => x.Cantidad).ToList();
        }

        private static List<Video> BuildListVideo(string listaReproduccion, out int totalSeconds)
        {
            var videosArr = listaReproduccion.Split("\r\n\r\n\r\n");
            var lista = new List<Video>();
            List<Video> videosCantidad = new List<Video>();

            totalSeconds = 0;
            foreach (var v in videosArr)
            {
                var obj = v.Split("\r\n");
                var duration = obj[0].Trim();
                var durationArr = duration.Split(":");
                int sec = durationArr.Count() == 3
                    ? Int16.Parse(durationArr[0]) * 3600 + Int16.Parse(durationArr[1]) * 60 + Int16.Parse(durationArr[2])
                    : Int16.Parse(durationArr[0]) * 60 + Int16.Parse(durationArr[1]);
                string publicacion = obj.Last().Split(" • ").Last();
                var video = new Video { Duracion = sec, Titulo = obj[2], Autor = obj[3], CuandoSePublico = publicacion };
                lista.Add(video);
            }
            foreach (var item in lista)
            {
                item.Cantidad = lista.Where(x => x.Autor == item.Autor).Count();
                videosCantidad.Add(item);
            }
            return videosCantidad;
        }

        #endregion
    }
}