namespace API.Utilities
{
    public class Logger
    {

        public void getMessage(string message) =>
            Console.WriteLine(message);

        public void getMessage(string message1, string message2) =>
            Console.WriteLine($"{message1}, {message2}");

        public void getMessage<T>(string message, T entity) =>
            Console.WriteLine(message, entity);

        public void getMessage(Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Exception: {exception.Message}\nStackTrace: {exception.StackTrace}");
            Console.ResetColor();
        }

    }
}
