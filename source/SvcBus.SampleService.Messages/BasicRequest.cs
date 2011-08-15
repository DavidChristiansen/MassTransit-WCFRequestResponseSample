using System;
using MassTransit;

namespace SvcBus.SampleService.Messages {
	public class BasicRequest : CorrelatedBy<Guid> {
		public Guid CorrelationId { get; set; }
		public string Text { get; set; }
	}
}
