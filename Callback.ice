#pragma once

module Demo 
{
    interface CallbackReceiver 
    {
        void callback(string messageCallback);
    };

    interface CallbackSender
    {
        void initiateCallback(CallbackReceiver* proxy, string message);
    };
};