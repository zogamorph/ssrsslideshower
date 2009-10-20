// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="SSRS Slide Shower Development Team">
//   Copyright (c) 2009
// </copyright>
// <summary>
//   The _ default.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Reporting.WebForms;

namespace SSRSSlideShower
{
    #region Directives

    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Security.Principal;
    using System.Web;
    using System.Web.UI;
    using System.Xml.Linq;

    using Microsoft.Reporting.WebForms;

    #endregion

    /// <summary>
    /// The default page code behind.
    /// </summary>
    public partial class _Default : Page
    {
        #region Constants and Fields

        /// <summary>
        /// The attrib display seconds.
        /// </summary>
        private const string ATTRIB_DISPLAY_SECONDS = "DisplaySeconds";

        /// <summary>
        /// The default pause duration.
        /// </summary>
        private const int DEFAULT_PAUSE_DURATION = 30;

        /// <summary>
        /// The report server url.
        /// </summary>
        private const string REPORT_SERVER_URL = "ReportServer";

        /// <summary>
        /// The report slide show list.
        /// </summary>
        private const string REPORT_SLIDESHOW_LIST = "ReportSlideShowList";

        #endregion

        #region Methods

        /// <summary>
        /// The page load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int duration;

            this.lblErrorMessage.Visible = false;

            string path = HttpContext.Current.Request.MapPath(ConfigurationManager.AppSettings[REPORT_SLIDESHOW_LIST]);

            XDocument reportsList = XDocument.Load(path);

            this.ReportViewer.Reset();
            this.ReportViewer.ServerReport.ReportServerCredentials = new SlideShowReportServerCredentials();
            this.ReportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings[REPORT_SERVER_URL]);
            XElement[] elements = reportsList.Root.Descendants("Report").ToArray();

            if (elements.Length == 0)
            {
                return;
            }

            int index;
           
            // If we have already displayed a report, increment the index
            if (this.hidDisplayIndex.Value != string.Empty)
            {
                int maxNumber = elements.Length;
                index = Convert.ToInt32(this.hidDisplayIndex.Value);
                index = index == (maxNumber - 1) ? 0 : index + 1;
                
            }
            else
            {
                index = 0;
            }

            XElement reportItem = elements[index];

            this.hidDisplayIndex.Value = index.ToString();

            if (reportItem.Attribute("DisplaySeconds") != null)
            {
                if (int.TryParse(reportItem.Attribute("DisplaySeconds").Value, out duration))
                {
                    this.ReportPauseTimer.Interval = duration;
                }
                else
                {
                    this.ReportPauseTimer.Interval = DEFAULT_PAUSE_DURATION;
                }
            }
            else
            {
                this.ReportPauseTimer.Interval = DEFAULT_PAUSE_DURATION;
            }

            this.ReportViewer.ServerReport.ReportPath = reportItem.Attribute("ReportPath").Value;
            this.ReportViewer.ServerReport.Refresh();
        }

        #endregion

        /// <summary>
        /// This is the Scrum Report Server Credentials 
        /// </summary>
        internal class SlideShowReportServerCredentials : IReportServerCredentials
        {
            #region Properties

            /// <summary>
            /// Gets the Windows Identity of Impersonation User
            /// </summary>
            WindowsIdentity IReportServerCredentials.ImpersonationUser
            {
                get
                {
                    return null;
                }
            }

            /// <summary>
            /// Gets the Network Credentials of the user
            /// </summary>
            ICredentials IReportServerCredentials.NetworkCredentials
            {
                get
                {
                    return CredentialCache.DefaultNetworkCredentials;
                }
            }

            #endregion

            #region Implemented Interfaces

            #region IReportServerCredentials

            /// <summary>
            /// Will Return <c>false</c> as this method is not used
            /// </summary>
            /// <param name="authCookie">
            /// The Auth Cookie
            /// </param>
            /// <param name="userName">
            /// The User Name
            /// </param>
            /// <param name="password">
            /// The Password
            /// </param>
            /// <param name="authority">
            /// The Authority
            /// </param>
            /// <returns>
            /// <c>false</c> as this not used
            /// </returns>
            bool IReportServerCredentials.GetFormsCredentials(
                out Cookie authCookie, out string userName, out string password, out string authority)
            {
                authCookie = null;
                userName = string.Empty;
                password = string.Empty;
                authority = string.Empty;
                return false;
            }

            #endregion

            #endregion
        }
    }
}