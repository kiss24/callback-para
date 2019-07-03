using Demo;
using System;

public class Client
{
    public static int Main(string[] args)
    {
        int status = 0;

        try
        {
            //
            // The new communicator is automatically destroyed (disposed) at the end of the
            // using statement
            //
            using (var communicator = Ice.Util.initialize(ref args, "config.client"))
            {
                //
                // The communicator initialization removes all Ice-related arguments from args
                //
                if (args.Length > 0)
                {
                    Console.Error.WriteLine("too many arguments");
                    status = 1;
                }
                else
                {
                    status = Run(communicator);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            status = 1;
        }

        return status;
    }

    private static int Run(Ice.Communicator communicator)
    {
        var sender = CallbackSenderPrxHelper.checkedCast(communicator.propertyToProxy("CallbackSender.Proxy").
                                                         ice_twoway().ice_timeout(-1).ice_secure(false));
        if (sender == null)
        {
            Console.Error.WriteLine("invalid proxy");
            return 1;
        }

        var adapter = communicator.createObjectAdapter("Callback.Client");
        adapter.add(new CallbackReceiverI(), Ice.Util.stringToIdentity("callbackReceiver"));
        adapter.activate();

        var receiver = CallbackReceiverPrxHelper.uncheckedCast(
            adapter.createProxy(Ice.Util.stringToIdentity("callbackReceiver")));

        Menu();

        int key = -1;
        do
        {
            try
            {
                Console.Out.Write("==> ");
                Console.Out.Flush();
                key = Convert.ToInt32(Console.In.ReadLine());

                if (key == -1)
                    break;

                switch (key)
                {
                    case 0:
                        string message;
                        message = Console.In.ReadLine();
                        sender.initiateCallback(receiver, message);
                        break;
                    case 1:
                        sender.shutdown();
                        break;
                    case 2:
                        break;
                    case 3:
                        Menu();
                        break;
                    default:
                        Console.Out.WriteLine("unknown command `" + key + "'");
                        Menu();
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
        while (!key.Equals(2));

        return 0;
    }

    private static void Menu()
    {
        Console.Out.Write("usage:\n"
                          + "0: send callback\n"
                          + "1: shutdown server\n"
                          + "2: exit\n"
                          + "3: help\n");
    }
}