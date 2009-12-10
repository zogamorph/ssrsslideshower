// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportSlide.cs" company="SSRS Slide Shower Development Team">
//   Copyright (c) 2009
// </copyright>
// <summary>
//   The report slide.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SSRSSlideShower.App_Code
{
    using System.Collections.Generic;

    using Microsoft.Reporting.WebForms;

    /// <summary>
    /// The report slide.
    /// </summary>
    public class ReportSlide
    {
        private List<ReportParameter> reportParameters;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportSlide"/> class.
        /// </summary>
        public ReportSlide()
        {
            this.reportParameters = new List<ReportParameter>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportSlide"/> class.
        /// </summary>
        /// <param name="reportPath">
        /// The report path.
        /// </param>
        /// <param name="displaySeconds">
        /// The display seconds.
        /// </param>
        /// <param name="reportSlideNumber">
        /// The report slide number.
        /// </param>
        public ReportSlide(string reportPath, int displaySeconds, int reportSlideNumber) : this()
        {
            this.ReportPath = reportPath;
            this.DisplaySeconds = displaySeconds;
            this.ReportSlideNumber = reportSlideNumber;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets DisplaySeconds.
        /// </summary>
        public int DisplaySeconds { get; set; }

        /// <summary>
        /// Gets or sets ReportPath.
        /// </summary>
        public string ReportPath { get; set; }

        /// <summary>
        /// Gets or sets ReportSlideNumber.
        /// </summary>
        public int ReportSlideNumber { get; set; }

        public List<ReportParameter> ReportParameters
        {
            get
            {
                return this.reportParameters;
            }
        }

        #endregion
    }
}