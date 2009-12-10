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

    using App_Code;

    using Microsoft.Reporting.WebForms;

    #endregion

    /// <summary>
    /// The default page code behind.
    /// </summary>
    public partial class ReportSlideShow : Page
    {
        #region Constants and Fields

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
            this.lblErrorMessage.Visible = false;
            this.ReportViewer.Visible = true;
            string path = HttpContext.Current.Request.MapPath(ConfigurationManager.AppSettings[REPORT_SLIDESHOW_LIST]);
            int index = string.IsNullOrEmpty(this.hidDisplayIndex.Value)
                            ? -1
                            : Convert.ToInt32(this.hidDisplayIndex.Value);

            this.ReportViewer.Reset();
            this.ReportViewer.ServerReport.ReportServerCredentials = new SlideShowReportServerCredentials();
            this.ReportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings[REPORT_SERVER_URL]);

            ReportServerSlideShow reportServerSlideShow = new ReportServerSlideShow(path);
            ReportSlide nextReportSlide = reportServerSlideShow.GetNextReportSlide(index);
         
            if (nextReportSlide == null)
            {
                this.lblErrorMessage.Visible = true;
                this.lblErrorMessage.Text = "There was an error";
                this.ReportViewer.Visible = false;
            }
            else
            {
                this.ReportViewer.ServerReport.ReportPath = nextReportSlide.ReportPath;
                if (nextReportSlide.ReportParameters.Any())
                {
                    this.ReportViewer.ServerReport.SetParameters(nextReportSlide.ReportParameters);
                }
                this.ReportTimer.Interval = nextReportSlide.DisplaySeconds * 1000;
                this.hidDisplayIndex.Value = nextReportSlide.ReportSlideNumber.ToString();
                this.ReportViewer.ServerReport.Refresh();
            }
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