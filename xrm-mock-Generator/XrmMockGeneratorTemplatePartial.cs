using System.Collections.Generic;
using xrm_mock_Generator.Model;

namespace xrm_mock_Generator
{
    public partial class XrmMockGeneratorTemplate
    {
		private T4TemplateModel  templateModel;

	    public XrmMockGeneratorTemplate(T4TemplateModel templateModel)
	    {
		    this.templateModel = templateModel;
	    }
    }
}
