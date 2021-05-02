using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public enum ServiceType
    {
        [Description("خرید")]
        Buy = 1,
        [Description("قبض")]
        Bill = 3,
    }
}
