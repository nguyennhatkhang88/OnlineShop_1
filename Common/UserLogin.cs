using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnLineShop_1.Common
{
    [Serializable]//Để gửi một đối tượng từ quy trình này sang quy trình khác (Miền ứng dụng) 
    //trên cùng một máy và cũng gửi đối tượng qua dây tới một quy trình đang chạy trên máy khác
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }
    }
}