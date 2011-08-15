using System.ServiceModel;
using SvcBus.WCFService.Contracts;

namespace SvcBus.WCFService.Services {
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, Namespace = Constants.Namespace)]
	public class SampleService : ISampleService {
		public SampleServiceResponse AskQuestion(SampleServiceRequest request) {
			//Bus.Initialize(sbc => {
			//    sbc.UseMsmq();
			//    sbc.VerifyMsmqConfiguration();
			//    sbc.UseMulticastSubscriptionClient();
			//    sbc.ReceiveFrom("msmq://localhost/samplemessage_requestor");
			//});

			//Bus.Instance.PublishRequest(new BasicRequest(), x => {
			//    x.Handle<BasicResponse>(message => Console.WriteLine(message.Text));
			//    x.SetTimeout(30.Seconds());
			//});

			return new SampleServiceResponse() {
				Answer = "Maybe's aye maybe's naw"
			};
		}
	}
}
