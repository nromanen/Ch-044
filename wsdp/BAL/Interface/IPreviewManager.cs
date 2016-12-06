using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface {
	public interface IPreviewManager {
		string GetPreviewForOneInput(string url, string xpath);
	}
}
