using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.DataLayer
{
    public interface ICategoryDAL
    {
        /// <summary>
        /// lấy danh sách các mặt hàng
        /// </summary>
        /// <returns></returns>
        IList<Category> List();
        /// <summary>
        /// Lấy thông tin của 1 loại hàng theo mã loại hàng
        /// </summary>
        /// <param name="categoryID">Mã loại hàng cần lấy</param>
        /// <returns></returns>
        Category Get(int categoryID);
        /// <summary>
        /// bổ sung một loại hàng mới. hàm trả về mã loaij hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Category data);
        /// <summary>
        /// cập nhật thông tin của một loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);
        /// <summary>
        /// xoá 1 loại hàng dựa vào mã loại hàng, lưu ý không xóa nếu loại hàng đã đc sử dụng ở 1 mặt hàng nào đó
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        bool Delete(int categoryID);
    }
}
