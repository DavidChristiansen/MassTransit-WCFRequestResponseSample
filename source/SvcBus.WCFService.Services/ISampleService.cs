using System.ServiceModel;
using SvcBus.WCFService.Contracts;

namespace SvcBus.WCFService.Services {
	[ServiceContract(SessionMode = SessionMode.NotAllowed, Namespace = Constants.Namespace)]
	public interface ISampleService {
		[OperationContract]
		SampleServiceResponse AskQuestion(SampleServiceRequest request);
	}
}