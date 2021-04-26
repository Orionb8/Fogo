using Fogo.Filters;

namespace Fogo.Requests {

    public class PageRequest {
        private int _page = 1;
        private int _size = 25;

        public int Page {
            get => _page;
            set {
                if (value > 0) {
                    _page = value;
                }
            }
        }

        public int Size {
            get => _size;
            set {
                if (value > 0) {
                    _size = value;
                }
            }
        }

        public Filter Filter { get; set; }

        public static implicit operator PageRequest(int page) => new PageRequest { Page = page };
    }
}