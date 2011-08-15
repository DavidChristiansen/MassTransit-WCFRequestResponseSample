using System.Runtime.Serialization;

namespace SvcBus.WCFService.Contracts {
	[DataContract(Namespace = Constants.Namespace)]
	public class SampleServiceRequest {
		[DataMember]
		public string Question { get; set; }
	}
}