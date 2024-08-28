using System;
using System.Collections.Generic;
using System.Text;

namespace ShushrutEyeHospitalCRM.Models.Wrapper
{
    public class PagingResult<T>
    {
        public List<T> Data { get; set; }

        /// <summary>
        /// The actual result of returned rows
        /// </summary>
        public int ReturnedRow { get => Data == null ? 0 : Data.Count; }

        /// <summary>
        /// Total length
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int ItemsPerPage
        {
            get; set;
        }
    }
}
