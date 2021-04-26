using Fogo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fogo.Requests {

    public class PageResult<TResult> {
        private readonly int _page = 1;
        private readonly int _size = 25;
        private readonly int _count = 0;
        private readonly int _total = 0;
        private readonly int _pages = 0;
        private readonly IEnumerable<TResult> _results = Array.Empty<TResult>();
        private readonly bool _hasNextPage = false;
        private readonly bool _hasPreviousPage = false;
        private readonly Filter _filter;

        public PageResult() {
        }

        public PageResult(PageRequest request) {
            _page = request.Page;
            _size = request.Size;
            _filter = request.Filter;
        }

        public PageResult(PageRequest request, int total, IEnumerable<TResult> results) : this(request) {
            if (total >= 0) {
                _total = total;
            }
            if (results != null) {
                _results = results;
                _count = _results.Count();
                _pages = (int)Math.Ceiling((double)_total / _size);
                _hasNextPage = _page < _pages;
                _hasPreviousPage = _page > 1;
            }
        }

        public int Page => _page;
        public int Size => _size;
        public int Count => _count;
        public int Total => _total;
        public int Pages => _pages;
        public bool HasNextPage => _hasNextPage;
        public bool HasPreviousPage => _hasPreviousPage;
        public IEnumerable<TResult> Results => _results;
        public Filter Filter => _filter;
    }
}