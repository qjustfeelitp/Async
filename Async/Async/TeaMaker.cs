namespace Async;

/// <summary>
/// Analogy from https://github.com/T0shik/raw-coding-101-tutorials/blob/main/Async%20Await%20Task%20Explained/making_tea_analogy.linq
/// </summary>
public static class TeaMaker
{
    public static async Task<string> MakeTeaAsync()
    {
        Console.WriteLine(Environment.CurrentManagedThreadId);
        var boilingWater = BoilWaterAsync();

        Console.WriteLine("take the cups out");

        //var a = 0;
        //for (int i = 0; i < 100_000_000; i++)
        //{
        //    a += i;
        //}

        Console.WriteLine("put tea in cups");

        Console.WriteLine(Environment.CurrentManagedThreadId);
        string water = await boilingWater;
        string tea = $"pour {water} in cups";
        Console.WriteLine(Environment.CurrentManagedThreadId);

        Console.WriteLine(tea);

        return tea;
    }

    private static async Task<string> BoilWaterAsync()
    {
        Console.WriteLine("Start the kettle");
        Console.WriteLine("waiting for the kettle");
        Console.WriteLine(Environment.CurrentManagedThreadId);
        await Task.Delay(300);

        Console.WriteLine(Environment.CurrentManagedThreadId);
        Console.WriteLine("kettle finished boiling");

        return "water";
    }

    public static string MakeTea()
    {
        string water = BoilWater();

        Console.WriteLine("take the cups out");

        Console.WriteLine("put tea in cups");

        string tea = $"pour {water} in cups";

        Console.WriteLine(tea);

        return tea;
    }

    private static string BoilWater()
    {
        Console.WriteLine("Start the kettle");

        Console.WriteLine("waiting for the kettle");

        Thread.Sleep(300);

        Console.WriteLine("kettle finished boiling");

        return "water";
    }
}
