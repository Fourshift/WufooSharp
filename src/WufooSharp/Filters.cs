using System;
using System.Collections.Generic;

namespace WufooSharp
{
    public enum FilterOperator
    {
        Contains,
        DoesNotContain,
        BeginsWith,
        EndsWith,
        IsLessThan,
        IsGreaterThan,
        IsOn,
        IsBefore,
        IsAfter,
        IsNotEqualTo,
        IsEqualTo,
        IsNotNull
    }
    public enum FilterMatchType
    {
        And,
        Or
    }
    public enum SortDirection
    {
        Asc,
        Desc
    }
    public class Sort {
        public SortDirection Direction { get; private set; }
        public string Field { get; private set; }
        public Sort(string field , SortDirection direction) {
            this.Field = field;
            this.Direction = direction;
        }
    }
    public class Filter
    {
        private static readonly Dictionary<FilterOperator, string> __filterStringLookup = new Dictionary<FilterOperator, string> { 
          { FilterOperator.Contains, "Contains" },
          { FilterOperator.DoesNotContain, "Does_not_contain" },
          { FilterOperator.BeginsWith, "Begins_with" },
          { FilterOperator.EndsWith, "Ends_with"},
          { FilterOperator.IsLessThan, "Is_less_than" },
          { FilterOperator.IsGreaterThan, "Is_greater_than"},
          { FilterOperator.IsOn, "Is_on"},
          { FilterOperator.IsBefore, "Is_before"},
          { FilterOperator.IsAfter, "Is_after" },
          { FilterOperator.IsNotEqualTo, "Is_not_equal_to" },
          { FilterOperator.IsEqualTo, "Is_equal_to" },
          { FilterOperator.IsNotNull, "Is_not_NULL" }
        };

        public FilterOperator Operator { get; private set; }
        public string Field { get; private set; }
        public string Value { get; private set; }
        
        public Filter(FilterOperator op, string field, string value)
        {
            this.Operator = op;
            this.Field = field;
            this.Value = value;
        }
        public override string ToString()
        {
            string format = Operator == FilterOperator.IsNotNull ? "{0}+{1}" : "{0}+{1}+{2}";
            return format.FormatWith(Field, __filterStringLookup[Operator], Value);
        }
    }

}