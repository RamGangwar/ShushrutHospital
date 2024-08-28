using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShushrutEyeHospitalCRM.Models.Wrapper
{
    public class Paginable : Sortable
    {
        /// <summary>
        /// 50 items per page by default. You can change this value.
        /// </summary>
        public static int DEFAULT_PER_PAGE = 10;

        /// <summary>
        /// From
        /// </summary>        
        public virtual Nullable<int> Skip { get; set; } = 0;

        public virtual Nullable<int> Take { get; set; }

    }
}
