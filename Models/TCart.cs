using System;
using System.Collections.Generic;

namespace BanHangAsp.Models
{
    public partial class TCart
    {
        public string CartId { get; set; } = null!;

        public virtual TDanhMucSp? MaSp { get; set; }

        public virtual TKhachHang? MaKhanhHang { get; set; }

        public int Quantity { get; set; }

        public DateOnly? DateCreated { get; set; }
    }
}
