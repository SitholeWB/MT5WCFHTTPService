using MT5WCFHTTPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace MT5HttpService
{
	public class Program
	{
		private static void Main(string[] args)
		{
			WebServiceHost host = new WebServiceHost(typeof(Service), new Uri("http://localhost:8000/"));
			try
			{
				ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IService), new WebHttpBinding(), "");
				host.Open();
				using (ChannelFactory<IService> cf = new ChannelFactory<IService>(new WebHttpBinding(), "http://localhost:8000"))
				{
					cf.Endpoint.Behaviors.Add(new WebHttpBehavior());

					IService channel = cf.CreateChannel();

					Console.WriteLine("Calling GetAccountBalance via HTTP GET: ");
					var s = channel.GetAccountBalance();
					Console.WriteLine("   Output: {0}", s);

					Console.WriteLine("");
					Console.WriteLine("This can also be accomplished by navigating to");
					Console.WriteLine("http://localhost:8000/GetAccountBalance");
					Console.WriteLine("Calls with parameters can be done like...");
					Console.WriteLine("http://localhost:8000/GetCurrentIncompleteCandle?symbol=EURUSD&timeframe=PERIOD_D1");
					Console.WriteLine("in a web browser while this sample is running.");

					Console.WriteLine("");
				}

				Console.WriteLine("Press <ENTER> to terminate");
				Console.ReadLine();

				host.Close();
			}
			catch (CommunicationException cex)
			{
				Console.WriteLine("An exception occurred: {0}", cex.Message);
				host.Abort();
				Console.ReadLine();
			}
		}
	}
}