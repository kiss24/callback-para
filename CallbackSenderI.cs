using Demo;
using System;

public sealed class CallbackSenderI : CallbackSenderDisp_
{
    public override void initiateCallback(CallbackReceiverPrx proxy, Ice.Current current = null)
    {
        Console.Out.WriteLine("initiating callback");

        try
        {
            proxy.callback();
        }
        catch (System.Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }

    public override void shutdown(Ice.Current current = null)
    {
        Console.Out.WriteLine("Shutting down...");

        try
        {
            current.adapter.getCommunicator().shutdown();
        }
        catch (System.Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }
}