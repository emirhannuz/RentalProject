using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public class EnglishMessage : IMessageService
    {
        public EnglishMessage()
        {
            CarNameInvalid = "Car name invalid.";
            CarListed = "Cars listed.";
            CarNotFound = "Car not found.";
            CarFound = "Car found.";
            CarAdded = "Car information added.";
            CarNotAdded = "Car information does not added.";
            CarUpdated = "Car information modified.";
            CarDeleted = "Car information deleted.";
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
