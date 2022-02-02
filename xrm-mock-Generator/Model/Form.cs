#region Imports

using System.Collections.Generic;
using xrm_mock_Generator.Model.Control;

#endregion

namespace xrm_mock_Generator.Model
{
	public class Form
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public List<Tab> Tabs { get; set; }
	}
}
