using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using xrm_mock_Generator.Generator;
using xrm_mock_Generator.Helpers;
using xrm_mock_Generator.Model;
using xrm_mock_Generator.Model.Generator;

namespace xrm_mock_Generator.Control
{
    public class Process
    {
		public IOrganizationService Service;
		String mainFolderName = "generated";
		public void Run()
        {
			connectCRM();

			Console.WriteLine("Retrieving data ...");
			// Console.WriteLine("Retrieving users ...");
			// var users = DataHelpers.RetrieveUsers(Service);
			Console.WriteLine("Retrieving entity names ...");
			var entityNames = DataHelpers.RetrieveEntityNames(Service);
			Console.WriteLine("Retrieving forms ...");
			var forms = DataHelpers.RetrieveForms(Service);

			String[] entitiesToGetForm = ConfigurationManager.AppSettings["entities"].Split(',');
			if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["folderName"]))
            {
				mainFolderName = ConfigurationManager.AppSettings["folderName"];
			}
			// String[] entitiesToGetForm = new string[] { "lead", "account" };

			foreach (String entityName in entitiesToGetForm)
            {
				foreach (var form in forms.Where(form => form.ObjectTypeCodeEntity == entityName))
                {
					AddSelectedForm(form);
				}
				generateForms(entityName);
				clearSelectedForms();
			}
		}

		private void connectCRM()
        {
			var connectionStrings = ConfigurationManager.ConnectionStrings;
			String Url = connectionStrings["Url"].ConnectionString;
			String Username = connectionStrings["Username"].ConnectionString;
			String Password = connectionStrings["Password"].ConnectionString;

			ClientCredentials clientCredentials = new ClientCredentials();
			clientCredentials.UserName.UserName = Username;
			clientCredentials.UserName.Password = Password;
			Uri uri = new Uri(Url);
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			Service = new OrganizationServiceProxy(uri, null, clientCredentials, null);
		}

		private void generateForms(String entityName)
        {
			var groupedForms = ControlData.SelectedForms.GroupBy(f => f.ObjectTypeCode).ToList();

			var modelGenerator = new ModelGenerator();
			var models = new List<XrmModel>();

			for (int i = 0; i < groupedForms.Count; i++)
			{
				var group = groupedForms[i];
				Console.WriteLine("Generating forms for " + entityName + " ...");

				models.Add(
					modelGenerator.Generate(
						Service,
						new ModelGeneratorParams
						{
							// TO-DO: Get User guid
							SelectedUserId = new Guid("606d5b7f-03a8-e611-80e6-c4346bad8114"),
							EntityName = group.FirstOrDefault<SystemFormViewModel>().ObjectTypeCodeEntity,
							SelectedForms = group.Select(g => g.Id.GetValueOrDefault()).ToList()
						}));
			}

			var templateModel =
				new T4TemplateModel
				{
					IsGenerateOnlineCode = ControlData.IsGenerateOnlineCode,
					Models = models
				};

			Console.WriteLine("Generating TypeScript code ...");
			XrmMockGeneratorTemplate page = new XrmMockGeneratorTemplate(templateModel);
			String pageContent = page.TransformText();
			createFile(entityName, entityName, pageContent);
		}

		private void createFile(String folderName, String fileName, String content)
        {
			String filePath = mainFolderName + "/" + folderName + "/" + fileName + ".model.ts";
			FileInfo file = new System.IO.FileInfo(filePath);
			file.Directory.Create();
			
			File.WriteAllText(filePath, content);

		}

		private void AddSelectedForm(SystemFormViewModel selectedForm)
		{
			if (ControlData.SelectedForms.Any(f => f.Id == selectedForm.Id))
			{
				return;
			}

			ControlData.SelectedForms.Add(selectedForm);
		}

		private void clearSelectedForms()
        {
			ControlData.SelectedForms.Clear();
		}
	}
}
