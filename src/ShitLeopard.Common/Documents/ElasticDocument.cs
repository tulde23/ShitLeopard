using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShitLeopard.Common.Documents
{
    public abstract class ElasticDocument
    {
        public virtual string DocumentId { get; set; }
        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        /// <value>
        /// The update date.
        /// </value>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Gets or sets the relative search rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public double? Score { get; set; }
    }
}
