using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xrm_mock_Generator.Model.ViewModel;

namespace xrm_mock_Generator.Control
{
	public static class ControlData
	{
		public static bool? IsGenerateOnlineCode { get; set; }
		public static List<EntityNameViewModel> EntityNames = new List<EntityNameViewModel>();
		public static List<SystemFormViewModel> Forms = new List<SystemFormViewModel>();
		public static List<SystemFormViewModel> SelectedForms = new List<SystemFormViewModel>();
	}

	internal class RetrieveResult
	{
		internal IEnumerable<UserViewModel> Users { get; set; }
		internal IEnumerable<SystemFormViewModel> Forms { get; set; }
		internal IEnumerable<EntityNameViewModel> EntityNames { get; set; }
	}
}
