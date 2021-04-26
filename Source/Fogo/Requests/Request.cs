using Fogo.Filters;

namespace Fogo.Requests {

    public class Request {
        private int _skip = 0;
        private int _take = 25;

        public int Skip {
            get => _skip;
            set {
                if (value >= 0) {
                    _skip = value;
                }
            }
        }

        public int Take {
            get => _take;
            set {
                if (value > 0) {
                    _take = value;
                }
            }
        }

        public Filter Filter { get; set; }

        public static implicit operator Request(int skip) => new Request { Skip = skip };
    }
}