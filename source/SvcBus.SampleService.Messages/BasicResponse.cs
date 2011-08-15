using System;
using MassTransit;

namespace SvcBus.SampleService.Messages {
	public class BasicResponse : CorrelatedBy<Guid> {
		public Guid CorrelationId { get; set; }
		public string Text { get; set; }
	}
}