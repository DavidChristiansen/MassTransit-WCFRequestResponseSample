using System;
using System.ServiceModel;
using System.Threading;
using SvcBus.WCFClient.WebSampleServiceReference;

namespace SvcBus.WCFClient.Infrastructure.Proxies {
	public class WebSampleServiceProxy : ServiceProxyBase, ISampleServiceProxy {
		public string Ask(string question) {
			SampleServiceClient proxy = null;
			string response = null;
			int callCount = 0;
			bool callCompleted = false;
			bool shouldRetry = true;
			while (!callCompleted && callCount < MaxRetryCount && shouldRetry) {
				callCount++;
				try {
					proxy = new SampleServiceClient();
					var svcResponse = proxy.AskQuestion(new SampleServiceRequest {
						Question = "Are we nearly there yet?"
					});
					if (svcResponse != null)
						response = svcResponse.Answer;
					callCompleted = true;
				} catch (EndpointNotFoundException ex) {
					if (callCount >= MaxRetryCount && !callCompleted) {
						throw;
					} else {
						Console.WriteLine("Can't find service - going to sit here patiently for 2seconds before trying again.");
						Thread.Sleep(2000);
						// Do nothing
					}
				} catch (Exception ex) {
					shouldRetry = false;
				} finally {
					if (proxy != null) {
						try {
							if (proxy.State == CommunicationState.Opened)
								proxy.Close();
							else if (proxy.State == CommunicationState.Faulted)
								proxy.Abort();
						} catch (CommunicationException) {
							proxy.Abort();
						} catch (TimeoutException) {
							proxy.Abort();
						}
					}
				}
			}
			return response;
		}
	}
}