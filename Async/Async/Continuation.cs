namespace Async;

public static class Continuation
{
    private static readonly string filePath = "TextFile.txt";

    public static void Run()
    {
        string[] lines = null!;

        // what happens when there is an exception for example when file is not found
        File.ReadAllLinesAsync(filePath)
            .ContinueWith(task =>
             {
                 Thread.Sleep(100);
                 lines = task.Result;

                 foreach (string line in lines)
                 {
                     Console.WriteLine(line);
                 }
             })
            .ContinueWith(_ =>
             {
                 File.AppendAllLinesAsync(filePath, lines)
                     .ContinueWith(_ =>
                      {
                          Thread.Sleep(100);
                          Console.WriteLine("Done?");
                      });
             });
    }

    public static async Task RunBetter()
    {
        string[] lines = await File.ReadAllLinesAsync("TextFile.txt");

        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }

        await File.AppendAllLinesAsync("TextFile.txt", lines);
    }
}
