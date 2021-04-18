using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public class TurkishMessage : IMessageService
    {
        public TurkishMessage()
        {
              CarNameInvalid = "Girilen araç ismi geçersiz.";
              CarListed = "Araçlar listelendi.";
              CarNotFound = "Araca ait bilgi bulunamadı.";
              CarFound = "Araca ait bilgiler getirildi.";
              CarAdded = "Araç bilgileri eklendi.";
              CarNotAdded = "Araç bilgileri eklenirken hata oluştu.";
              CarUpdated = "Araç bilgileri güncellendi.";
              CarDeleted = "Araç bilgileri silindi.";
        }

        public string CarNameInvalid { get; }

        public string CarListed { get; }

        public string CarNotFound { get; }

        public string CarFound { get; }

        public string CarAdded { get; }

        public string CarNotAdded { get; }

        public string CarUpdated { get; }

        public string CarDeleted { get; }
    }
}
