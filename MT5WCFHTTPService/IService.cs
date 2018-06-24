using MtApi5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace MT5WCFHTTPService
{
	[ServiceContract]
	public interface IService
	{
		[OperationContract]
		[WebGet]
		double AccountBalance();

		[OperationContract]
		[WebGet]
		DateTime ServerTimeCurrent();

		[OperationContract]
		[WebGet]
		MqlTick SymbolInfoTick(string symbol);

		[OperationContract]
		[WebGet]
		double SymbolInfoDouble(string symbol);

		[OperationContract]
		[WebGet]
		//[WebInvoke(Method = "GET",
		//   RequestFormat = WebMessageFormat.Json,
		//   ResponseFormat = WebMessageFormat.Json)]
		List<FXModes.MqlRates> RatesByPositions(string symbol, string timeframe, int startPosition, int count);

		[OperationContract]
		[WebGet]
		List<FXModes.MqlRates> RatesByDates(string symbol, string timeframe, string startDateString, string endDateString);

		[OperationContract]
		[WebGet]
		FXModes.MqlRates CurrentIncompleteCandle(string symbol, string timeframe);
	}
}