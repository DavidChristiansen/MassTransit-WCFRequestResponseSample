using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SvcBus.WCFClient.Infrastructure.Proxies;

namespace SvcBus.WCFClient {
	class Program {
		private static ISampleServiceProxy svcProxy;
		static void Main(string[] args) {
			svcProxy = new SampleServiceProxy();
			for (int i = 0; i < 10; i++) {
				var question = "Are we there yet?";
				var answer = svcProxy.Ask(question);
				Console.WriteLine("{0} : {1} : {2}", i, DateTime.Now, answer);
				Thread.Sleep(2000);
			}
		}
	}
}
