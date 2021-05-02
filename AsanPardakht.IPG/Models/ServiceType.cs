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
        //TelecomCharge = 60,
        TelecomChargeMci = 61,
        TelecomChargeIrancell = 62,
        TelecomChargeRightel = 64,
        TelecomChargeAzartel = 66,
        //TelecomBolton = 70,
        TelecomBoltonMci = 71,
        TelecomBoltonIrancell = 72,
        TelecomBoltonRightel = 74,
        TelecomBoltonAptel = 75,
        TelecomBoltonAzartel = 76,
    }
}
