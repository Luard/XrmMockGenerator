#region Imports

using System.Linq;
using System.Xml;
using xrm_mock_Generator.Generator.Abstract;
using xrm_mock_Generator.Model.Constants;
using xrm_mock_Generator.Model.Control;

#endregion

namespace xrm_mock_Generator.Generator.Control
{
	public class ControlGenerator : IGeneratableFromXml<Model.Control.Abstract.Control>
	{
		public Model.Control.Abstract.Control Generate(XmlNode controlXml, XmlDocument doc)
		{
			var controlId = controlXml.SelectSingleNode("@id")?.Value;
			var isControlDisabled = controlXml.SelectSingleNode("@disabled")?.Value == "true";
			var controlName = controlId;
			var controlLabels = controlXml.SelectNodes(FormXmlContants.ControlLabelPath)?.Cast<XmlNode>()
				.ToDictionary(
					e => int.Parse(e.SelectSingleNode("@languagecode")?.Value ?? "1033"),
					e => e.SelectSingleNode("@description")?.Value);

			return
				FormXmlContants.GridClassIds.Contains(controlXml.SelectSingleNode("@classid")?.Value)
					? new GridControl
					  {
						  Name = controlName,
						  Labels = controlLabels
					  }
					: (Model.Control.Abstract.Control)
						new FieldControl
						{
							Name = controlName,
							Labels = controlLabels,
							IsDisabled = isControlDisabled,
							IsVisible = controlXml.SelectSingleNode("../@visible")?.Value != "false"
						};
		}
	}
}
