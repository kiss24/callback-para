using System;

public class server
{
    public static int Main(string[] args)
    {
        int status = 0;

        try
        {
            using (var communicator = Ice.Util.initialize(ref args, "config.server"))
            {
                Console.CancelKeyPress += (sender, EventArgs) => communicator.destroy();

                if (args.Length > 0)
                {
                    Console.Error.WriteLine("too many arguments");
                    status = 1;
                }
                else
                {
                    var adapter = communicator.createObjectAdapter("Callback.Server");
                    adapter.add(new CallbackSenderI(), Ice.Util.stringToIdentity("callbackSender"));
                    adapter.activate();
                    communicator.waitForShutdown();
                }
            }
        }
        catch (System.Exception ex)
        {
            Console.Error.WriteLine(ex);
            status = 1;
        }

        return status;
    }
}