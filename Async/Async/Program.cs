//https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md
//https://www.youtube.com/watch?v=FIZVKteEFyk&ab_channel=RainerStropek
//https://www.youtube.com/watch?v=S49dpEwMSUY&ab_channel=RainerStropek
//https://www.youtube.com/watch?v=By2HlOKIZxs&ab_channel=RainerStropek
//https://www.youtube.com/watch?v=3GhKdDCvtKE&ab_channel=RawCoding
//https://www.youtube.com/watch?v=il9gl8MH17s&ab_channel=RawCoding
//https://www.youtube.com/watch?v=vYXs--S0Xxo&ab_channel=SingletonSean
//https://www.youtube.com/watch?v=RqJESGHEMDY
//https://www.youtube.com/watch?v=wErS2ODmkNQ
//https://www.youtube.com/watch?v=TgUYcZV-foM

// TOKEN binding
// https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/ModelBinding/Binders/CancellationTokenModelBinder.cs
// https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/ModelBinding/Binders/CancellationTokenModelBinderProvider.cs

// WHAT IS TASK?
// task is something like a bridge between async keyword (user code) and compiled state machine and await generates some sort of checkpoints in the state machine

// WHAT IS VALUETASK?
// discriminated union where it can return one of two values Task<T> or T

using Async;

// ----------------------------------------------------------------------------------
//Examples.Example01();
//await Examples.Example02();

//try
//{
//    Examples.Example03();
//}
//catch
//{
//    // will be caught?
//}

//Console.WriteLine(await Examples.Example04());
//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

//await 1;
//await TimeSpan.FromMilliseconds(1);
//await Process.Start("notepad.exe");

//var tasks = new List<Task<int>>();

//for (int i = 0; i < 10; i++)
//{
//    tasks.Add(Task.FromResult(RandomNumberGenerator.GetInt32(100)));
//}

//await tasks;

//foreach (var task in tasks)
//{
//    Console.WriteLine(await task);
//}

// ----------------------------------------------------------------------------------

//Console.WriteLine(new string('-', 50));

//TeaMaker.MakeTea();
//Console.WriteLine(new string('-', 50));

//await TeaMaker.MakeTeaAsync();
//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

//var awaitableClass = new AwaitableClass();
//await awaitableClass;
//Console.WriteLine(awaitableClass.IsSomething);
//Console.WriteLine(new string('-', 50));

//var asyncCtor = new AsyncCtor();
//Console.WriteLine(asyncCtor.SomeData);
//Console.WriteLine(new string('-', 50));

//var asyncCtorIfNeeded = await AsyncCtorIfNeededExecutor.Create();
//Console.WriteLine(asyncCtorIfNeeded.SomeData);
//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

//Environment.SetEnvironmentVariable("DOTNET_SYSTEM_THREADING_POOLASYNCVALUETASKS", "1");

//Console.WriteLine(await ValueTaskMagic.GetValue());
//Console.WriteLine(new string('-', 50));

//var valueTaskMagic = ValueTaskMagic.GetValue();

//Console.WriteLine(await valueTaskMagic);
//Console.WriteLine(await valueTaskMagic);
//Console.WriteLine(new string('-', 50));

//var valueTasks = new List<ValueTask<int>>();

//for (int i = 0; i < 10; i++)
//{
//    valueTasks.Add(valueTaskMagic);
//}

//await Task.WhenAll(valueTasks);

//foreach (var valueTask in valueTasks)
//{
//    Console.WriteLine(await valueTask);
//}

//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

//Continuation.Run();
//await Continuation.RunBetter();

//using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

//Console.WriteLine(await Cancellation.CancellableTask(cts.Token));

using var cts2 = new CancellationTokenSource();

var cancellableTask = Cancellation.CancellableTask(cts2.Token);

await Task.Delay(100);
cts2.Cancel();

int result = default;

try
{
    result = await cancellableTask;
}
catch (OperationCanceledException)
{
    //ignored
}

Console.WriteLine(result);

//using var cts3 = new CancellationTokenSource(TimeSpan.FromSeconds(2));

//Console.WriteLine(Cancellation.CancellableNotTask(cts3.Token));

