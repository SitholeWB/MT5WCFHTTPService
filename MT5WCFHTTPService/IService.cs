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
		double GetAccountBalance();

		[OperationContract]
		[WebGet]
		DateTime GetServerTimeCurrent();

		[OperationContract]
		[WebGet]
		MqlTick GetSymbolInfoTick(string symbol);

		[OperationContract]
		[WebGet]
		double SymbolInfoDouble(string symbol);

		[OperationContract]
		[WebGet]
		//[WebInvoke(Method = "GET",
		//   RequestFormat = WebMessageFormat.Json,
		//   ResponseFormat = WebMessageFormat.Json)]
		List<FXModes.MqlRates> GetRatesByPositions(string symbol, string timeframe, int startPosition, int count);

		[OperationContract]
		[WebGet]
		List<FXModes.MqlRates> GetRatesByDates(string symbol, string timeframe, string startDateString, string endDateString);

		[OperationContract]
		[WebGet]
		FXModes.MqlRates GetCurrentIncompleteCandle(string symbol, string timeframe);
	}
}