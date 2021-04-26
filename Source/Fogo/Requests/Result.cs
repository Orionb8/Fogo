using Fogo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fogo.Requests {

    public class Result<TResult> {
        private readonly int _skip = 0;
        private readonly int _take = 25;
        private readonly int _count = 0;
        private readonly int _total = 0;
        private readonly IEnumerable<TResult> _results = Array.Empty<TResult>();
        private readonly Filter _filter;

        public Result() {
        }

        public Result(Request request) {
            _skip = request.Skip;
            _take = request.Take;
            _filter = request.Filter;
        }

        public Result(Request request, int total, IEnumerable<TResult> results) : this(request) {
            if (total >= 0) {
                _total = total;
            }
            if (results != null) {
                _results = results;
                _count = _results.Count();
            }
        }

        public int Skip => _skip;
        public int Take => _take;
        public int Count => _count;
        public int Total => _total;
        public IEnumerable<TResult> Results => _results;
        public Filter Filter => _filter;
    }
}