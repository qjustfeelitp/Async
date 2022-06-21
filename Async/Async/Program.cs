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
// discriminated union where it can return one of two values Task<T> or T, useful for situation where code can execute synchronously for example from cached value

// ----------------------------------------------------------------------------------



//Console.WriteLine(new string('-', 50));

//TeaMaker.MakeTea();
//Console.WriteLine(new string('-', 50));

//await TeaMaker.MakeTeaAsync();
//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

//string result = await WithAndWithoutAsyncKeyword.Without();
//Console.WriteLine(result);

//string result2 = await WithAndWithoutAsyncKeyword.With();
//Console.WriteLine(result2);

//using var client = new HttpClient();

//string githubUnauthorizedResult = await WithAndWithoutAsyncKeyword.HttpCallWithout(client);
//Console.WriteLine(githubUnauthorizedResult);

//string githubUnauthorizedResult2 = await WithAndWithoutAsyncKeyword.HttpCallWith(client);
//Console.WriteLine(githubUnauthorizedResult2);
//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

//Continuation.Run();
//await Continuation.RunBetter();

// ----------------------------------------------------------------------------------

//using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

//Console.WriteLine(await Cancellation.CancellableTask(cts.Token));

//using var cts2 = new CancellationTokenSource();

//var cancellableTask = Cancellation.CancellableTask(cts2.Token);

//int value = default;

//try
//{
//    await Task.Delay(100);
//    cts2.Cancel();
//    value = await cancellableTask;
//}
//catch (OperationCanceledException)
//{
//    //ignored
//}

//Console.WriteLine(value);

//using var cts3 = new CancellationTokenSource(TimeSpan.FromSeconds(2));

//Console.WriteLine(Cancellation.CancellableNotTask(cts3.Token));
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
//await Task.WhenAll(valueTasks.Select(x => x.AsTask()));

//foreach (var valueTask in valueTasks)
//{
//    Console.WriteLine(await valueTask);
//}

//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

//BadExamples.Example01();
//await BadExamples.Example02();

//try
//{
//    BadExamples.Example03();
//}
//catch (Exception exception)
//{
//    Console.WriteLine(exception.Message);
//}

//Console.WriteLine(await BadExamples.Example04());
//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

//int o = default;
//var thread = new Thread(() => o = 42);
//thread.Start();
//thread.Join();
//Console.WriteLine(o);

//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

//Console.WriteLine(Environment.CurrentManagedThreadId);

//var badTask = Task.Run(async () =>
//                   {
//                       Console.WriteLine(Environment.CurrentManagedThreadId);
//                       await Task.Delay(1_000).ConfigureAwait(false);
//                       Console.WriteLine(Environment.CurrentManagedThreadId);
//                   })
//                  .ConfigureAwait(false);

//badTask.GetAwaiter().GetResult();
//Console.WriteLine(Environment.CurrentManagedThreadId);

//await Task.Run(async () =>
//           {
//               Console.WriteLine(Environment.CurrentManagedThreadId);
//               await Task.Delay(1_000).ConfigureAwait(false);
//               Console.WriteLine(Environment.CurrentManagedThreadId);
//           })
//          .ConfigureAwait(false);

//Console.WriteLine(Environment.CurrentManagedThreadId);
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

//var asyncCtor = new AsyncCtor();
//Console.WriteLine(asyncCtor.SomeData);
//Console.WriteLine(new string('-', 50));

//var tcs = new TaskCompletionSource();

//var asyncCtorIfNeeded = new AsyncCtorIfNeeded(() => tcs.TrySetResult());

//await tcs.Task;
//Console.WriteLine(asyncCtorIfNeeded.SomeData);
//Console.WriteLine(new string('-', 50));

//var awaitableClass = new AwaitableClass();
//await awaitableClass;
//Console.WriteLine(awaitableClass.IsSomething);
//Console.WriteLine(new string('-', 50));

// ----------------------------------------------------------------------------------

// WHAT TO REMEMBER?
// do not use async void (except events)
// do not return Task or ValueTask, await if possible - exception source
// do not use continuations, use consequent awaits
// you can start task and await it later, if result is not needed
// if using manually created cancellation token sources, dispose them, always
// you can run multiple tasks in parallel with WhenXXX methods on Task class, do not use WaitXXX that blocks executing threads
// do not use .Result .GetAwaiter().GetResult .Wait() wherever possible, it just blocks executing thread, sync over async problem where two threads are needed to execute that operation and can cause threadpool starvation or deadlocks in platforms with synchronization context available, just replace it with sync version!
// if in platform with synchronization context and the operation result is not needed to be synchronized to that context, use configure await set to false so that there is no synchronization overhead, is asp net core, no synchronization context is present, so no need to call configure await
// when using async keyword you are practically creating new state machine with await checkpoints, if there is no need for method to execute asynchronously, use Task.CompletedTask or FromResult or other statically available methods (similar to lambdas capturing outer variables, it creates new classes DisplayClasses to hold those variables)
// if using Task.Run to fire and forget some job, always try to manage exception handling inside, not outside! exception will not be propagated to caller for obvious reasons