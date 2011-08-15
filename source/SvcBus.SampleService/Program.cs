using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using SvcBus.SampleService.Messages;

namespace SvcBus.SampleService {
	class Program {
		static void Main(string[] args) {
			Bus.Initialize(sbc => {
				sbc.UseMsmq();
				sbc.VerifyMsmqConfiguration();
				sbc.UseMulticastSubscriptionClient();
				sbc.ReceiveFrom("msmq://localhost/samplemessage_responder");
				sbc.Subscribe(subs => subs.Handler<BasicRequest>(msg => Bus.Instance.MessageContext<BasicResponse>().Respond(new BasicResponse { Text = "RESP" + msg.Text })));
			});
		}
	}
}
