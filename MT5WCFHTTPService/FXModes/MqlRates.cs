using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MT5WCFHTTPService.FXModes
{
	public class MqlRates : MtApi5.MqlRates
	{
		public DateTime time { get; set; }
	}
}