namespace Greenglobal.Core.Models
{
    /// <summary>
    /// Paging Model Request
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// Number of page
        /// </summary>
        public int PageNumber { get; set; }


        /// <summary>
        /// Size of page
        /// </summary>
        public int PageSize { get; set; }
    }
}
