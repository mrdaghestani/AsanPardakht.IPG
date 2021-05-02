using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public class TelecomOperator
    {
        public static TelecomOperator Mci = new TelecomOperator(1, "همراه اول", ServiceType.TelecomChargeMci, ServiceType.TelecomBoltonMci);
        public static TelecomOperator Irancell = new TelecomOperator(2, "ایرانسل", ServiceType.TelecomChargeIrancell, ServiceType.TelecomBoltonIrancell);
        public static TelecomOperator Rightel = new TelecomOperator(4, "رایتل", ServiceType.TelecomChargeRightel, ServiceType.TelecomBoltonRightel);
        public static TelecomOperator Aptel = new TelecomOperator(5, "آپتل", ServiceType.TelecomChargeAzartel, ServiceType.TelecomBoltonAptel);
        public static TelecomOperator Azartel = new TelecomOperator(6, "آذرتل", ServiceType.TelecomChargeAzartel, ServiceType.TelecomBoltonAzartel);

        public static IEnumerable<TelecomOperator> GetList()
        {
            yield return Mci;
            yield return Irancell;
            yield return Rightel;
            yield return Aptel;
            yield return Azartel;
        }

        public TelecomOperator(int id, string title, ServiceType chargeServiceType, ServiceType boltonServiceType)
        {
            Id = id;
            Title = title;
            ChargeServiceType = chargeServiceType;
            BoltonServiceType = boltonServiceType;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public ServiceType ChargeServiceType { get; private set; }
        public ServiceType BoltonServiceType { get; private set; }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;
            return ((TelecomOperator)obj).Id == Id;
        }
    }
}
