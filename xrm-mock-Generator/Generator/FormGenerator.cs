#region Imports

using System;
using System.Linq;
using System.Xml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using xrm_mock_Generator.Generator.Control;
using xrm_mock_Generator.Model;
using xrm_mock_Generator.Model.Constants;

#endregion

namespace xrm_mock_Generator.Generator
{
	public class FormGenerator
	{
		public Form Generate(IOrganizationService service, Guid formId)
		{
			var crmForm = service.Retrieve(SystemForm.EntityLogicalName, formId,
				new ColumnSet(SystemForm.Fields.FormIdId, SystemForm.Fields.Name, SystemForm.Fields.FormXml))
				.ToEntity<SystemForm>();

			var formXml = crmForm.FormXml;
			var doc = new XmlDocument();
			doc.LoadXml(formXml);

			return
				new Form
				{
					Id = formId.ToString(),
					Name = crmForm.Name,
					Tabs = doc.SelectNodes(FormXmlContants.TabPath)?.Cast<XmlNode>()
						.Select(xmlNode => new TabGenerator().Generate(xmlNode, doc)).ToList()
				};
		}
	}
}
