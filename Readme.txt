1. Tất cả input trong các form đều phải điền ko đc để trống trước khi bấm submit + validate định dạng cho từng input -> sử dụng regex
2. Khi xóa hiện dialog xác nhận (Bạn có muốn xóa sinh viên 17020564 - Bùi Vũ Anh)
3. Khi "Chọn làm kì thi hiện tại" hiện dialog xác nhận 

Cách thêm database:
- Tạo database có tên "BTlon2"
- Chạy các file query theo thứ tự
- Thêm tài khoản admin, user để đăng nhập được
- Thêm vài dữ liệu mẫu trước cũng đc, GUID thì generate ở web này: https://www.guidgenerator.com/online-guid-generator.aspx
- Lấy connection string rồi sửa vào file Models/BTLonContext.cs. Ở dòng 34
       optionsBuilder.UseSqlServer("{đặt connection string vào đây}");

Thêm từ excel: 
- Chỉ có trong Admin/Student
- File excel mẫu đã có trong github, các header của file excel là các thuộc tính của đối tượng User nhưng không được viết hoa chữ đầu tiên
	 VD: UserName -> userName
- Thêm xong user thì phải quay về "Chọn làm kì thi hiện tại" vì ban đầu mình chưa cho KyThiID vào user
- Khi thêm môn học cho sinh viên thực chất là thêm các id vào bảng SV_MonThi_KiThi, tạo 1 file excel tương tự rồi thêm vào 

