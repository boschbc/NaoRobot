﻿using System;
using System.Net;
using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Testing.GUI;

namespace Naovigate
{
    class MainProgram
    {
        public static void Main(String[] args)
        {
            ShutDownHook();
            //Use this switch to deactivate debugger invocation:
            bool Debug = true;
            string localhost = "127.0.0.1";
            string orange = "192.168.0.109";
            if (Debug)
                LaunchDebugger.DebugMain(orange);

        }

        private static void ShutDownHook()
        {
             AppDomain.CurrentDomain.ProcessExit += Cleanup;  
        }

        private static void Cleanup(object sender, EventArgs e)
        {
            Console.WriteLine("Shutting down...");
            NaoState.Disconnect();
        }
    }
}
