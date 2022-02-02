using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xrm_mock_Generator
{
	public partial class SystemFormViewModel
	{
        public object ObjectTypeCode { get; internal set; }

        public override string ToString()
		{
			return Name;
		}
    }
}
