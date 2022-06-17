using System.Runtime.CompilerServices;

namespace Async;

public class AwaitableClass
{
    public bool IsSomething { get; private set; }

    public async Task Execute()
    {
        Console.WriteLine("awaiting starts");
        await Task.Delay(1_000);
        this.IsSomething = true;
        Console.WriteLine("awaiting ends");
    }

    public TaskAwaiter GetAwaiter()
    {
        //return new AwaitableClassAwaiter(this);
        return Execute().GetAwaiter();
    }

    public class AwaitableClassAwaiter : ICriticalNotifyCompletion
    {
        private readonly Task task;

        //FOR COMPILER THERE IS NO INTERFACE!
        public bool IsCompleted
        {
            get { return this.task.IsCompleted; }
        }

        //FOR COMPILER THERE IS NO INTERFACE!
        public void GetResult()
        {
            this.task.GetAwaiter().GetResult();
        }

        public AwaitableClassAwaiter(AwaitableClass awaitableClass)
        {
            this.task = awaitableClass.Execute();
        }

        // CALLS MOVENEXT IN STATE MACHINE
        /// <inheritdoc />
        public void OnCompleted(Action continuation)
        {
            this.task.ContinueWith(_ => continuation);
        }

        // CALLS MOVENEXT IN STATE MACHINE
        /// <inheritdoc />
        public void UnsafeOnCompleted(Action continuation)
        {
            this.task.ContinueWith(_ => continuation);
        }
    }
}
