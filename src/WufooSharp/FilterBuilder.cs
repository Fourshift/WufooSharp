using System;
using System.Collections.Generic;

namespace WufooSharp
{

    public class FilterBuilder
    {

        private List<Filter> _filters;
        public FilterMatchType MatchType { get; set; }

        public Filter[] GetFilters()
        {
            return _filters.ToArray();
        }

        public FilterBuilder(FilterMatchType matchType)
        {
            _filters = new List<Filter>();
            MatchType = matchType;
        }
        public FilterBuilder()
            : this(FilterMatchType.And)
        {
        }


        public FilterBuilder Contains(string field, string value)
        {
            AddFilter(FilterOperator.Contains, field, value);
            return this;
        }

        public FilterBuilder DoesNotContain(string field, string value)
        {
            AddFilter(FilterOperator.DoesNotContain, field, value);
            return this;
        }

        public FilterBuilder BeginsWith(string field, string value)
        {
            AddFilter(FilterOperator.BeginsWith, field, value);
            return this;
        }

        public FilterBuilder EndsWith(string field, string value)
        {
            AddFilter(FilterOperator.EndsWith, field, value);
            return this;
        }

        public FilterBuilder IsLessThan(string field, string value)
        {
            AddFilter(FilterOperator.IsLessThan, field, value);
            return this;
        }

        public FilterBuilder IsGreaterThan(string field, string value)
        {
            AddFilter(FilterOperator.IsGreaterThan, field, value);
            return this;
        }

        public FilterBuilder IsOn(string field, string value)
        {
            AddFilter(FilterOperator.IsOn, field, value);
            return this;
        }

        public FilterBuilder IsOn(string field, DateTime when)
        {
            return IsOn(field, when.ToString("yyyy-MM-dd"));
        }

        public FilterBuilder IsBefore(string field, string value)
        {
            AddFilter(FilterOperator.IsBefore, field, value);
            return this;
        }

        public FilterBuilder IsBefore(string field, DateTime when)
        {
            return IsBefore(field, when.ToString("yyyy-MM-dd"));
        }

        public FilterBuilder IsAfter(string field, string value)
        {
            AddFilter(FilterOperator.IsAfter, field, value);
            return this;
        }

        public FilterBuilder IsAfter(string field, DateTime when)
        {
            return IsAfter(field, when.ToString("yyyy-MM-dd"));
        }

        public FilterBuilder IsNotEqualTo(string field, string value)
        {
            AddFilter(FilterOperator.IsNotEqualTo, field, value);
            return this;
        }

        public FilterBuilder IsEqualTo(string field, string value)
        {
            AddFilter(FilterOperator.IsEqualTo, field, value);
            return this;
        }

        public FilterBuilder IsNotNull(string field)
        {
            AddFilter(FilterOperator.IsNotNull, field, string.Empty);
            return this;
        }

        private void AddFilter(FilterOperator op, string field, string value)
        {
            _filters.Add(new Filter(op, field, value));
        }

    }
}
