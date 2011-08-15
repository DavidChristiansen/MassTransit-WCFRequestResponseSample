using System.Runtime.Serialization;

namespace SvcBus.WCFService.Contracts {
	[DataContract(Namespace = Constants.Namespace)]
	public class SampleServiceResponse {
		[DataMember]
		public string Answer { get; set; }
	}
}