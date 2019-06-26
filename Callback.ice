#pragma once

module Demo 
{
    interface CallbackReceiver 
    {
        void callback();
    };

    interface CallbackSender
    {
        void initiateCallback(CallbackReceiver* proxy);
        void shutdown();
    };
};