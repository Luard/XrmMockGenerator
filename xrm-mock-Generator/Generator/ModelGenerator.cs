#region Imports

using System.Linq;
using Microsoft.Xrm.Sdk;
using xrm_mock_Generator.Model;
using xrm_mock_Generator.Model.Generator;

#endregion

namespace xrm_mock_Generator.Generator
{
	public class ModelGenerator
	{
		public XrmModel Generate(IOrganizationService service, ModelGeneratorParams parameters)
		{
			var form = new FormGenerator();

			return
				new XrmModel
				{
					EntityName = parameters.EntityName,
					Context = new ContextGenerator().Generate(service, parameters.SelectedUserId),
					CrmAttributes = new AttributeGenerator().Generate(service, parameters.EntityName).ToList(),
					Forms = parameters.SelectedForms.Select(f => form.Generate(service, f)).ToList()
				};
		}
	}
}
