using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xrm_mock_Generator.Model
{
    public class Settings
    {
		public Guid SelectedUserId;
		public bool? IsGenerateOnlineCode;
		public List<Guid> SelectedForms;
	}
}
