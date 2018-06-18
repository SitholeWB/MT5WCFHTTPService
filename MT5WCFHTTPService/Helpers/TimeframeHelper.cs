using MtApi5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MT5WCFHTTPService.Helpers
{
	public static class TimeframeHelper
	{
		public static int GetMinutesFromForTimeframe(string timeframe)
		{
			switch ((ENUM_TIMEFRAMES)Enum.Parse(typeof(ENUM_TIMEFRAMES), timeframe))
			{
				case ENUM_TIMEFRAMES.PERIOD_MN1:
					return 43800;

				case ENUM_TIMEFRAMES.PERIOD_W1:
					return 10080;

				case ENUM_TIMEFRAMES.PERIOD_D1:
					return 1440;

				case ENUM_TIMEFRAMES.PERIOD_H4:
					return 240;

				case ENUM_TIMEFRAMES.PERIOD_H1:
					return 60;

				case ENUM_TIMEFRAMES.PERIOD_M30:
					return 30;

				case ENUM_TIMEFRAMES.PERIOD_M15:
					return 15;

				case ENUM_TIMEFRAMES.PERIOD_M5:
					return 5;

				case ENUM_TIMEFRAMES.PERIOD_M1:
					return 1;
			}
			throw new Exception($"Failed to GetMinutesFromForTimeframe for {timeframe}");
		}
	}
}