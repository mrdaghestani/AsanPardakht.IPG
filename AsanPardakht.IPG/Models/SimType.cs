using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public class SimType
    {
        public static SimType PrePaied = new SimType(1, "اعتباری");
        public static SimType PostPaied = new SimType(2, "دائمی");
        public static SimType TDLTE = new SimType(3, "TD-LTE");
        public static SimType Data = new SimType(4, "دیتا");

        public static IEnumerable<SimType> GetList()
        {
            yield return PrePaied;
            yield return PostPaied;
            yield return TDLTE;
            yield return Data;
        }

        public SimType(int id, string title)
        {
            Id = id;
            Title = title;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;
            return ((TelecomOperator)obj).Id == Id;
        }
    }
}
