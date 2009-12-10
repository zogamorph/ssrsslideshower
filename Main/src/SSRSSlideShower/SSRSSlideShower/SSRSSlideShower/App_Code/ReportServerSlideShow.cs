// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportServerSlideShow.cs" company="SSRS Slide Shower Development Team">
//   Copyright (c) 2009
// </copyright>
// <summary>
//   The report server slide show.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SSRSSlideShower.App_Code
{
    #region Directives

    using System.Linq;
    using System.Xml.Linq;

    using Microsoft.Reporting.WebForms;

    #endregion

    /// <summary>
    /// The report server slide show.
    /// </summary>
    public class ReportServerSlideShow
    {
        #region Constants and Fields

        /// <summary>
        /// The attrib display seconds.
        /// </summary>
        private const string ATTRIB_DISPLAY_SECONDS_NAME = "DisplaySeconds";

        /// <summary>
        /// The attri b_ reportpat h_ name.
        /// </summary>
        private const string ATTRIB_REPORTPATH_NAME = "ReportPath";

        /// <summary>
        /// The default pause duration.
        /// </summary>
        private const int DEFAULT_PAUSE_DURATION = 30;

        /// <summary>
        /// The elelment  report name.
        /// </summary>
        private const string ELELMENT_REPORT_NAME = "Report";

        /// <summary>
        /// The reports list.
        /// </summary>
        private XDocument reportsList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportServerSlideShow"/> class.
        /// </summary>
        /// <param name="configurationFile">
        /// The configuration file.
        /// </param>
        public ReportServerSlideShow(string configurationFile)
        {
            this.reportsList = XDocument.Load(configurationFile);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get next report slide.
        /// </summary>
        /// <param name="CurrentSlideNumber">
        /// The current slide number.
        /// </param>
        /// <returns>
        /// </returns>
        public ReportSlide GetNextReportSlide(int CurrentSlideNumber)
        {
            ReportSlide nextReportSlide = new ReportSlide();
            XElement[] reportElements = this.reportsList.Root.Descendants(ELELMENT_REPORT_NAME).ToArray();

            if (reportElements.Length == 0)
            {
                return null;
            }

            int maxNumber = reportElements.Length;
            nextReportSlide.ReportSlideNumber = CurrentSlideNumber == (maxNumber - 1) ? 0 : CurrentSlideNumber + 1;

            XElement reportItem = reportElements[nextReportSlide.ReportSlideNumber];

            if (reportItem.Attribute(ATTRIB_DISPLAY_SECONDS_NAME) != null)
            {
                int duration;
                nextReportSlide.DisplaySeconds = int.TryParse(
                                                     reportItem.Attribute(ATTRIB_DISPLAY_SECONDS_NAME).Value, 
                                                     out duration)
                                                     ? duration
                                                     : DEFAULT_PAUSE_DURATION;
            }
            else
            {
                nextReportSlide.DisplaySeconds = DEFAULT_PAUSE_DURATION;
            }

            nextReportSlide.ReportPath = reportItem.Attribute(ATTRIB_REPORTPATH_NAME).Value;

            if (reportItem.HasElements)
            {
                XElement[] parameterElements = reportItem.Descendants("Parameter").ToArray();
                foreach (XElement parameterElement in parameterElements)
                {

                    nextReportSlide.ReportParameters.Add(new ReportParameter(parameterElement.Attribute("Name").Value, parameterElement.Value));
                }
            }

            return nextReportSlide;
        }

        #endregion
    }
}