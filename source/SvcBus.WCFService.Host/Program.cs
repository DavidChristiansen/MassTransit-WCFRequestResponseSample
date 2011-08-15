using System;
using SvcBus.WCFService.Services;
using Topshelf;

namespace SvcBus.WCFService.Host {
	class Program {
		static void Main(string[] args) {
			const string serviceUri = "http://localhost:10000/SampleService";
			var host = HostFactory.New(c => {
				c.Service<WcfServiceWrapper<SampleService, ISampleService>>(s => {
							s.SetServiceName("CalculatorService");
							s.ConstructUsing(x => new WcfServiceWrapper<SampleService, ISampleService>("SampleService"));
							s.WhenStarted(service => service.Start());
							s.WhenStopped(service => service.Stop());
						});
					c.RunAsLocalSystem();

					c.SetDescription("Runs SampleService.");
					c.SetDisplayName("SampleService");
					c.SetServiceName("SampleService");
				});

			Console.WriteLine("Hosting ...");
			host.Run();
			Console.WriteLine("Done hosting ...");
		}
	}
}
