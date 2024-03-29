using Demo;
using System;

public sealed class CallbackSenderI : CallbackSenderDisp_
{
    public override void initiateCallback(CallbackReceiverPrx proxy, string message, Ice.Current current = null)
    {
        Console.Out.WriteLine("received " + message);

        try
        {
            proxy.callback(message);
        }
        catch (System.Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }
}