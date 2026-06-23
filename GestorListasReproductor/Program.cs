using GestorListasReproductor.Models;

namespace GestorListasReproductor;

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

        var listas = ObtenerListas(listaReproduccionNuevaString.TrimStart('\r', '\n'));
        ExportToPdf(listas);
        Console.WriteLine("Listado de videos procesado exitosamente.");
    }

    #region Metodos Privados

    private static Dictionary<int, List<Video>> ObtenerListas(string listaReproduccionString)
    {
        List<Video> listaReproduccion = BuildAndlogListReproduccion(listaReproduccionString);

        // Crear un diccionario
        Dictionary<int, List<Video>> listas = new Dictionary<int, List<Video>>();

        // Identificar repetidos
        List<Video> listaRepetidos = BuscarRepetidos(listaReproduccion);

        // Filtrar videos únicos
        List<Video> listaUnicos = listaReproduccion.Except(listaRepetidos).ToList();

        // Añadir las listas al diccionario
        listas.Add(1, listaUnicos);
        listas.Add(2, listaRepetidos);

        return listas;
    }

    private static List<Video> BuscarRepetidos(List<Video> listaReproduccion)
    {
        List<Video> repetidos = new List<Video>();
        listaReproduccion.ForEach(v =>
        {
            int repetido = listaReproduccion.Count(x => x.Titulo == v.Titulo && x.Duracion == v.Duracion);
            if (repetido > 1 && !repetidos.Any(r => r.Titulo == v.Titulo && r.Duracion == v.Duracion))
            {
                repetidos.Add(v);
            }
        });
        return repetidos;
    }

    private static string GetProjectDirectory()
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        for (int i = 0; i < 4; i++) // Ajusta el número según la ubicación del proyecto en tu estructura de carpetas
        {
            currentDirectory = Path.GetDirectoryName(currentDirectory);
        }
        return currentDirectory;
    }

    private static void ExportToPdf(Dictionary<int, List<Video>> listaReproduccionNueva)
    {
        string projectDirectory = GetProjectDirectory();
        string inputFolderPath = Path.Combine(projectDirectory, "output");
        string outputPath = Path.Combine(inputFolderPath, "output.txt");
        using (var writer = new StreamWriter(outputPath))
        {
            string duracionTotalListaReproduccion = GetTotalTime(listaReproduccionNueva[1]);

            writer.WriteLine("Lista de Videos");
            writer.WriteLine("________________________________________________________________________________");
            writer.WriteLine($"Duracion total: {duracionTotalListaReproduccion}");
            writer.WriteLine($"Te quedan por ver {listaReproduccionNueva.Count} videos");

            writer.WriteLine("________________________________________________________________________________");
            int index = 1;
            foreach (var video in listaReproduccionNueva[1])
            {
                writer.WriteLine($"{index} {GetTime(video.Duracion)} {video.Titulo}");
                index++;
            }
            writer.WriteLine("________________________________________________________________________________");
            writer.WriteLine("Repetidos");
            writer.WriteLine("________________________________________________________________________________");
            foreach (var video in listaReproduccionNueva[2])
            {
                writer.WriteLine($"{video.Titulo}");
                index++;
            }


        }
        Console.WriteLine($"Archivo de texto creado exitosamente en: {outputPath}");
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

        return ListaReproduccion;
    }

    private static string GetTime(int totalSeconds)
    {
        int hor, min, seg;
        hor = totalSeconds / 3600;
        min = ((totalSeconds - hor * 3600) / 60);
        seg = totalSeconds - (hor * 3600 + min * 60);
        return $"{hor}:{min}:{seg}";
    }

    private static List<Video> CreateListaReproduccion(string listaReproduccion)
    {
        int totalSeconds;
        return BuildListVideo(listaReproduccion, out totalSeconds);
    }

    private static List<Video> BuildListVideo(string listaReproduccion, out int totalSeconds)
    {
        var videosArr = listaReproduccion.Split("\r\n\r\n\r\n");
        var lista = new List<Video>();

        totalSeconds = 0;
        foreach (var v in videosArr)
        {
            try
            {
                var obj = v.Split("\r\n");
                var duration = obj[0].Trim();
                var durationArr = duration.Split(":");
                int sec = durationArr.Count() == 3
                    ? Int16.Parse(durationArr[0]) * 3600 + Int16.Parse(durationArr[1]) * 60 + Int16.Parse(durationArr[2])
                    : Int16.Parse(durationArr[0]) * 60 + Int16.Parse(durationArr[1]);
                string publicacion = obj.Last().Split(" • ").Last();
                var video = new Video { Duracion = sec, Titulo = obj[2], Autor = obj[3], CuandoSePublico = obj[5] };
                lista.Add(video);
            }
            catch (Exception e)
            {
                Console.WriteLine("error en" + e);
                throw;
            }
        }
        return lista;
    }

    #endregion
}
