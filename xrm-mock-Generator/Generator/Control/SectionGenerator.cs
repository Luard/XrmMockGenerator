#region Imports

using System.Linq;
using System.Xml;
using xrm_mock_Generator.Generator.Abstract;
using xrm_mock_Generator.Model.Constants;
using xrm_mock_Generator.Model.Control;

#endregion

namespace xrm_mock_Generator.Generator.Control
{
	public class SectionGenerator : IGeneratableFromXml<Section>
	{
		public Section Generate(XmlNode sectionXml, XmlDocument doc)
		{
			return
				new Section
				{
					Id = sectionXml.SelectSingleNode("@name")?.Value,
					Labels = sectionXml.SelectNodes(FormXmlContants.LabelPath)?.Cast<XmlNode>()
						.ToDictionary(
							e => int.Parse(e.SelectSingleNode("@languagecode")?.Value ?? "1033"),
							e => e.SelectSingleNode("@description")?.Value),
					IsVisible = sectionXml.SelectSingleNode("@visible")?.Value != "false",
					Controls = sectionXml.SelectNodes(FormXmlContants.ControlPath)?.Cast<XmlNode>()
						.Select(xmlNode => new ControlGenerator().Generate(xmlNode, doc)).ToList()
				};
		}
	}
}
