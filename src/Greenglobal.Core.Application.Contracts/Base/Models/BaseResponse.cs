namespace Greenglobal.Core.Models
{
    /// <summary>
    /// Base Model Reponse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// Status code
        /// </summary>
        public int Status { get; set; } = 200;

        /// <summary>
        /// Message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public T? Data { get; set; }
    }
}
