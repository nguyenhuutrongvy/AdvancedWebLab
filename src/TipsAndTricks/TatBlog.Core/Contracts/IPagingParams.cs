using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.Contracts
{
    // Chứa các thông tin cần thiết cho việc phân trang
    public interface IPagingParams
    {
        // Số mẩu tin trên một trang
        int PageSize { get; set; }

        // Số trang tính bắt đầu từ 1
        int PageNumber { get; set; }

        // Tên cột muốn sắp xếp
        string SortColumn { get; set; }

        // Thứ tự sắp xếp: tăng hoặc giảm
        string SortOrder { get; set; }
    }
}
