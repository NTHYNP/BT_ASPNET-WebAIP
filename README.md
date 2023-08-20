![image](https://github.com/NTHYNP/BT_ASPNET-WebAIP/assets/128686080/8e806729-cffa-4261-a3ed-9acbb2fa3c6f)

Thực hiện các yêu cầu sau:
Viết API thêm sửa xóa hóa đơn, sản phẩm, trong đó:

*API thêm hóa đơn: 
  - Mỗi hóa đơn sẽ có mã giao dịch và mã này là duy nhất, 
    mã giao dịch được tạo ra tự động theo quy tắc YYYYMMDD_00X, 
    trong đó YYYY là năm hiện tại (Vd: 2020, 2021),
     MM là tháng hiện tại (Vd: 01, 02),
     DD là ngày hiện tại (Vd: 15, 25)
     X là chỉ số thể hiện hóa đơn là hóa đơn thứ bao nhiêu trong ngày
     Ví dụ: ngày 20 tháng 10 năm 2020 hóa đơn đầu tiên => Mã giao dịch = 20201020_001
    Thời gian tạo hóa đơn = thời gian hiện tại
    Tổng tiền = 0
    Sau khi thêm hóa đơn sẽ thêm chi tiết của hóa đơn đó:
    Cần kiểm tra sản phẩm trong hóa đơn đã tồn tại hay chưa. 

      Nếu chưa thì kết thúc và xóa hóa đơn vừa tạo. Thông báo nội dung: "Sản phẩm chưa tồn tại. Vui lòng kiểm tra lại!"
    Nếu sản phẩm đã tồn tại, thành tiền = số lượng sp * giá thành của sp đó.
  Sau khi thêm các chi tiết đơn hàng. Tiến hành cập nhật Tổng tiền của hóa đơn đó
  Tổng tiền = tổng thành tiền của các chi tiết hóa đơn.


*API sửa hóa đơn
  Kiểm tra danh sách chi tiết hóa đơn để cập nhật danh sách chi tiết hóa đơn 
  nếu có sự thay đổi (Thêm, sửa, bớt) sản phẩm trong hóa đơn
  Cập nhật tổng tiển của hóa đơn đó nếu danh sách chi tiết hóa đơn thay đối
  Thời gian cập nhật hóa đơn = thời gian hiện tại

*API xóa hóa đơn
  Xóa hóa đơn và danh sách chi tiết của hóa đơn đó

*Viết API sử dụng kỹ thuật phân trang để lấy dữ liệu, mỗi lần lấy 20 hoặc 30 hóa đơn đồng thời vẫn lọc được dữ liệu
  -lấy dữ liệu hóa đơn cùng chi tiết của hóa đơn đó, sắp xếp theo thời gian tạo mới nhất.
  -để lọc hóa đơn theo yêu cầu sau:
      Lấy hóa đơn theo năm, tháng
      Lấy hóa đơn được tạo từ ngày ... đến ngày
      Lấy hóa đơn theo tổng tiền từ XXXX -> XXXX
      Tìm kiếm hóa đơn theo Mã giao dịch hoặc tên hóa đơn
      Tìm kiếm sản phẩm theo tên  
