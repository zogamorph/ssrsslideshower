<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SSRSSlideShower._Default" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblErrorMessage" ForeColor="Red" runat="server">
        <span>Error: No &quot;Projects&quot; query string parameters specified.</span><br />
        <span>Usage: http://TFSHOST/ScrumforTeamSystem/ReportSlideShow.aspx?Projects=MyProject</span><br />
        <span>Usage: http://TFSHOST/ScrumforTeamSystem/ReportSlideShow.aspx?Projects=MyProject1,MyProject2</span><br />
    </asp:Label>
    <div style="height: 100%; vertical-align: middle; text-align:center; border: 1px solid black;">
        <rsweb:ReportViewer ID="ReportViewer" runat="server" ProcessingMode="Remote"
            SizeToReportContent="True" ShowToolBar="False" 
            ShowParameterPrompts="False" ShowCredentialPrompts="False"
            PromptAreaCollapsed="True" AsyncRendering="False" 
            ShowDocumentMapButton="False" ShowExportControls="False" 
            ShowFindControls="False" ShowPageNavigationControls="False" 
            ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" 
            ShowZoomControl="False">
        </rsweb:ReportViewer>
    </div>
    <input type="hidden" name="hidDisplayIndex" id="hidDisplayIndex" runat="server" />
    <input type="hidden" name="hidProjectNames" id="hidProjectNames" runat="server" />
    <asp:Timer ID="ReportPauseTimer" runat="server" />
    <asp:ScriptManager ID="ReportScriptManager" runat="server" />
    </div>
    </form>
</body>
</html>
