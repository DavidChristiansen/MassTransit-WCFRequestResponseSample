using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace SvcBus.WCFService.Host {
	public class WcfServiceWrapper<TServiceImplementation, TServiceContract> : ServiceBase
		where TServiceImplementation : TServiceContract {
		private readonly string _serviceUri;
		private ServiceHost _serviceHost;

		public WcfServiceWrapper(string serviceName, string serviceUri) {
			_serviceUri = serviceUri;
			ServiceName = serviceName;
		}
		public WcfServiceWrapper(string serviceName) {
			ServiceName = serviceName;
		}

		protected override void OnStart(string[] args) {
			Start();
		}

		protected override void OnStop() {
			Stop();
		}

		public void Start() {
			Console.WriteLine(ServiceName + " starting...");
			bool openSucceeded = false;
			try {
				if (_serviceHost != null) {
					_serviceHost.Close();
				}

				_serviceHost = new ServiceHost(typeof(TServiceImplementation));
			} catch (Exception e) {
				Console.WriteLine("Caught exception while creating {0}: {1}", ServiceName, e);
				return;
			}

			try {
				var webHttpBinding = new WSHttpBinding(SecurityMode.None);
				if (!string.IsNullOrEmpty(_serviceUri))
					_serviceHost.AddServiceEndpoint(typeof(TServiceContract), webHttpBinding, _serviceUri);
				_serviceHost.Open();
				openSucceeded = true;
			} catch (Exception ex) {
				Console.WriteLine("Caught exception while starting {0}: {1}", ServiceName, ex);
			} finally {
				if (!openSucceeded) {
					_serviceHost.Abort();
				}
			}


			if (_serviceHost.State == CommunicationState.Opened) {
				Console.WriteLine(ServiceName + " started");
			} else {
				Console.WriteLine(ServiceName + " failed to open");
				bool closeSucceeded = false;
				try {
					_serviceHost.Close();
					closeSucceeded = true;
				} catch (Exception ex) {
					Console.WriteLine("{0} failed to close: {1}", ServiceName, ex);
				} finally {
					if (!closeSucceeded) {
						_serviceHost.Abort();
					}
				}
			}
		}

		public new void Stop() {
			Console.WriteLine(ServiceName + " stopping...");
			try {
				if (_serviceHost != null) {
					_serviceHost.Close();
					_serviceHost = null;
				}
			} catch (Exception ex) {
				Console.WriteLine("Caught exception while stopping {0}: {1}", ServiceName, ex);
			} finally {
				Console.WriteLine(ServiceName + " stopped...");
			}
		}
	}
}