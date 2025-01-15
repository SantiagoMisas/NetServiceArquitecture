namespace API.Utilities
{
    public class Logger
    {

        public void printMessage(string message) =>
            Console.WriteLine(message);

        public void printMessage(string message1, string message2) =>
            Console.WriteLine($"{message1}, {message2}");

        public void printMessage<T>(string message, T entity) =>
            Console.WriteLine(message, entity);

        public void printMessage(Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Exception: {exception.Message}\nStackTrace: {exception.StackTrace}");
            Console.ResetColor();
        }

    }
}
