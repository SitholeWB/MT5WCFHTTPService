using MT5WCFHTTPService.Helpers;
using MtApi5;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MT5WCFHTTPService
{
	public class Service : IService
	{
		private static MtApi5Client mtApi5Client = new MtApi5Client();

		public Service()
		{
			int i = 0;
			while (i < 5 && mtApi5Client.ConnectionState != Mt5ConnectionState.Connected)
			{
				i++;
				try
				{
					mtApi5Client.BeginConnect("localhost", 8228);
				}
				catch (Exception e)
				{
					Console.WriteLine(DateTime.Now + " <<>> " + e.Message);
					Task.Delay(70000);
					Console.WriteLine(DateTime.Now + " <<!!>> " + e.Message);
				}
			}
		}

		public double GetAccountBalance()
		{
			RetryConnecting();
			return mtApi5Client.AccountInfoDouble(ENUM_ACCOUNT_INFO_DOUBLE.ACCOUNT_BALANCE);
		}

		public DateTime GetServerTimeCurrent()
		{
			RetryConnecting();
			return mtApi5Client.TimeCurrent();
		}

		public MqlTick GetSymbolInfoTick(string symbol)
		{
			RetryConnecting();
			mtApi5Client.SymbolInfoTick(symbol, out MqlTick mqlTick);
			return mqlTick;
		}

		public double SymbolInfoDouble(string symbol)
		{
			RetryConnecting();
			return mtApi5Client.SymbolInfoDouble(symbol, ENUM_SYMBOL_INFO_DOUBLE.SYMBOL_POINT);
		}

		public List<FXModes.MqlRates> GetRatesByPositions(string symbol, string timeframe, int startPosition, int count)
		{
			RetryConnecting();
			if (count <= 0 || startPosition < 0)
			{
				return new List<FXModes.MqlRates>();
			}
			ENUM_TIMEFRAMES enumTimeframe = (ENUM_TIMEFRAMES)Enum.Parse(typeof(ENUM_TIMEFRAMES), timeframe);
			var candles = new MqlRates[count];
			mtApi5Client.CopyRates(symbol, enumTimeframe, startPosition, count, out candles);

			//This is done so that time is not ignored from xml
			var newCandles = new List<FXModes.MqlRates>();
			foreach (var m in candles)
			{
				newCandles.Add(new FXModes.MqlRates
				{
					time = m.time,
					close = m.close,
					high = m.high,
					low = m.low,
					mt_time = m.mt_time,
					open = m.open,
					real_volume = m.real_volume,
					spread = m.spread,
					tick_volume = m.tick_volume
				});
			}
			return newCandles;
		}

		// </summary>
		// <param name="timeframe">Timeframe to do scanning on</param>
		// <param name="startDateString">Lower bound date in format yyyyMMddHHmmss</param>
		// <param name="endDateString">Upper bound date in format yyyyMMddHHmmss</param>
		// <returns>List of Engulfings</returns>
		public List<FXModes.MqlRates> GetRatesByDates(string symbol, string timeframe, string startDateString, string endDateString)
		{
			RetryConnecting();
			var startDate = DateTime.ParseExact(startDateString, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
			var endDate = DateTime.ParseExact(endDateString, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

			if ((endDate - startDate).TotalMinutes <= TimeframeHelper.GetMinutesFromForTimeframe(timeframe))
			{
				return new List<FXModes.MqlRates>();
			}
			if ((mtApi5Client.TimeCurrent() - startDate).TotalMinutes <= TimeframeHelper.GetMinutesFromForTimeframe(timeframe))
			{
				return new List<FXModes.MqlRates>();
			}

			ENUM_TIMEFRAMES enumTimeframe = (ENUM_TIMEFRAMES)Enum.Parse(typeof(ENUM_TIMEFRAMES), timeframe);
			int size = (int)((endDate - startDate).TotalMinutes / (TimeframeHelper.GetMinutesFromForTimeframe(timeframe)));
			var candles = new MqlRates[size];

			mtApi5Client.CopyRates(symbol, enumTimeframe, startDate, endDate, out candles);
			//This is done so that time is not ignored from xml
			var newCandles = new List<FXModes.MqlRates>();
			foreach (var m in candles)
			{
				newCandles.Add(new FXModes.MqlRates
				{
					time = m.time,
					close = m.close,
					high = m.high,
					low = m.low,
					mt_time = m.mt_time,
					open = m.open,
					real_volume = m.real_volume,
					spread = m.spread,
					tick_volume = m.tick_volume
				});
			}
			return newCandles;
		}

		public FXModes.MqlRates GetCurrentIncompleteCandle(string symbol, string timeframe)
		{
			RetryConnecting();
			ENUM_TIMEFRAMES enumTimeframe = (ENUM_TIMEFRAMES)Enum.Parse(typeof(ENUM_TIMEFRAMES), timeframe);
			var candles = new MqlRates[1];

			mtApi5Client.CopyRates(symbol, enumTimeframe, 0, 1, out candles);

			var m = candles[0];
			//This is done so that time is not ignored from xml
			return new FXModes.MqlRates
			{
				time = m.time,
				close = m.close,
				high = m.high,
				low = m.low,
				mt_time = m.mt_time,
				open = m.open,
				real_volume = m.real_volume,
				spread = m.spread,
				tick_volume = m.tick_volume
			};
		}

		#region private methods

		private static void RetryConnecting()
		{
			int i = 0;
			while (i < 5 && mtApi5Client.ConnectionState != Mt5ConnectionState.Connected)
			{
				i++;
				try
				{
					mtApi5Client.BeginConnect("localhost", 8228);
					mtApi5Client.TimeCurrent();
				}
				catch (Exception e)
				{
					Console.WriteLine(DateTime.Now + " <<>> " + e.Message);
					Task.Delay(5000);
					Console.WriteLine(DateTime.Now + " <<!!>> " + e.Message);
				}
			}
		}

		#endregion private methods
	}
}