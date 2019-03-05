﻿using System;
using System.Xml.Serialization;

namespace taskt.Core.Automation.Commands
{
    [Serializable]
    [Attributes.ClassAttributes.Group("Excel Commands")]
    [Attributes.ClassAttributes.Description("This command allows you to activate a specific worksheet in a workbook")]
    [Attributes.ClassAttributes.UsesDescription("Use this command when you want to switch to a specific worksheet")]
    [Attributes.ClassAttributes.ImplementationDescription("This command implements Excel Interop to achieve automation.")]
    public class ExcelActivateSheetCommand : ScriptCommand
    {
        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Please Enter the instance name")]
        [Attributes.PropertyAttributes.InputSpecification("Enter the unique instance name that was specified in the **Create Excel** command")]
        [Attributes.PropertyAttributes.SampleUsage("**myInstance** or **seleniumInstance**")]
        [Attributes.PropertyAttributes.Remarks("Failure to enter the correct instance name or failure to first call **Create Excel** command will cause an error")]
        [Attributes.PropertyAttributes.PropertyUIHelper(Attributes.PropertyAttributes.PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        public string v_InstanceName { get; set; }
        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Indicate the name of the sheet within the Workbook to activate")]
        [Attributes.PropertyAttributes.InputSpecification("Specify the name of the actual sheet")]
        [Attributes.PropertyAttributes.SampleUsage("Sheet1, mySheetName, [vSheet]")]
        [Attributes.PropertyAttributes.Remarks("")]
        public string v_SheetName { get; set; }
        public ExcelActivateSheetCommand()
        {
            this.CommandName = "ExcelActivateSheetCommand";
            this.SelectionName = "Activate Sheet";
            this.CommandEnabled = true;
        }
        public override void RunCommand(object sender)
        {
            var engine = (Core.Automation.Engine.AutomationEngineInstance)sender;
            var vInstance = v_InstanceName.ConvertToUserVariable(engine);

            var excelObject = engine.GetAppInstance(vInstance);

            Microsoft.Office.Interop.Excel.Application excelInstance = (Microsoft.Office.Interop.Excel.Application)excelObject;
                string sheetToDelete = v_SheetName.ConvertToUserVariable(sender);
                Microsoft.Office.Interop.Excel.Worksheet workSheet = excelInstance.Sheets[sheetToDelete];
                workSheet.Select();



            
        }
        public override string GetDisplayValue()
        {
            return base.GetDisplayValue() + " [Sheet Name: " + v_SheetName + ", Instance Name: '" + v_InstanceName + "']";
        }
    }
}