using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public class TelecomeBoltonData
    {
        public TelecomeBoltonData(string destinationMobile, TelecomOperator telecomOperator, SimType simType, int productId)
        {
            DestinationMobile = destinationMobile;
            TelecomOperator = telecomOperator;
            SimType = simType;
            ProductId = productId;
        }
        public string DestinationMobile { get; private set; }
        public int ProductId { get; private set; }
        public TelecomOperator TelecomOperator { get; private set; }
        public SimType SimType { get; private set; }
        public int SimTypeId
        {
            get
            {
                return SimType.Id;
            }
        }
    }
}