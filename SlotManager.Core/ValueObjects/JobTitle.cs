using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Core.ValueObjects
{
    public sealed class JobTitle
    {
        public string Value { get; }

        public const string Employee = nameof(Employee);
        public const string Manager = nameof(Employee);
        public const string Boss = nameof(Employee);

        private JobTitle(string value)
        {
            Value = value;
        }

        public static implicit operator string(JobTitle jobTitle)
        {
            return jobTitle.Value;
        }

        public static implicit operator JobTitle(string value)
        {
            return new JobTitle(value);
        }
    }
}
