//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DishesYulin4ISP9_7.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderProduct
    {
        public int ID { get; set; }
        public int IdOrder { get; set; }
        public string ProductArticleNumber { get; set; }
        public int ProductQty { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}