namespace NguyenVanThinh_Buoi4.Models
{
    public class ShoppingCart
    {
        //Danh sách sản phẩm trong giỏ hàng
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        /// <summary>
        /// Thêm sản phẩm vô giỏ hàng
        /// </summary>
        /// <param name="item">sản phẩm</param>
        public void AddItem(CartItem item)
        {
            //Kiểm tra trong giỏ hàng đã tồn tại sản phẩm đó trước ko
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                //Nếu tồn tại thì mình chỉ cần tăng số lượng
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                //Nếu ko tồn tại thì mình add mới
                Items.Add(item);
            }
        }


        /// <summary>
        /// Bỏ, vứt sản phẩm ra khỏi giỏ
        /// </summary>
        /// <param name="productId">id sản phẩm</param>
        public void RemoveItem(int productId)
        {
            //Thực hiện remove sản phẩm có id product
            Items.RemoveAll(i => i.ProductId == productId);
        }

        // Các phương thức khác...
    }
}
