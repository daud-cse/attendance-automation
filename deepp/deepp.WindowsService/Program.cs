﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace deepp.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Service objSERVICE = new Service();

            objSERVICE.debug();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}