using Demo;

public sealed class CallbackReceiverI : CallbackReceiverDisp_
{
    public override void callback(string messageCallback, Ice.Current current)
    {
        System.Console.Out.WriteLine("callback: " + messageCallback);
    }
}