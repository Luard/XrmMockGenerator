#region Imports

using System;
using System.Collections.Generic;

#endregion

namespace xrm_mock_Generator.Model.Generator
{
	public class ModelGeneratorParams
	{
		public string EntityName { get; set; }
		public Guid SelectedUserId { get; set; }
		public List<Guid> SelectedForms { get; set; }
	}
}
