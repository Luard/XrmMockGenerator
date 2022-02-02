#region Imports

using System.Collections.Generic;

#endregion

namespace xrm_mock_Generator.Model
{
	public class XrmModel
	{
		public string EntityName { get; set; }
		public Context Context { get; set; }
		public List<CrmAttribute> CrmAttributes { get; set; }
		public List<Form> Forms { get; set; }
	}
}
