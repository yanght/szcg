using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Topshelf;

namespace Szcg.TaskScheduler
{
    class Program
    {
       
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

            HostFactory.Run(x =>
            {
                //x.UseLog4Net();

                x.Service<ServiceRunner>();

                x.SetDescription("Szcg.TaskScheduler");
                x.SetDisplayName("Szcg.TaskScheduler");
                x.SetServiceName("Szcg.TaskScheduler");

                x.EnablePauseAndContinue();
            });



            Console.ReadLine();
        }
    }
}
