1. Tất cả input trong các form đều phải điền ko đc để trống trước khi bấm submit + validate định dạng cho từng input -> sử dụng regex
2. Khi xóa hiện dialog xác nhận (Bạn có muốn xóa sinh viên 17020564 - Bùi Vũ Anh)
3. Trang trí cho đẹp (Thêm icon cho các button bằng cách sử dụng font awesome, nó có sẵn icon rồi thêm class là đc thì phải)
4. Xóa bớt mấy cái html ko động đến đi
*Mỗi page t đã sử dụng 1 file js tên tương ứng, có gì chỉ việc sửa ở đây. VD: wwwroot/js/admin/Student.js

Cách thêm database:
- Tạo database có tên "BTlon2"
- Chạy các file query theo thứ tự
- Thêm tài khoản admin, user để đăng nhập được
- Thêm vài dữ liệu mẫu trước cũng đc, GUID thì generate ở web này: https://www.guidgenerator.com/online-guid-generator.aspx

Thêm từ excel: 
- Chỉ có trong Admin/Student
- File excel mẫu đã có trong github, các header của file excel là các thuộc tính của đối tượng User nhưng không được viết hoa chữ đầu tiên
	 VD: UserName -> userName
- Khi thêm môn học cho sinh viên thực chất là thêm các id vào bảng SV_MonThi_KiThi, tạo 1 file excel tương tự rồi thêm vào 