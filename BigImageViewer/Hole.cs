using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigImageViewer {
    class Hole {
        public float x;
        public float y;
        public float w;
        public float h;
        public bool exist;
        public Hole(float x, float y, float dx, float dy, bool exist) {
            this.x = x;
            this.y = y;
            this.w = dx;
            this.h = dy;
            this.exist = exist;
        }
    }
}
