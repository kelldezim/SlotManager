﻿using SlotManager.Api.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Api.ValueObjects
{
    public sealed record LicensePlate //: IEquatable<LicensePlate>
    {
        public string Value { get; }

        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyLicencePlateException();
            }

            if(value.Length is < 5 or > 8)
            {
                throw new InvalidLicencePlateException(value);
            }

            Value = value;
        }

        public static implicit operator string(LicensePlate licensePlate)
        {
            return licensePlate?.Value;
        }

        public static implicit operator LicensePlate(string licensePlate)
        {
            return new LicensePlate(licensePlate);
        }
    }
}
