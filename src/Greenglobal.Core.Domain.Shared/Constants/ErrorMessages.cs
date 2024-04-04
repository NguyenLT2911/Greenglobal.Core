namespace Greenglobal.Core.Constants
{
    public static class ErrorMessages
    {
        #region Messages Common
        public static class POST
        {
            /// <summary>
            /// Created
            /// </summary>
            public const string Created = "Tạo thành công";

            /// <summary>
            /// Added
            /// </summary>
            public const string Added = "Thêm thành công";

            /// <summary>
            /// Sent
            /// </summary>
            public const string Sent = "Gửi thành công";

            /// <summary>
            /// Create failed
            /// </summary>
            public const string CannotCreate = "Tạo không thành công. Có lỗi xảy ra trong quá trình tạo";

            /// <summary>
            /// Send failed
            /// </summary>
            public const string SentError = "Gửi không thành công";
        }

        public static class PUT
        {
            /// <summary>
            /// Updated
            /// </summary>
            public const string Updated = "Cập nhật thành công";

            /// <summary>
            /// Update failed
            /// </summary>
            public const string CannotUpdate = "Update không thành công. Có lỗi xảy ra trong quá trình cập nhật";
        }

        public static class DELETE
        {
            /// <summary>
            /// Deleted
            /// </summary>
            public const string Deleted = "Xóa thành công";

            /// <summary>
            /// Cannot delete
            /// </summary>
            public const string CannotDelete = "Không thể xóa dữ liệu này. Bạn không có quyền hoặc không tồn tại";

            /// <summary>
            /// Deleted error
            /// </summary>
            public const string DeletedError = "Xóa không thành công. Có lỗi xảy ra trong quá trình xóa";
        }

        public static class GET
        {
            /// <summary>
            /// Get data successfully
            /// </summary>
            public const string Getted = "Lấy dữ liệu thành công";

            /// <summary>
            /// Get data fail
            /// </summary>
            public const string GetFail = "Lấy dữ liệu không thành công";
        }

        public static class FILE
        {
            /// <summary>
            /// Choose file
            /// </summary>
            public const string Required = "Vui lòng lựa chọn file";

            /// <summary>
            /// Upload file successfully
            /// </summary>
            public const string Uploaded = "Upload thành công";

            /// <summary>
            /// Invalid file type
            /// </summary>
            public const string InvalidFileType = "Kiểu file không hợp lệ";

            /// <summary>
            /// Invalid file size
            /// </summary>
            public const string InvalidFileSize = "Kích thước file nhỏ hơn {0} MB";
        }

        public static class COMMON
        {
            /// <summary>
            /// The successfully
            /// </summary>
            public const string Successfully = "Thành công";

            /// <summary>
            /// The failly
            /// </summary>
            public const string Failly = "Thất bại";

            /// <summary>
            /// 403 Forbidden
            /// </summary>
            public const string Forbidden403 = "Người dùng không được phân quyền";
        }

        public static class VALID
        {
            /// <summary>
            /// Existed
            /// </summary>
            public const string Existed = "{0} đã tồn tại";

            /// <summary>
            /// The invalid field
            /// </summary>
            public const string InvalidField = "{0} không hợp lệ";

            /// <summary>
            /// The invalid parameters
            /// </summary>
            public const string RequestModelNotNull = "Không được để trống dữ liệu nhập vào";

            /// <summary>
            /// The not email format
            /// </summary>
            public const string NotEmailFormat = "Không đúng định dạng email";

            /// <summary>
            /// Not existed
            /// </summary>
            public const string NotExisted = "{0} không tồn tại";

            /// <summary>
            /// The invalid sort field
            /// </summary>
            public const string InvalidSortField = "Sort field không hợp lệ";

            /// <summary>
            /// The required field
            /// </summary>
            public const string RequiredField = "Vui lòng nhập trường {0}";
        }
        #endregion

        #region User
        public static class User
        {
        }
        #endregion

        #region Unit
        public static class Unit
        {
        }
        #endregion
    }
}
