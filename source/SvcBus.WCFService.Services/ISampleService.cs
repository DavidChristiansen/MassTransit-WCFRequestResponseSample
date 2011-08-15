using System.ServiceModel;
using SvcBus.WCFService.Contracts;

namespace SvcBus.WCFService.Host {
	[ServiceContract(SessionMode = SessionMode.NotAllowed, Namespace = Constants.Namespace)]
	public interface ISampleService {
		[OperationContract]
		SampleServiceResponse AskQuestion(SampleServiceRequest request);
	}
}