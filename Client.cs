using Demo;
using System;

public class Client
{
    private static CallbackSenderPrx sender;
    private static CallbackReceiverPrx receiver;
    private static Ice.ObjectAdapter adapter;

    public static int Main(string[] args)
    {
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
                        Menu();
                        break;
                    case 1:
                        Init();
                        break;
                    case 2:
                        string message;
                        message = Console.In.ReadLine();
                        sender.initiateCallback(receiver, message);
                        break;
                    case 3:
                        return 0;
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
        while (!key.Equals(3));

        return 0;
    }

    private static void Menu()
    {
        Console.Out.Write("usage:\n"
                          + "0: help\n"
                          + "1: init\n"
                          + "2: send callback\n"
                          + "3: exit\n");
    }

    private static int Init()
    {
        int result = 0;

        try
        {
            Ice.Communicator communicator = Ice.Util.initialize("config.client");

            sender = CallbackSenderPrxHelper.checkedCast(communicator.propertyToProxy("CallbackSender.Proxy").
                                                 ice_twoway().ice_timeout(-1).ice_secure(false));
            if (sender == null)
            {
                Console.Error.WriteLine("invalid proxy");
                result = 0;
            }

            adapter = communicator.createObjectAdapter("Callback.Client");
            adapter.add(new CallbackReceiverI(), Ice.Util.stringToIdentity("callbackReceiver"));
            adapter.activate();

            receiver = CallbackReceiverPrxHelper.uncheckedCast(
                adapter.createProxy(Ice.Util.stringToIdentity("callbackReceiver")));

            Console.WriteLine("server connected");

            result = 1;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);

            Console.WriteLine("connection failed");
            result = 0;
        }

        return result;
    }
}