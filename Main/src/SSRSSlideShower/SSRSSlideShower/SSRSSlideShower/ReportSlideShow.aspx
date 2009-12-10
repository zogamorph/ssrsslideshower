<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportSlideShow.aspx.cs" Inherits="SSRSSlideShower.ReportSlideShow" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="ReportServerSlideShowForm" runat="server">
    <div>
        <asp:Label ID="lblErrorMessage" ForeColor="Red" runat="server"/>
        <div style="height: 100%; vertical-align: middle; text-align:center; border: 1px solid black;">
            <rsweb:ReportViewer ID="ReportViewer" runat="server" Height="400px" 
                ProcessingMode="Remote" ShowCredentialPrompts="False" 
                ShowDocumentMapButton="False" ShowExportControls="False" 
                ShowFindControls="False" ShowPageNavigationControls="False" 
                ShowParameterPrompts="False" ShowPrintButton="False" 
                ShowPromptAreaButton="False" ShowRefreshButton="False" ShowToolBar="False" 
                ShowZoomControl="False" SizeToReportContent="True" Width="400px" 
                AsyncRendering="False">
            </rsweb:ReportViewer>
    </div>
    <input type="hidden" name="hidDisplayIndex" id="hidDisplayIndex" runat="server" />
    </div>
    <asp:scriptmanager ID="Scriptmanager" runat="server"></asp:scriptmanager>
    <asp:timer ID="ReportTimer" runat="server"></asp:timer>
    </form>
</body>
</html>
