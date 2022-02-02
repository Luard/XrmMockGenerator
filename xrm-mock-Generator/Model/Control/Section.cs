#region Imports

using System.Collections.Generic;

#endregion

namespace xrm_mock_Generator.Model.Control
{
	public class Section
	{
		public string Id { get; set; }
		public IDictionary<int, string> Labels { get; set; }
		public bool IsVisible { get; set; }
		public List<Control.Abstract.Control> Controls { get; set; }
	}
}
