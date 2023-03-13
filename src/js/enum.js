const Enum = {
    //Trạng thái form submit
    FormMode: {
        Post: 1, //thêm mới
        Put: 2, //sửa
        Clone: 3, //nhân bản
        View: 4, //xem
    },

    //Các loại popup
    PopupType: {
        Notify: 0, //Thông báo bình thường.
        DataChange: 1, //Cảnh báo dữ liệu thay đổi
        DataDelete: 2, //Cảnh báo xóa dữ liệu
        DataDuplicate: 3, //Cảnh báo dữ liệu trùng lặp
        DataError: 4, //Thông báo lỗi
    },

    // Các loại toast message
    ToastType: {
        Success: 0, //thông báo thành công
        Error: 1, //thông báo lỗi
        Warning: 2, //cảnh báo
        Info: 3, //thông báo
        Question: 4, //câu hỏi?
    },

    //Các status code mà api trả về
    StatusCode: {
        OK: 200, //Thành công
        Created: 201, //Thêm mới thành công
        Accepted: 202,
        NoContent: 204, //Không có dữ liệu
        BadRequest: 400, //Dữ liệu gửi lên không hợp lệ
        Unauthorized: 401, //Hệ thống chưa được ủy quyền
        Forbidden: 403, //Máy chủ từ chối yêu cầu (Ngay cả khi đã được cấp quyền)
        NotFound: 404, // không tìm thấy bất kỳ thứ gì với request này
        TimeOut: 408, //
        InternalServerError: 500, //Có lỗi xảy ra phía máy chủ
    },

    //Các keycode tương ứng với các phím trên bàn phím
    KeyCode: {
        Enter: 13,
        Tab: 9,
        Shift: 16,
        Ctrl: 17,
        Alt: 18,
        CapsLock: 20,
        ESC: 27,
        Left: 37,
        Up: 38,
        Right: 39,
        Down: 40,
        F1: 112,
        S: 83,
    },

    //Các lựa chọn của table function
    TableFunction: {
        Clone: 1, //nhân bản
        Delete: 2, // xóa
        Stop: 3, //ngưng sử dụng
        View: 4, //Xem
        Update: 5, //sửa
    },

    //Các tùy chọn format cho datetime
    DateFormat: "DD/MM/YYYY",
};
export default Enum;