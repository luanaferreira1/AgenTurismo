namespace AgenTurismo.Services
{
    public class LogService
    {
        public static void LogToConsole(string mensagem)
        {
            Console.WriteLine($"[Console] {DateTime.Now}: {mensagem}");
        }

        public static void LogToFile(string mensagem)
        {
            System.IO.File.AppendAllText("logs.txt", $"[File] {DateTime.Now}: {mensagem}\n");
        }

        public static void LogToMemory(string mensagem)
        {
            LogsMemoria.Add($"[Memory] {DateTime.Now}: {mensagem}");
        }

        public static List<string> LogsMemoria = new List<string>();

        public static void TestarLogMulticast(string mensagem)
        {
            Action<string> logger = LogToConsole;
            logger += LogToFile;
            logger += LogToMemory;

            logger(mensagem);
        }
    }
}
