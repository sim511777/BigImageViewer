using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigImageViewer {
    public class Hole {
        public float x;
        public float y;
        public float w;
        public float h;
        public int idxX;
        public int idxY;
        public int fwd;
        public Hole(float x, float y, float dx, float dy, int idxX, int idxY, int fwd) {
            this.x = x;
            this.y = y;
            this.w = dx;
            this.h = dy;
            this.idxX = idxX;
            this.idxY = idxY;
            this.fwd = fwd;
        }
    }
}
